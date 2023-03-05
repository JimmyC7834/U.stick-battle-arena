using System;
using Game.UI;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerStat), typeof(PlayerMovement))]
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;

        [SerializeField] private SpriteRenderer _accessoryDisplay;
        [SerializeField] private SpriteRenderer _playerDisplay;
        [SerializeField] private ParticleSystem _playerDust;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerMovement.OnMovement += HandleMovementVisual;
            _playerDust.Stop();
        }

        private void Start()
        {
            PlayerReadyInfo info = _gameSettings.GetPlayerSettings(GetComponent<PlayerStat>().ID);
            _accessoryDisplay.sprite = info.Accessory;
            _playerDisplay.color = info.Color;
        }

        private void HandleMovementVisual(Vector2 vec)
        {
            if (!_playerMovement.IsJumping && vec.x != 0)
            {
                _playerDust.Play();
                return;
            }
            
            _playerDust.Stop();
        }
    }
}