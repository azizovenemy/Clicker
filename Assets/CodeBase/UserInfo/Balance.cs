using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using TMPro;
using UnityEngine;

namespace CodeBase.UserInfo
{
    public class Balance : MonoBehaviour, ISavedProgress
    {
        public static Balance Instance => _instance;
        private static Balance _instance;

        [SerializeField] private TMP_Text balanceText;
        
        private double _balance;

        public void IncreaseBalance(float money)
        {
            if (money <= 0) return;
            
            _balance += money;
            UpdateUI();
        }

        public void DecreaseBalance(float money)
        {
            if (_balance < 0 || !IsEnough(money)) return;

            _balance = Mathf.Clamp((float)(_balance - money), 0, Mathf.Infinity);
            UpdateUI();
        }

        public bool IsEnough(float money) => 
            _balance >= money;
        
        public void LoadProgress(PlayerProgress progress)
        {
            _balance = progress.playerData.balance;
            UpdateUI();
            CreateSingleton();
        }

        public void UpdateProgress(PlayerProgress progress) => 
            progress.playerData.balance = (float)_balance;

        private void UpdateUI() => 
            balanceText.text = $"{Constants.FormatString(_balance)} $";

        private void CreateSingleton()
        {
            //minus vaibik
            _instance = this;
        }
    }
}