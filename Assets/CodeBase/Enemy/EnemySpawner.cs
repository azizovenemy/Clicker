using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public int CurrentEnemyIndex = 1;
        public int CurrentEnemyMoneyReward;
        
        private EEnemyTypeId _typeId;
        
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        private readonly Dictionary<EEnemyTypeId, Color32> _colors = new()
        {
            { EEnemyTypeId.Cube, new Color32(17, 1, 180, 200) },
            { EEnemyTypeId.Capsule, new Color32(180, 1, 17, 200) },
            { EEnemyTypeId.Sphere, new Color32(1, 180, 17, 200) },
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

        private float CalculateReward()
        {
            return CurrentEnemyIndex * Constants.Increase; //* uprageService.moneyIncrease;
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
            var enemy = _gameFactory.InitEnemy(_typeId, transform, CurrentEnemyIndex);
            enemy.GetComponent<MeshRenderer>().material.color = _colors[_typeId];
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += OnSlain;
        }

        private void OnSlain()
        {
            _enemyDeath.Happened -= OnSlain;
            CurrentEnemyIndex++;
            Spawn();
        }
    }
}