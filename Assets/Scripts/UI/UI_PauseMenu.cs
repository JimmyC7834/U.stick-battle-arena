using UnityEngine;
using UnityEngine.UI;

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

        private void ReturnToMainMenu()
        {
            _gameService.SceneManager.LoadScene(SceneID.MainMenu);
        }

        private void HandleEsc()
        {
            if (!isActiveAndEnabled)
            {
                OpenPauseMenu();
                return;
            }
            
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