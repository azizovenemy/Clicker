using System;
using System.Collections.Generic;
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
        public int CurrentEnemyIndex { get; private set; }
        public GameObject CurrentEnemy { get; private set; }

        private EEnemyTypeId _typeId;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        private readonly Dictionary<EEnemyTypeId, Color32> _colors = new()
        {
            { EEnemyTypeId.Cube, new Color32(17, 1, 180, 255) },
            { EEnemyTypeId.Capsule, new Color32(180, 1, 17, 255) },
            { EEnemyTypeId.Sphere, new Color32(1, 180, 17, 255) },
        };

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            
            Spawn();
        }

        private EEnemyTypeId ChooseEnemyTypeById(int index)
        {
            if (index % 5 == 0) return EEnemyTypeId.Capsule;

            var type = Random.Range(0, 2);
            return type == 0 ? EEnemyTypeId.Sphere : EEnemyTypeId.Cube;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            CurrentEnemyIndex = progress.currentEnemyData.index;
        }

        public void UpdateProgress(PlayerProgress progress) => 
            progress.currentEnemyData.index = CurrentEnemyIndex;

        private void Spawn()
        {
            _typeId = ChooseEnemyTypeById(CurrentEnemyIndex);
            CurrentEnemy = _gameFactory.InitEnemy(_typeId, transform, CurrentEnemyIndex);
            CurrentEnemy.GetComponent<MeshRenderer>().material.color = _colors[_typeId];
            _enemyDeath = CurrentEnemy.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += OnSlain;
        }

        private void OnSlain()
        {
            Balance.Instance.IncreaseBalance(CalculateReward(CurrentEnemyIndex));
            
            _enemyDeath.Happened -= OnSlain;
            CurrentEnemyIndex++;
            Spawn();
        }

        private float CalculateReward(int index) =>
            (Constants.Increase + (index % 5 == 0 ? index * Constants.Increase : index)) * CalculateRewardByUpgrade();

        private float CalculateRewardByUpgrade() =>
            Upgrades.Instance.FindExists(EUpgradeTypeId.MoneyRewardIncrease)
                ? Upgrades.Instance.GetUpgradeCount(EUpgradeTypeId.MoneyRewardIncrease) * 1.54f
                : 0.0f;
    }
}