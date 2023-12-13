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
        public static Upgrades Instance
        {
            get
            {
                if (_instance != null) return _instance;
                GameObject go = new();
                return _instance = go.AddComponent<Upgrades>();
            }
        }
        private static Upgrades _instance;

        private PlayerData _progress;

        private readonly List<UpgradeEntity> _entities = new List<UpgradeEntity>();

        private void InstantiateUpgrades()
        {
            foreach (var upgrade in _progress.upgradesData)
            {
                InstantiateUpgrade(Constants.UpgradeEntityPath, upgrade.upgradeType, upgrade.count);
            }
            Debug.Log(_entities.Count);
        }

        private void InstantiateUpgrade(string path, EUpgradeTypeId upgradeType, int upgradeCount)
        {
            var upgradeEntity = Instantiate(Resources.Load<UpgradeEntity>(path), this.transform);
            upgradeEntity.Construct(upgradeType, upgradeCount);
            
            _entities.Add(upgradeEntity);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress.playerData;
            InstantiateUpgrades();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            UpdateDictionary();
            progress.playerData = _progress;
        }

        private void UpdateDictionary()
        {
            _progress.upgradesData.Clear();
            
            foreach (var entity in _entities)
                _progress.upgradesData.Add(new UpgradesData(entity.UpgradeType, entity.Count));

            //DebugDictionaryInfo();
        }

        private void DebugDictionaryInfo()
        {
            foreach (var field in _progress.upgradesData)
            {
                Debug.Log($"{field.upgradeType} - {field.count}");
            }
        }
        
        public bool FindExists(EUpgradeTypeId upgradeType)
        {
            //DebugAll(upgradeType.ToString());
            foreach (UpgradeEntity upgrade in _entities)
            {
                if (upgrade.IsExists(upgradeType))
                {
                    return true;
                }
            }

            return false;
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
    }
}
