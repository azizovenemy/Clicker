using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain Curtain;

        private Game _game;

        void Awake()
        {
            _game = new Game(this, Curtain);
            _game.stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
