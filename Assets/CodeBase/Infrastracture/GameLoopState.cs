namespace CodeBase.Infrastracture
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private UI _ui;

        public GameLoopState(GameStateMachine stateMachine, UI hud)
        {
            _stateMachine = stateMachine;
            _ui = hud;
        }

        public void Enter() => 
            _ui.Show(_ui.HUDCanvas);

        public void Exit() { }
    }
}