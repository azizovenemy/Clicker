using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Settings
{
    public class SettingsUI : MonoBehaviour
    {
        public event Action OnRemoveData;
        
        [SerializeField] private Button saveProgressButton;
        [SerializeField] private Button clearProgressButton;
        [SerializeField] private Button closeGameButton;

        [SerializeField] private AudioClip someSound;
        
        private ISaveLoadService _saveLoadService;
        
        private void OnEnable()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            
            saveProgressButton.onClick.AddListener(SaveProgress);
            clearProgressButton.onClick.AddListener(ClearProgress);
            closeGameButton.onClick.AddListener(Close);
        }

        private void SaveProgress()
        {
            _saveLoadService.SaveProgress();
            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = someSound;
            audioSource.Play(0);
        }

        private void ClearProgress()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
                
            OnRemoveData?.Invoke();
        }

        private void Close() => 
            Application.Quit();

        private void OnApplicationQuit() => 
            _saveLoadService.SaveProgress();
    }
}
