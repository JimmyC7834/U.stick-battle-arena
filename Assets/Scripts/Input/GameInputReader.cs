using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Game
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/GameInputReader")]
    public class GameInputReader : ScriptableObject, GameInput.IMenuActions
    {
        public event UnityAction escEvent = delegate { };
        private GameInput _gameInput;

        private void OnEnable() {
            if (_gameInput == null) {
                _gameInput = new GameInput();

                _gameInput.Menu.SetCallbacks(this);
            }
            
            _gameInput.Menu.Enable();
        }

        private void OnDestroy()
        {
            escEvent = () => { };
            _gameInput.Menu.Disable();
        }
        
        public void OnEsc(InputAction.CallbackContext context)
        {
            escEvent.Invoke();
        }
    }
}