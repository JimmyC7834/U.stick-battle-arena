#region

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Game
{
    public enum SceneID
    {
        // GlobalManagers = 1,
        MainMenu = 2,
        MapSelectionMenu = 3,
        PlayerLobby = 4,
        Farm = 10,
        Space = 11,
        Factory = 12,
        Waterfall = 13,
        Mine = 14,
        Dungeon = 15,
        Snow = 16,
    }
    
    /**
     * Respond for loading and unloading scenes
     */
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private GameService _gameService;
        [SerializeField] private SceneID _currentScene;

        private void Awake()
        {
            _gameService.ProvideSceneManager(this);
        }

        /**
         * Unload current scene and load the new scene
         */
        public void LoadScene(SceneID id)
        {
            _gameService.SceneTransition.CloseScene(() => LoadSceneProcess(id));
        }

        private void LoadSceneProcess(SceneID id)
        {
            UnityEngine.SceneManagement.SceneManager
                .UnloadSceneAsync(_currentScene.ToString());
            AdditionLoadScene(id);
        }

        /**
         * Load the first scene and unload the default scene
         */
        public void AdditionLoadScene(SceneID id)
        {
            _currentScene = id;
            UnityEngine.SceneManagement.SceneManager
                .LoadSceneAsync(
                    id.ToString(), 
                    LoadSceneMode.Additive).completed += (_) =>
            {
                if (_gameService.SceneTransition == null) return;
                _gameService.SceneTransition.OpenScene(null);
            };
        }

        public void ExitGame()
        {
            App.ExitApplication();
        }
    }
}