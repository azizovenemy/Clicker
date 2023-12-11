using System;
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
        private PlayerData _progress;

        private Dictionary<EUpgradeTypeId, int> _upgrades;
        private readonly List<UpgradeEntity> _entities = new();
        
        private void InstantiateUpgrades()
        {
            foreach (var field in _progress.upgradesData)
                InstantiateUpgrade(Constants.UpgradeEntityPath, field.upgradeType, field.count);
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

            DebugDictionaryInfo();
        }

        private void DebugDictionaryInfo()
        {
            foreach (var field in _progress.upgradesData)
            {
                Debug.Log($"{field.upgradeType} - {field.count}");
            }
        }
    }
}
