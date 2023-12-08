using System;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Constants.BootstrapSceneName, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(Constants.MainSceneName);

        private void RegisterServices() => 
            Debug.Log("Register Services from BootstrapState");

        public void Exit() { }
    }
}
