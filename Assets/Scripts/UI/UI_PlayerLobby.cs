using System;
using UnityEngine;

namespace Game.UI
{
    public class UI_PlayerLobby : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;
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
    }
}