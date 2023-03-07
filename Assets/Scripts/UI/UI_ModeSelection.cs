using System;
using Game.DataSet;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UI_ModeSelection : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private GameService _gameService;
        [SerializeField] private TMP_Text _shadowBR;
        [SerializeField] private TMP_Text _shadowTS;
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;
        [SerializeField] private AudioID _clickSound;

        private void Start()
        {
            _gameSettings.SetGameMode(GameModeID.BattleRoyal);
            _shadowTS.color = _offColor;
            _shadowBR.color = _onColor;
        }

        public void SetBattleRoyalMode()
        {
            _gameSettings.SetGameMode(GameModeID.BattleRoyal);
            _shadowTS.color = _offColor;
            _shadowBR.color = _onColor;
            _gameService.AudioManager.PlayAudio(_clickSound);
        }
        
        public void SetTargetScoreMode()
        {
            _gameSettings.SetGameMode(GameModeID.TargetScore);
            _shadowTS.color = _onColor;
            _shadowBR.color = _offColor;
            _gameService.AudioManager.PlayAudio(_clickSound);
        }
    }
}