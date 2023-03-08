#region

using Game.UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

#endregion

namespace Game
{
    public class UI_MapSelection : MonoBehaviour
    {
        [SerializeField] private GameService _gameService;
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private SceneID _previousScene;
        [SerializeField] private UI_MapButton[] _mapButtons;
        [SerializeField] private Image _background;

        private void Start()
        {
            for (int i = 0; i < _mapButtons.Length; i++)
            {
                UI_MapButton button = _mapButtons[i];
                button.Button.onClick.AddListener(() => SetStage(button));
            }
            
            if (!_gameService.AudioManager.AudioIsPlaying(AudioID.Menus))
            {
                _gameService.AudioManager.PlayAudio(AudioID.Menus);
            }

            RandomSelect();
        }

        private void SetStage(UI_MapButton button)
        {
            _gameService.AudioManager.PlayAudio(AudioID.Click);
            _gameSettings.SetGameplayStageID(button.SceneID);
            _background.sprite = button.PreviewImage;
        }
        

        public void RandomSelect()
        {
            _gameService.AudioManager.PlayAudio(AudioID.Click);
            SetStage(_mapButtons[Random.Range(0, _mapButtons.Length)]);
        }
        
        public void GameStart()
        {
            _gameService.AudioManager.PlayAudio(AudioID.Click);
            _gameService.AudioManager.StopAudio(AudioID.Menus);
            _gameService.GameStart();
        }

        public void BackToPrevious()
        {
            _gameService.AudioManager.PlayAudio(AudioID.Return);
            _gameService.SceneManager.LoadScene(_previousScene);
        }
    }
}