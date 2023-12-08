using System.Linq.Expressions;
using Unity.VisualScripting;

namespace CodeBase.Infrastracture
{
    public class Game
    {
        public GameStateMachine stateMachine;

        public Game(ICoroutineRunner coroutineRunner, UI ui)
        {
            stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), ui);
        }
    }
}