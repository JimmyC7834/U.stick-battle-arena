using System;
using UnityEngine;

namespace Game
{
    public class ScreenSize : MonoBehaviour
    {
        [SerializeField] private int width, height;
        [SerializeField] private int fwidth, fheight;
        [SerializeField] private bool fullscreen = false;

        private void Awake()
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.SetResolution(width, height, false);
        }

        void Update()
        {
            if (fullscreen)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.SetResolution(fwidth, fheight, true);
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.SetResolution(width, height, false);
            }
 
            if (Input.GetKeyDown(KeyCode.F11)) fullscreen = !fullscreen;
        }
    }
}