using System;

namespace CodeBase.Data
{
    [Serializable]
    public class CurrentEnemyData
    {
        public int Index;
        public float MaxHP;
        public float CurrentHP;

        public CurrentEnemyData(int index, float currentHp)
        {
            Index = index;
            CurrentHP = currentHp;
        }
    }
}