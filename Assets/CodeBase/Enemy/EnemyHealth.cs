using System;
using System.Globalization;
using CodeBase.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(BaseEnemy))]
    public class EnemyHealth : MonoBehaviour
    {
        public event Action OnHealthChanged;

        [HideInInspector] public float Current;
        [HideInInspector] public float Max;

        [SerializeField] private Color widgetColor;
        [SerializeField] private WidgetDamageValue widgetPrefab;
        [SerializeField] private Transform damageValuesContainer;

        public void TakeDamage(float damage)
        {
            if (Current <= 0) return;

            Current -= damage;
            CreateWidgetDamageValue(damage);
            OnHealthChanged?.Invoke();
        }

        private void CreateWidgetDamageValue(float damage)
        {
            if(damage <= 0) return;

            var widget = Instantiate(widgetPrefab, damageValuesContainer);
            var maxDistance = 0.8f;
            var randomOffset = Random.insideUnitCircle * maxDistance;
            var position = damageValuesContainer.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
            widget.transform.position = position;

            widget.SetValue($"{damage}"); 
            widget.SetColor(widgetColor);
        }
    }
}