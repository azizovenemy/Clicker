using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper bootstrapperPrefab;
        
        private void OnEnable()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            var ui = GameObject.Find("INTERFACE");
            var curtain = FindObjectOfType<LoadingCurtain>();

            if (bootstrapper && ui && curtain)
            {
                Destroy(ui);
                Destroy(curtain);
                Destroy(bootstrapper);
            }

            Instantiate(bootstrapperPrefab);
        }
    }
}