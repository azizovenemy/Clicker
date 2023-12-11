using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public CurrentEnemyData currentEnemyData = new();
        public PlayerData playerData = new();
    }
}