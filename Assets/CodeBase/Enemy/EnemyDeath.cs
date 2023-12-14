using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        public event Action Happened;

        [HideInInspector] public GameObject deathFx;

        [SerializeField] private List<SkinnedMeshRenderer> meshRenderers;
        [SerializeField] private Material deathMaterial;
        [SerializeField] private GameObject canvas;
        [SerializeField] private EnemyHealth health;

        private void Start()
        {
            health.OnHealthChanged += HealthChanged;
        }

        private void OnDisable() => 
            health.OnHealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if(health.Current <= 0)
                OnDeath();
        }

        private void OnDeath()
        {
            health.OnHealthChanged -= HealthChanged;
            canvas.SetActive(false);
            SetMeshTransparent();
            SpawnDeathFX();
            StartCoroutine(DestroyRoutine());
        }

        private void SpawnDeathFX()
        {
            var parentTransform = transform;
            var go = Instantiate(deathFx, parentTransform.position, Quaternion.identity, parentTransform);
            go.GetComponent<ParticleSystem>().Play();
        }

        private IEnumerator DestroyRoutine()
        {
            yield return new WaitForSeconds(1.2f);
            Happened?.Invoke();
            Destroy(gameObject);
        }

        private void SetMeshTransparent()
        {
            foreach (var meshRenderer in meshRenderers)
                meshRenderer.material = deathMaterial;
        }
    }
}