using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator Container;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            Container = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.BootstrapSceneName, onLoaded: EnterLoadLevel);
        }

        public void Exit() { }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<DataLoadState>();

        private void RegisterServices()
        {
            RegisterStaticDataService();
            Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            Container.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            Container.RegisterSingle<IGameFactory>(new GameFactory(Container.Single<IAssetProvider>(), Container.Single<IStaticDataService>()));
            Container.RegisterSingle<ISaveLoadService>(new SaveLoadService(Container.Single<IPersistentProgressService>(), Container.Single<IGameFactory>()));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadEnemies();
            Container.RegisterSingle(staticDataService);
        }
    }
}
