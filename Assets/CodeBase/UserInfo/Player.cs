using CodeBase.Enemy;
using CodeBase.Logic.Upgrades;
using UnityEngine;

namespace CodeBase.UserInfo
{
    public class Player : MonoBehaviour
    {
        private Camera _camera;
        private EnemyHealth _enemy;
        private EnemySpawner _spawner;
        private float _time;

        public void Construct(EnemySpawner spawner)
        {
            _spawner = spawner;
            _enemy = _spawner.CurrentEnemy.GetComponent<EnemyHealth>();
        }

        private void Awake()
        {
            _camera = Camera.main;
            _time = 0f;
        }

        private void Update()
        {
            DamageRepeating();

            ApplyDamageOnClick();
        }

        private void ApplyDamageOnClick()
        {
            if (!_enemy)
                _enemy = _spawner.CurrentEnemy.GetComponent<EnemyHealth>();

            if (!Input.GetMouseButtonDown(0)) return;

            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 100)) return;

            hit.transform.TryGetComponent<EnemyHealth>(out var component);
            if (!component) return;

            _enemy.TakeDamage(1 + Upgrades.Instance.CalculateDamagePerClick());
        }

        private void DamageRepeating()
        {
            if (!_enemy) return;

            _time += Time.deltaTime;
            while (_time >= 1.0f)
            {
                _enemy.TakeDamage(Upgrades.Instance.CalculateDamagePerSecond());

                _time -= 1.0f;
            }
        }
    }
}