#region

using UnityEngine;

#endregion

namespace Game.UI
{
    public class UI_PlayerLobby : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private GameService _gameService;
        [SerializeField] private UI_PlayerLobbyPanel[] _playerPanels;
        private int _playerCount = 2;

        private void Start()
        {
            for (int i = 0; i < _playerPanels.Length; i++)
            {
                _playerPanels[i].OnReady += SetGameplaySetting;
                _playerPanels[i].OnJoin += AddPlayerCount;
            }
            _gameSettings.SetPlayerCount(GameSettingsSO.MIN_PLAYER_COUNT);
        }

        private void CheckGameStart()
        {
            if (AllReady())
            {
                _gameSettings.SetStageID(SceneID.Farm);
                _gameService.GameStart();
            }
        }

        private bool AllReady()
        {
            for (int i = 0; i < _playerPanels.Length; i++)
            {
                if (!_playerPanels[i].Joined) continue;
                if (!_playerPanels[i].IsReady) return false;
            }

            return true;
        }
        
        private void AddPlayerCount()
        {
            if (_playerCount == 4) return;
            _playerCount++;
            _gameSettings.SetPlayerCount(_playerCount);
        }

        private void SetGameplaySetting(PlayerReadyInfo info)
        {
            _gameSettings.SetPlayerSettings(info);
            CheckGameStart();
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