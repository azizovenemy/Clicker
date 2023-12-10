using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}