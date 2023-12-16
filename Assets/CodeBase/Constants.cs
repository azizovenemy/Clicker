using System;

namespace CodeBase
{
    public static class Constants
    {
        public const string BootstrapSceneName = "Bootstrap";
        public const string MainSceneName = "MainScene";
        public const string UIPath = "UI/INTERFACE";
        public const string UpgradeEntityPath = "UI/Upgrades/UpgradeEntity";
        public const string PlayerPath = "Player/Player";
        public const string EnemyStaticDataPath = "StaticData/Enemy";
        public const string LevelStaticDataPath = "StaticData/Level";
        public const string SpawnerPath = "Enemy/SpawnPoint";
        
        public const string ProgressKey = "Progress";

        public const int Increase = 10;

        private static readonly string[] Reductions = { "", "K", "M", "B", "T" };

        public static string FormatString(double num)
        {
            if (num == 0) return "0";

            num = Math.Round((float) num);

            int i = 0;

            while(i + 1 < Reductions.Length && num >= 1000d)
            {
                num /= 1000d;
                i++;
            }

            return num.ToString(format: "#.##") + Reductions[i];
        }
    }
}
