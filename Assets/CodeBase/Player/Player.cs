using System;
using System.Text;
using CodeBase.Data;
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

        private SaveType _saveType;

        private void Awake()
        {
            _saveType = new SaveType();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _saveType.SaveProgress();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.CurrentEnemyData.Index = 1;
            progress.CurrentEnemyData.CurrentHP = 10;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _levelIndex = progress.CurrentEnemyData.Index;
            _currentEnemyHp = progress.CurrentEnemyData.CurrentHP;
            _balance = progress.PlayerData.Balance;
            Debug.Log(new StringBuilder().Append("Data loaded with balance : ")
                .Append(_balance)
                .Append(" and level : ")
                .Append(_levelIndex)
                .Append(" current Enemy health : ")
                .Append(_currentEnemyHp)
                .ToString());
        }

        //private void OnApplicationQuit()
        //{
        //    _saveType.SaveProgress();
        //}
    }
}