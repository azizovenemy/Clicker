using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Logic.Settings
{
    public class SettingsUI : MonoBehaviour
    {
        public event Action OnRemoveData;
        
        [SerializeField] private Button saveProgressButton;
        [SerializeField] private Button clearProgressButton;
        [SerializeField] private Button listenOppenheimer;
        
        private ISaveLoadService _saveLoadService;
        
        private void OnEnable()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            
            saveProgressButton.onClick.AddListener(SaveProgress);
            clearProgressButton.onClick.AddListener(ClearProgress);
            listenOppenheimer.onClick.AddListener(Oppenheimer);
        }

        private void SaveProgress()
        {
            _saveLoadService.SaveProgress();
        }

        private void ClearProgress()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
                
            OnRemoveData?.Invoke();
        }

        private void Oppenheimer()
        {
            Debug.Log("listening");
        }
    }
}
