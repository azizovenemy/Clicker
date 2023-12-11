using CodeBase.Logic.Settings;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public GameLoopState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter()
        {
            var settings = Object.FindObjectOfType<SettingsUI>();

            settings.OnRemoveData += LoadBootstrapperState;
        }

        private void LoadBootstrapperState()
        {
            _curtain.Show();
            _sceneLoader.Load(Constants.BootstrapSceneName, onLoaded: OnLoaded);
        }

        private void OnLoaded() 
            => _curtain.Hide();

        public void Exit() { }
    }
}