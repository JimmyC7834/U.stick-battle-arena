#region

using System.Linq;
using Game.DataSet;
using Game.UI;
using Unity.VisualScripting;
using UnityEngine;

#endregion

namespace Game
{
    /**
     * Contains settings for gameplay
     */
    [CreateAssetMenu]
    public class GameSettingsSO : ScriptableObject
    {
        public static readonly int MIN_PLAYER_COUNT = 2;
        public static readonly int MAX_PLAYER_COUNT = 4;
        public static readonly PlayerID[] PLAYER_IDS =
        {
            PlayerID.Player1, 
            PlayerID.Player2,
            PlayerID.Player3,
            PlayerID.Player4,
        };

        public int PlayerCount { get; private set; } = MIN_PLAYER_COUNT;
        public int PlayerLifeCount { get; private set; } = 5;
        public SceneID GameplayStageID { get; private set; }
        public (PlayerReadyInfo, int) WinnerInfo { get; private set; }
        public GameModeID GameModeID { get; private set; }
        public static readonly SceneID[] NON_STAGE_IDS =
        {
            SceneID.MainMenu,
            SceneID.PlayerLobby,
            SceneID.MapSelectionMenu
        };

        private FlexibleDictionary<PlayerID, PlayerReadyInfo> _playerSettings;

        /**
         * Check if the given player id is in the gameplay
         * return true if the given id is in the gameplay, false otherwise
         */
        public bool PlayerIDInGameplay(PlayerID id)
        {
            if (id == PlayerID.Player3 && PlayerCount == MIN_PLAYER_COUNT) return false;
            if (id == PlayerID.Player4 && PlayerCount != MAX_PLAYER_COUNT) return false;
            return true;
        }
        
        public void SetPlayerCount(int value)
        {
            if (value < MIN_PLAYER_COUNT || value > MAX_PLAYER_COUNT)
            {
                Debug.LogError($"Trying to set invalid player count: {value}!");
                return;
            }

            PlayerCount = value;
        }

        public void SetPlayerLifeCount(int val)
        {
            PlayerLifeCount = val;
        }
        
        public void SetGameMode(GameModeID id)
        {
            GameModeID = id;
        }
        
        public void SetGameplayStageID(SceneID id)
        {
            if (NON_STAGE_IDS.Contains(id)) return;
            GameplayStageID = id;
        }

        public void SetPlayerSettings(PlayerReadyInfo info)
        {
            if (_playerSettings == null)
                _playerSettings = new FlexibleDictionary<PlayerID, PlayerReadyInfo>();

            _playerSettings[info.PlayerID] = info;
        }
        
        public void SetWinner(PlayerID id, int score)
        {
            WinnerInfo = (_playerSettings[id], score);
        }
        
        public PlayerReadyInfo GetPlayerSettings(PlayerID id)
        {
            if (!PlayerIDInGameplay(id)) return _playerSettings[0];
            return _playerSettings[id];
        }
    }
}