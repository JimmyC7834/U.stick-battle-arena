#region

using Game.DataSet;
using UnityEngine;

#endregion

namespace Game
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private GameService _gameService;
        [SerializeField] private GameplayService _service;
        [SerializeField] private GameModeLogicDataSetSO _gameModeLogicDataSet;
        
        [Header("Manager References")]
        [SerializeField] private ProjectileManager _projectileManager;
        [SerializeField] private UsableItemManager _usableItemManager;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private GameplayUIManager _gameplayUIManager;

        [SerializeField] private GameModeLogicSO _gameModeLogic;
        
        private void Awake()
        {
            _gameModeLogic = _gameModeLogicDataSet[_gameSettings.GameModeID];

            _service.ProvideProjectileManager(_projectileManager);
            _service.ProvideUsableItemManager(_usableItemManager);
            _service.ProvidePlayerManager(_playerManager);
            _service.ProvideGameplayUIManager(_gameplayUIManager);
            
            // initialize and hook the game ended event
            _gameModeLogic.Initialize();
            _gameModeLogic.OnGameEnded += HandleGameEnded;
            
            // Initialize managers in order of dependency
            _playerManager.Initialize();
            _gameplayUIManager.Initialize();

            _gameService.PlayStageBGM();
        }

        private void HandleGameEnded(PlayerID winnerId)
        {
            Debug.Log("Game ended");
            // temporary winning effect
            // slow time and wait for 4 seconds to load back to main menu
            Time.timeScale = 0.25f;
            _gameService.StopStageBGM();
            _gameSettings.SetWinner(winnerId, _service.PlayerManager.GetScore(winnerId));
            _service.GameplayUIManager.ShowWinningScreen();
            Invoke(nameof(LoadBackToMainMenu), 2f);
        }

        private void LoadBackToMainMenu()
        {
            Time.timeScale = 1f;
            _gameService.SceneManager.LoadScene(SceneID.MainMenu);
        }
    }
}