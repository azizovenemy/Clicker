using UnityEngine;

namespace CodeBase.Infrastracture
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly UI _ui;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, UI ui)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _ui = ui;
        }

        public void Enter(string sceneName)
        {
            _ui.Show(_ui.LoadingCanvas);
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        public void Exit() =>
            _ui.Hide();

        private void OnLoaded()
        {
            Debug.Log("Level loaded");
            _stateMachine.Enter<GameLoopState>();
        }
    }
}