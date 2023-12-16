using System;
using CodeBase.UI;
using CodeBase.UserInfo;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(BaseEnemy))]
    public class EnemyHealth : MonoBehaviour
    {
        public event Action OnHealthChanged;

        public float Current { get; set; }
        public float Max { get; set; }

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

            widget.SetValue(Constants.FormatString(damage)); 
            widget.SetColor(widgetColor);
        }
    }
}