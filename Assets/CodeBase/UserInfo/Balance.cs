using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using TMPro;
using UnityEngine;

namespace CodeBase.UserInfo
{
    public class Balance : MonoBehaviour, ISavedProgress
    {
        public float balance;

        [SerializeField] private TMP_Text balanceText;
        
        private void UpdateUI()
        {
            balanceText.text = $"Balance : {balance}";            
        }

        public void LoadProgress(PlayerProgress progress)
        {
            balance = progress.playerData.balance;
            UpdateUI();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.playerData.balance = balance;
        }
    }
}