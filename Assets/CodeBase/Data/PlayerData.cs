using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerData
    {
        public float Balance;
        //public Upgrades Upgrades;

        public PlayerData(float balance)
        {
            Balance = balance;
        }
    }
}