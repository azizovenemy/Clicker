using CodeBase.StaticData;
using CodeBase.UserInfo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Upgrades
{
    public class UpgradeEntity : MonoBehaviour
    {
        public EUpgradeTypeId UpgradeType;
        public int Count;

        [SerializeField] private float startCost;
        
        [SerializeField] private Image mainImage;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text countText;
        
        [SerializeField] private Button buyButton;
        [SerializeField] private Button sellButton;

        [SerializeField] private AudioClip buySound;
        [SerializeField] private AudioClip sellSound;
        
        private float _currentBuyCost;
        private float _currentSellCost;

        public void Construct(EUpgradeTypeId upgradeType, int count, Sprite sprite)
        {
            UpgradeType = upgradeType;
            Count = count;
            mainImage.sprite = sprite;
            
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

        public bool GetCount(EUpgradeTypeId upgradeTypeId, out int upgradesCount)
        {
            upgradesCount = Count;
            return UpgradeType == upgradeTypeId;
        }

        public bool IsExists(EUpgradeTypeId upgradeType) =>
            UpgradeType == upgradeType;

        private void OnSell()
        {
            if (Count <= 0) return;

            Balance.Instance.IncreaseBalance(_currentSellCost);
            Count--;
            CalculateCost();
            PlaySound(sellSound);
            UpdateUI();
        }

        private void OnBuy()
        {
            if (!Balance.Instance.IsEnough(_currentBuyCost)) return;
            
            Balance.Instance.DecreaseBalance(_currentBuyCost);
            Count++;
            CalculateCost();
            PlaySound(buySound);
            UpdateUI();
        }

        private void PlaySound(AudioClip audioClip)
        {
            var source = GetComponent<AudioSource>();
            source.clip = audioClip;
            source.Play(0);
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