using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EEnemyTypeId, EnemyData> _enemies;
        private Dictionary<string, LevelData> _levels;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyData>(Constants.EnemyStaticDataPath)
                .ToDictionary(x => x.enemyTypeId, x => x);

            _levels = Resources.LoadAll<LevelData>(Constants.LevelStaticDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public EnemyData ForEnemy(EEnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out var staticData) ? staticData : null;

        public LevelData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out var staticData) ? staticData : null;
    }
}