using System.Collections.Generic;
using System.Text;
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

        private PlayerData _progress;

        private readonly List<UpgradeEntity> _entities = new List<UpgradeEntity>();

        public float CalculateDamagePerSecond() =>
            FindExists(EUpgradeTypeId.PlayerDamageIncrease) ? GetUpgradeCount(EUpgradeTypeId.PlayerDamageIncrease) * 0.3f : 0f;

        public float CalculateDamagePerClick() =>
            FindExists(EUpgradeTypeId.PlayerDamageIncrease) ? GetUpgradeCount(EUpgradeTypeId.PlayerDamageIncrease) * 0.3f : 0f;

        public void UpdateEntityInfo(EUpgradeTypeId upgradeType)
        {

        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress.playerData;
            InstantiateUpgrades();
            CreateSingleton();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            UpdateDictionary();
            progress.playerData = _progress;
        }

        private void InstantiateUpgrades()
        {
            foreach (var upgrade in _progress.upgradesData)
                InstantiateUpgrade(Constants.UpgradeEntityPath, upgrade.upgradeType, upgrade.count);
        }

        private void InstantiateUpgrade(string path, EUpgradeTypeId upgradeType, int upgradeCount)
        {
            var upgradeEntity = Instantiate(Resources.Load<UpgradeEntity>(path), this.transform);
            upgradeEntity.Construct(upgradeType, upgradeCount);

            _entities.Add(upgradeEntity);
        }

        private void UpdateDictionary()
        {
            _progress.upgradesData.Clear();

            foreach (var entity in _entities)
                _progress.upgradesData.Add(new UpgradesData(entity.UpgradeType, entity.Count));
        }

        public bool FindExists(EUpgradeTypeId upgradeType)
        {
            foreach (UpgradeEntity upgrade in _entities)
            {
                if (upgrade.IsExists(upgradeType))
                {
                    upgrade.GetCount(upgradeType, out var count);
                    return count > 0;
                }
            }

            return false;
        }

        public int GetUpgradeCount(EUpgradeTypeId upgradeType)
        {
            foreach (UpgradeEntity upgrade in _entities)
            {
                if (upgrade.GetCount(upgradeType, out var count))
                {
                    return count;
                }
            }

            return 0;
        }

        private void CreateSingleton()
        {
            //minus vaibik
            _instance = this;
        }

        private void DebugDictionaryInfo()
        {
            foreach (var field in _progress.upgradesData)
            {
                Debug.Log($"{field.upgradeType} - {field.count}");
            }
        }

        private void DebugAll(string upgradeType)
        {
            string info = "";

            foreach (var entity in _entities)
            {
                info += $" . {entity.UpgradeType} . ";
            }

            Debug.Log(new StringBuilder()
                .Append("Finding existing type")
                .Append(info)
                .Append("finding type - ")
                .Append(upgradeType)
                .Append(" entities list length ")
                .Append(_entities.Count)
            );
        }
    }
}
