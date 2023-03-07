#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Game.UI
{
    public class UI_PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameService _gameService;
        [SerializeField] private GameplayService _gameplayService;
        [SerializeField] private GameInputReader _gameInputReader;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _returnToMainMenuButton;

        private void Awake()
        {
            _gameInputReader.escEvent += HandleEsc;
            
            _continueButton.onClick.AddListener(ClosePauseMenu);
            _returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _gameInputReader.escEvent -= HandleEsc;
        }

        private void ReturnToMainMenu()
        {
            Time.timeScale = 1;
            _gameService.AudioManager.PlayAudio(AudioID.Return);
            _gameService.SceneManager.LoadScene(SceneID.MainMenu);
        }

        private void HandleEsc()
        {
            if (!isActiveAndEnabled)
            {
                _gameService.AudioManager.PlayAudio(AudioID.Pause);
                OpenPauseMenu();
                return;
            }
            
            _gameService.AudioManager.PlayAudio(AudioID.Pause);
            ClosePauseMenu();
        }

        private void OpenPauseMenu()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            _gameplayService.PauseGameplay();
        }
        
        private void ClosePauseMenu()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            _gameplayService.ContinueGameplay();
        }
    }
}