#region

using System;
using UnityEngine;

#endregion

namespace Game
{
    public class UI_MainMenu : MonoBehaviour
    {
        [SerializeField] private GameService _gameService;
        [SerializeField] private SceneID _playGameButtonScene;

        private void Start()
        {
            _gameService.AudioManager.PlayAudio(AudioID.MainMenu);
        }

        public void PlayGame() 
        {
            _gameService.AudioManager.PlayAudio(AudioID.Click);
            _gameService.AudioManager.StopAudio(AudioID.MainMenu);
            _gameService.SceneManager.LoadScene(_playGameButtonScene);
        }

        public void QuitGame()
        {
            _gameService.AudioManager.PlayAudio(AudioID.Return);
            _gameService.AudioManager.StopAudio(AudioID.MainMenu);
            _gameService.SceneManager.ExitGame();
        }
    }
}