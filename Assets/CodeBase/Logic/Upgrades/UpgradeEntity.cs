using System;
using CodeBase.StaticData;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace CodeBase.Logic.Upgrades
{
    public class UpgradeEntity : MonoBehaviour
    {
        public EUpgradeTypeId UpgradeType;
        public int Count;

        [SerializeField] private float startCost;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text countText;
        
        [SerializeField] private Button buyButton;
        [SerializeField] private Button sellButton;

        private float _currentBuyCost;
        private float _currentSellCost;

        public void Construct(EUpgradeTypeId upgradeType, int count)
        {
            UpgradeType = upgradeType;
            Count = count;
            
            CalculateCost();
            UpdateUI();
        }
        
        private void OnEnable()
        {
            buyButton.onClick.AddListener(OnBuy);
            sellButton.onClick.AddListener(OnSell);
        }

        private void OnDestroy()
        {
            buyButton.onClick.RemoveListener(OnBuy);
            sellButton.onClick.RemoveListener(OnSell);
        }

        private void OnSell()
        {
            if (Count <= 0) return;
            
            Count--;
            CalculateCost();
            UpdateUI();
        }

        private void OnBuy()
        {
            Count++;
            CalculateCost();
            UpdateUI();
        }

        private void CalculateCost()
        {
            CalculateBuyCost();
            CalculateSellCost();
        }

        private void UpdateUI()
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = $"Buy: {_currentBuyCost}";
            sellButton.GetComponentInChildren<TMP_Text>().text = $"Sell: {_currentSellCost}";
            titleText.text = $"{UpgradeType}";
            countText.text = Count.ToString();
        }
        
        private void CalculateBuyCost() => 
            _currentBuyCost = Count != 0 ? Mathf.FloorToInt(Count * Constants.Increase * 1.36f) : startCost;

        private void CalculateSellCost() => 
            _currentSellCost = Mathf.FloorToInt(_currentBuyCost / 1.5f);
    }
}