using System;
using CodeBase.StaticData;

namespace CodeBase.Data
{
    [Serializable]
    public class UpgradesData
    {
        public EUpgradeTypeId upgradeType;
        public int count;

        public UpgradesData(EUpgradeTypeId upgradeType, int count)
        {
            this.count = count;
            this.upgradeType = upgradeType;
        }
    }
}