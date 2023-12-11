using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelData : ScriptableObject
    {
        public string LevelKey;

        public List<EnemySpawnerData> EnemySpawners;
    }
}