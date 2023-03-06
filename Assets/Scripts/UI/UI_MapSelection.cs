#region

using UnityEngine;

#endregion

namespace Game
{
    public class UI_MapSelection : MonoBehaviour
    {
        [SerializeField] private GameService _gameService;
        [SerializeField] private GameSettingsSO _gameSettings;
        
        public void LoadMap1()
        {
            _gameService.AudioManager.PlayAudio(AudioID.Click1);
            _gameSettings.SetGameplayStageID(SceneID.Farm);
            _gameService.SceneManager.LoadScene(_gameSettings.GameplayStageID);
        }
        
        public void LoadMap2()
        {
            _gameService.AudioManager.PlayAudio(AudioID.Click1);
            _gameSettings.SetGameplayStageID(SceneID.Farm);
            _gameService.SceneManager.LoadScene(_gameSettings.GameplayStageID);
        }
    }
}