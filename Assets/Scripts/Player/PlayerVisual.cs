using Game.UI;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerStat))]
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;

        [SerializeField] private SpriteRenderer _accessoryDisplay;
        [SerializeField] private SpriteRenderer _playerDisplay;

        private void Start()
        {
            PlayerReadyInfo info = _gameSettings.GetPlayerSettings(GetComponent<PlayerStat>().ID);
            _accessoryDisplay.sprite = info.Accessory;
            _playerDisplay.color = info.Color;
        }
    }
}