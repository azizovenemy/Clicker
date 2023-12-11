using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        public EEnemyTypeId enemyTypeId;

        public string Name;
        
        public GameObject Prefab;
        public GameObject DeathFx;
    }
}