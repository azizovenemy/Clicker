namespace CodeBase.Infrastracture
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}