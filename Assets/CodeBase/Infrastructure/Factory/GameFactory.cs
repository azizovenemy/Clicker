using System.Collections.Generic;
using CodeBase.UserInfo;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();

        private Balance _balance;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public void InitSpawners()
        {
            var sceneKey = SceneManager.GetActiveScene().name;
            LevelData levelData = _staticData.ForLevel(sceneKey);
            foreach (EnemySpawnerData enemySpawner in levelData.EnemySpawners)
            {
                CreateSpawner(enemySpawner.Position);
            }
        }

        public GameObject InitEnemy(EEnemyTypeId typeId, Transform parent, int index)
        {
            EnemyData enemyData = _staticData.ForEnemy(typeId);
            GameObject enemyObject = Object.Instantiate(enemyData.Prefab, parent.transform.position.AddY(0.5f), Quaternion.identity, parent);

            var health = enemyObject.GetComponent<EnemyHealth>();
            enemyObject.GetComponentInChildren<ActorUI>().Construct(health, enemyData.Name);
            enemyObject.GetComponent<EnemyDeath>().deathFx = enemyData.DeathFx;

            var enemy = enemyObject.GetComponent<BaseEnemy>();
            health.Max = enemy.CalculateMaxHealth(index);
            health.Current = health.Max;
            
            return enemyObject;
        }

        public void InitPlayer()
        {
            var player = InstantiateRegistered(Constants.PlayerPath);

            player.GetComponent<Player>().Construct(_balance);
        }

        public void InitHUD()
        {
            var hud = InstantiateRegistered(Constants.UIPath);

            _balance = hud.GetComponentInChildren<Balance>();

            BindCameraStack(hud);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void CreateSpawner(Vector3 at)
        {
            var spawner = InstantiateRegistered(Constants.SpawnerPath, at);
            spawner.GetComponent<EnemySpawner>().Construct(this);
        }

        private GameObject InstantiateRegistered(string path)
        {
            var gameObject = _assets.Instantiate(path);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(string path, Vector3 at)
        {
            var gameObject = _assets.Instantiate(path, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject player)
        {
            foreach (ISavedProgressReader progressReader in player.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }
        
        private void BindCameraStack(GameObject uiPrefab) => 
            Camera.main.GetUniversalAdditionalCameraData().cameraStack.Add(uiPrefab.GetComponentInChildren<Camera>());
    }
}