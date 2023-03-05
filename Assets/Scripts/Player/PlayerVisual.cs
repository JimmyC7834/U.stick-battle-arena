using System;
using System.Collections;
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
        private PlayerStat _playerStat;

        [Header("Flash Effect Settings")]
        [SerializeField] private float _flashTime;
        [SerializeField] private float _flashRate;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerStat = GetComponent<PlayerStat>();
            _playerMovement.OnMovement += HandleMovementVisual;
            _playerStat.OnRespawn += PlayFlashEffect;
            _playerStat.OnDeath += (_) => _playerDust.Stop();
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

        private void PlayFlashEffect()
        {
            StartCoroutine(FlashingEffect());
        }
        
        private IEnumerator FlashingEffect()
        {
            float timer = Time.time;
            Color originalC = _playerDisplay.color;
            while (Time.time - timer < _flashTime)
            {
                yield return null;
                Color c = _playerDisplay.color;
                float r = (Time.time - timer) % (1 / _flashRate);
                c.a = (r > 1/ (2 * _flashRate)) ? 1 : 0;
                _playerDisplay.color = c;
                Debug.Log(c.a);
            }

            _playerDisplay.color = originalC;
        }
    }
}