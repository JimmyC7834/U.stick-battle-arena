#region

using UnityEngine;

#endregion

namespace Game
{
    public class App
    {
        // Temporary fix for build
        // private static readonly SceneID GLOBALMANAGERS_SCENEID = SceneID.GlobalManagers;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            // UnityEngine.SceneManagement.SceneManager.LoadScene(
            //     GLOBALMANAGERS_SCENEID.ToString());
            
            // temporary setting
            Application.targetFrameRate = 60;
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.SetResolution(1920, 1080, true);
        }

        public static void ExitApplication()
        {
            Application.Quit();
            Debug.Log("Exit!");
        }
    }
}