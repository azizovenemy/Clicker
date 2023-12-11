using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private NameUI nameText;

        private EnemyHealth _enemyHealth;

        public void Construct(EnemyHealth health, string newName)
        {
            _enemyHealth = health;
            nameText.UpdateName(newName);
            _enemyHealth.OnHealthChanged += UpdateHealth;
        }

        private void OnDestroy() => 
            _enemyHealth.OnHealthChanged -= UpdateHealth;

        private void UpdateHealth() => 
            healthBar.SetValue(_enemyHealth.Current, _enemyHealth.Max);
    }
}