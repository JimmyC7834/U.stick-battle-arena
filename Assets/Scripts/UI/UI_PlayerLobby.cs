using System;
using UnityEngine;

namespace Game.UI
{
    public class UI_PlayerLobby : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private GameService _gameService;
        [SerializeField] private UI_PlayerLobbyPanel[] _playerPanels;

        private void Start()
        {
            for (int i = 0; i < _playerPanels.Length; i++)
                _playerPanels[i].OnReady += SetGameplaySetting;
        }

        private void SetGameplaySetting(PlayerReadyInfo info)
        {
            _gameSettings.SetPlayerSettings(info);
        }

        public void ReturnToMainMenu()
        {
            _gameService.SceneManager.LoadScene(SceneID.MainMenu);
        }
        
        public void GotoGameplaySetting()
        {
            _gameService.SceneManager.LoadScene(SceneID.MainMenu);
        }
    }
}