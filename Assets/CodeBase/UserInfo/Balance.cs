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
        private readonly string[] _reductions = { "", "K", "M", "B", "T" };

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

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.playerData.balance = (float)_balance;
        }

        private void UpdateUI() => 
            balanceText.text = $"{FormatBalance(_balance)} $";

        private string FormatBalance(double num)
        {
            if (num == 0) return "0";

            num = Math.Round((float) num);

            int i = 0;

            while(i + 1 < _reductions.Length && num >= 1000d)
            {
                num /= 1000d;
                i++;
            }

            return num.ToString(format: "#.##") + _reductions[i];
        }

        private void CreateSingleton()
        {
            //minus vaibik
            _instance = this;
        }
    }
}