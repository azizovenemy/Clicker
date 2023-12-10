using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public CurrentEnemyData CurrentEnemyData;
        public PlayerData PlayerData;
        
        public PlayerProgress(int currentEnemyIndex, float currentEnemyHp, float balance)
        {
            CurrentEnemyData = new CurrentEnemyData(currentEnemyIndex, currentEnemyHp);
            PlayerData = new PlayerData(balance);
        }
    }
}