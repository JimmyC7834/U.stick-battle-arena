#region

using UnityEngine;

#endregion

namespace Game
{
    /**
     * Game mode logic for battle royal mode.
     * The game end with there's only one player's score >= target score
     */
    [CreateAssetMenu]
    public class HighScoreGameModeLogicSO : GameModeLogicSO
    {
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private GameplayService _gameplayService;
        [SerializeField] private int _targetScore;
        
        protected override void HookEvents()
        {
            _gameSettings.SetPlayerLifeCount(99);
            _gameplayService.PlayerManager.OnScoreChange += CheckForWinner;
        }
        
        protected override void UnHookEvents()
        {
            _gameplayService.PlayerManager.OnScoreChange -= CheckForWinner;
        }

        private void CheckForWinner(PlayerID id)
        {
            if (_gameplayService.PlayerManager.GetScore(id) >= _targetScore)
                InvokeGameEndedEvent(id);
        }
    }
}