using System;
using System.Text;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.PersistentProgress;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Player
{
    public class Player : MonoBehaviour, ISavedProgress
    {
        private int _levelIndex;
        private float _balance;
        private float _currentEnemyHp;

        private Camera _camera;
        private PlayerData _playerProgress;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 100)) return;

            hit.transform.TryGetComponent<EnemyHealth>(out var component);
            if (!component) return;
            
            component.TakeDamage(_playerProgress.damage);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.playerData.balance = 100;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _playerProgress = progress.playerData;
            //DebugInfo();
        }

        private void DebugInfo()
        {
            Debug.Log(new StringBuilder().Append("Player data loaded with balance : ")
                .Append(_playerProgress.balance)
                .Append(" with damage : ")
                .Append(_playerProgress.damage)
                .ToString());
        }
    }
}