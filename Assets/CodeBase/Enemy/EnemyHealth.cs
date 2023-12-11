using System;
using System.Text;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(BaseEnemy))]
    public class EnemyHealth : MonoBehaviour, ISavedProgress
    {
        public event Action OnHealthChanged;

        public float Current;
        public float Max;

        [SerializeField] private BaseEnemy baseEnemy;

        private CurrentEnemyData _data;

        public void LoadProgress(PlayerProgress progress)
        {
            _data = progress.currentEnemyData;

            DebugLoadedData();

            Max = baseEnemy.CalculateMaxHealth(_data.index - 1);
            Current = _data.currentHp;
            OnHealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.currentEnemyData.currentHp = Current;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0) return;

            Current -= damage;
            OnHealthChanged?.Invoke();
        }

        private void DebugLoadedData()
        {
            Debug.Log(new StringBuilder()
                .Append("[EnemyData] Index : ")
                .Append(_data.index)
                .Append(", Current Health : ")
                .Append(_data.currentHp)
                .ToString());
        }
    }
}