using System;
using System.Text;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(BaseEnemy))]
    public class EnemyHealth : MonoBehaviour
    {
        public event Action OnHealthChanged;

        public float Current;
        public float Max;

        [SerializeField] private BaseEnemy baseEnemy;

        public void TakeDamage(float damage)
        {
            if (Current <= 0) return;

            Current -= damage;
            OnHealthChanged?.Invoke();
        }
    }
}