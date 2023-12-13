using System;
using System.Text;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Upgrades;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UserInfo
{
    public class Player : MonoBehaviour, ISavedProgress
    {
        private Camera _camera;
        private PlayerData _playerProgress;
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

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.playerData = _playerProgress;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerProgress = progress.playerData;
            //DebugInfo();
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
            
            _enemy.TakeDamage(_playerProgress.damage);
        }

        private void DamageRepeating()
        {
            if (_enemy && Upgrades.Instance.FindExists(EUpgradeTypeId.AutoDamageIncrease))
            {
                _time += Time.deltaTime;
                while(_time >= 1.0f) {
                    _enemy.TakeDamage(Upgrades.Instance.GetUpgradeCount(EUpgradeTypeId.AutoDamageIncrease));
                
                    _time -= 1.0f;
                }
            }
        }

        private void DebugInfo()
        {
            Debug.Log(new StringBuilder()
                .Append("Player data loaded with balance : ")
                .Append(_playerProgress.balance)
                .Append(" with damage : ")
                .Append(_playerProgress.damage)
                .ToString());
        }
    }
}