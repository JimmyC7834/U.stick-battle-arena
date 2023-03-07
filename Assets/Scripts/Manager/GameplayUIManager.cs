#region

using Game.UI;
using TMPro;
using UnityEngine;

#endregion

namespace Game
{
    public class GameplayUIManager : MonoBehaviour
    {
        [SerializeField] private UI_PlayerStatusPanel _playerStatusPanel;
        [SerializeField] private UI_WinningScreen _winningScreen;

        /**
         * Initialize gameplay UI elements,
         * should ONLY be called by GameplayManager
         */
        public void Initialize()
        {
            _playerStatusPanel.Initialize();
        }

        /**
         * Show the winning text for the player to screen
         * Temporary function, should be modified once the winning effect is decided 
         */
        public void ShowWinningScreen()
        {
            _winningScreen.gameObject.SetActive(true);
        }
    }
}