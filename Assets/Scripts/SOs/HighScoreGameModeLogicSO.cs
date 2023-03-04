using UnityEngine;

namespace Game
{
    /**
     * Game mode logic for battle royal mode.
     * The game end with there's only one player's score >= target score
     */
    [CreateAssetMenu]
    public class HighScoreGameModeLogicSO : GameModeLogicSO
    {
        [SerializeField] private GameplayService _gameplayService;
        [SerializeField] private int _targetScore;
        
        protected override void HookEvents()
        {
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