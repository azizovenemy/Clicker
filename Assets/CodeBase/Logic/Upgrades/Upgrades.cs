using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.Upgrades
{
    public class Upgrades : MonoBehaviour, ISavedProgress
    {
        public static Upgrades Instance => _instance;
        private static Upgrades _instance;

        [SerializeField] private Sprite autoDamageIncreaseImage;
        [SerializeField] private Sprite playerDamageIncreaseImage;
        [SerializeField] private Sprite moneyRewardIncreaseImage;
        
        private PlayerData _progress;
        private Dictionary<EUpgradeTypeId, Sprite> _images;
        private readonly List<UpgradeEntity> _entities = new();

        public float CalculateDamagePerSecond() =>
            FindExists(EUpgradeTypeId.AutoDamageIncrease)
                ? GetUpgradeCount(EUpgradeTypeId.AutoDamageIncrease) * 1.5f
                : 0f;

        public float CalculateDamagePerClick() =>
            FindExists(EUpgradeTypeId.PlayerDamageIncrease)
                ? GetUpgradeCount(EUpgradeTypeId.PlayerDamageIncrease)
                : 0f;

        public bool FindExists(EUpgradeTypeId upgradeType)
        {
            foreach (var upgrade in _entities.Where(upgrade => upgrade.IsExists(upgradeType)))
            {
                upgrade.GetCount(upgradeType, out var count);
                return count > 0;
            }

            return false;
        }

        public int GetUpgradeCount(EUpgradeTypeId upgradeType)
        {
            foreach (var upgrade in _entities)
            {
                if (upgrade.GetCount(upgradeType, out var count))
                {
                    return count;
                }
            }

            return 0;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress.playerData;
            InitializeUpgradesImages();
            InstantiateUpgrades();
            CreateSingleton();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            UpdateDictionary();
            progress.playerData = _progress;
        }

        private void InitializeUpgradesImages()
        {
            _images = new Dictionary<EUpgradeTypeId, Sprite>
            {
                { EUpgradeTypeId.AutoDamageIncrease, autoDamageIncreaseImage },
                { EUpgradeTypeId.PlayerDamageIncrease, playerDamageIncreaseImage },
                { EUpgradeTypeId.MoneyRewardIncrease, moneyRewardIncreaseImage },
            };
        }

        private void InstantiateUpgrades()
        {
            foreach (var upgrade in _progress.upgradesData)
                InstantiateUpgrade(Constants.UpgradeEntityPath, upgrade.upgradeType, upgrade.count);
        }

        private void InstantiateUpgrade(string path, EUpgradeTypeId upgradeType, int upgradeCount)
        {
            var upgradeEntity = Instantiate(Resources.Load<UpgradeEntity>(path), this.transform);
            upgradeEntity.Construct(upgradeType, upgradeCount, _images[upgradeType]);

            _entities.Add(upgradeEntity);
        }

        private void UpdateDictionary()
        {
            _progress.upgradesData.Clear();

            foreach (var entity in _entities)
                _progress.upgradesData.Add(new UpgradesData(entity.UpgradeType, entity.Count));
        }

        private void CreateSingleton()
        {
            //minus vaibik
            _instance = this;
        }
    }
}