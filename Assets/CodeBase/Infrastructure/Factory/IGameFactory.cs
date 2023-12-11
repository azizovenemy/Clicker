using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        
        void InitSpawners();
        void InitPlayer();
        GameObject InitEnemy(EEnemyTypeId typeId, Transform parent, int index);
        void InitHUD();
        
        void Cleanup();
    }
}