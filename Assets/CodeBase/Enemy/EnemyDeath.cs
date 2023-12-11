using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        public event Action Happened;

        public GameObject deathFx;
        
        [SerializeField] private EnemyHealth health;

        private void Start()
        {
            health.OnHealthChanged += HealthChanged;
        }

        private void OnDestroy() => 
            health.OnHealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if(health.Current <= 0)
                OnDeath();
        }

        private void OnDeath()
        {
            health.OnHealthChanged -= HealthChanged;
            
            //SpawnDeathFX();
            StartCoroutine(DestroyRoutine());
        }

        private void SpawnDeathFX()
        {
            GameObject vfx = Instantiate(deathFx, transform.position, Quaternion.identity);
            vfx.GetComponent<ParticleSystem>().Play();
        }

        private IEnumerator DestroyRoutine()
        {
            yield return new WaitForSeconds(3);
            Happened?.Invoke();
            Destroy(gameObject);
        }
    }
}