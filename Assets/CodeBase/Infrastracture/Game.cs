using System.Linq.Expressions;
using Unity.VisualScripting;

namespace CodeBase.Infrastracture
{
    public class Game
    {
        public GameStateMachine stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}