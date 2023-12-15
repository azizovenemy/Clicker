using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Settings
{
    public class SettingsUI : MonoBehaviour, ISavedProgress
    {
        public event Action OnRemoveData;
        
        [SerializeField] private Button saveProgressButton;
        [SerializeField] private Button clearProgressButton;
        [SerializeField] private Button changeSoundMute;
        [SerializeField] private Button closeGameButton;

        [SerializeField] private Sprite soundSprite;
        [SerializeField] private Sprite muteSprite;

        [SerializeField] private AudioClip someSound;

        private bool _mute;
        private ISaveLoadService _saveLoadService;
        
        public void UpdateProgress(PlayerProgress progress) => 
            progress.playerData.mute = _mute;

        public void LoadProgress(PlayerProgress progress)
        {
            _mute = progress.playerData.mute;
            ChangeSoundMuteUI();
        } 

        private void OnEnable()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            
            saveProgressButton.onClick.AddListener(SaveProgress);
            clearProgressButton.onClick.AddListener(ClearProgress);
            changeSoundMute.onClick.AddListener(ChangeSoundMute);
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

        private void ChangeSoundMute()
        {
            _mute = !_mute;
            ChangeSoundMuteUI();
        }

        private void ChangeSoundMuteUI()
        {
            Debug.Log($"sound state mute : {_mute}");
            changeSoundMute.GetComponent<Image>().sprite = _mute ? soundSprite : muteSprite;
            Camera.main.GetComponent<AudioListener>().enabled = _mute;
        }

        private void Close() => 
            Application.Quit();

        private void OnApplicationQuit() => 
            _saveLoadService.SaveProgress();

        private void OnDisable()
        {
            saveProgressButton.onClick.RemoveListener(SaveProgress);
            clearProgressButton.onClick.RemoveListener(ClearProgress);
            changeSoundMute.onClick.RemoveListener(ChangeSoundMute);
            closeGameButton.onClick.RemoveListener(Close);
        }
    }
}
