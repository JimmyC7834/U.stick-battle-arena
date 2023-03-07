using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UI_WinningScreen : MonoBehaviour
    {
        [SerializeField] private GameService _gameService;
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private TMP_Text _playerText;
        [SerializeField] private TMP_Text _playerScore;
        [SerializeField] private Image _playerDisplay;
        [SerializeField] private Image _playerAcc;

        private void OnEnable()
        {
            (PlayerReadyInfo, int) winInfo = _gameSettings.WinnerInfo;
            _gameService.AudioManager.PlayAudio(AudioID.Win);
            _playerText.text = winInfo.Item1.PlayerID.ToString();
            _playerScore.text = winInfo.Item2.ToString();
            _playerDisplay.color = winInfo.Item1.Color;
            _playerAcc.sprite = winInfo.Item1.Accessory;
        }
    }
}