using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public UI UI;

        private Game _game;

        void Awake()
        {
            _game = new Game(this, UI);
            _game.stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
