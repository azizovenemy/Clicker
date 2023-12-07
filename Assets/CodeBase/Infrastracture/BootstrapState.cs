using System;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public class BootstrapState : IState
    {
        private const string Bootstrap = "Bootstrap";
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
            _sceneLoader.Load(Bootstrap, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("MainScene");

        private void RegisterServices() => 
            Debug.Log("Register Services from BootstrapState");

        public void Exit() { }
    }
}
