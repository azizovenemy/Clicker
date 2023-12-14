using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Upgrades;
using CodeBase.StaticData;
using CodeBase.UserInfo;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Enemy
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public GameObject CurrentEnemy { get; private set; }

        private int _currentEnemyIndex;
        private EEnemyTypeId _typeId;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            
            Spawn();
        }

        private EEnemyTypeId ChooseEnemyTypeById(int index)
        {
            if (index % 5 == 0 && index != 0) return EEnemyTypeId.Boss;

            var type = Random.Range(0, 2);
            return type == 0 ? EEnemyTypeId.Sphere : EEnemyTypeId.Cube;
        }

        public void LoadProgress(PlayerProgress progress) => 
            _currentEnemyIndex = progress.currentEnemyData.index;

        public void UpdateProgress(PlayerProgress progress) => 
            progress.currentEnemyData.index = _currentEnemyIndex;

        private void Spawn()
        {
            _typeId = ChooseEnemyTypeById(_currentEnemyIndex);
            CurrentEnemy = _gameFactory.InitEnemy(_typeId, transform, _currentEnemyIndex);
            _enemyDeath = CurrentEnemy.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += OnSlain;
        }

        private void OnSlain()
        {
            Balance.Instance.IncreaseBalance(CalculateReward(_currentEnemyIndex));
            
            _enemyDeath.Happened -= OnSlain;
            _currentEnemyIndex++;
            Spawn();
        }

        private float CalculateReward(int index) =>
            (Constants.Increase + (index % 5 == 0 ? index * Constants.Increase : index)) + CalculateRewardByUpgrade();

        private float CalculateRewardByUpgrade() =>
            Upgrades.Instance.FindExists(EUpgradeTypeId.MoneyRewardIncrease)
                ? Upgrades.Instance.GetUpgradeCount(EUpgradeTypeId.MoneyRewardIncrease) * 1.54f
                : 0f;
    }
}