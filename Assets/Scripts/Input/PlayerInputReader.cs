#region

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

#endregion

namespace Game
{
    [CreateAssetMenu(fileName = "PlayerActionInput", menuName = "Game/PlayerActionInput")]
    public class PlayerInputReader : ScriptableObject, PlayerInput.IPlayer1Actions, 
                                                        PlayerInput.IPlayer2Actions,
                                                        PlayerInput.IPlayer3Actions,
                                                        PlayerInput.IPlayer4Actions
    {
        // Player
        public event UnityAction<Vector2> moveEvent = delegate { };
        public event UnityAction useItemDownEvent = delegate { };
        public event UnityAction useItemUpEvent = delegate { };
        public event UnityAction switchItemEvent = delegate { };

        // !!! Remember to edit Input Reader functions upon updating the input map !!!
        private PlayerInput _gameInput;
        [SerializeField] private PlayerID _playerID;

        private void OnEnable() {
            if (_gameInput == null) {
                _gameInput = new PlayerInput();
                _gameInput.Player1.SetCallbacks(this);
                _gameInput.Player2.SetCallbacks(this);
                _gameInput.Player3.SetCallbacks(this);
                _gameInput.Player4.SetCallbacks(this);
            }

            DisableAllInput();
            EnablePlayerInput(_playerID);
        }

        private void OnDestroy()
        { 
            moveEvent = delegate { };
            useItemDownEvent = delegate { };
            useItemUpEvent = delegate { };
            switchItemEvent = delegate { };
            DisableAllInput();
        }

        #region PlayerInput

        public void OnMovement(InputAction.CallbackContext context)
        {
            moveEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                useItemDownEvent.Invoke();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                useItemUpEvent.Invoke();
            }
        }

        public void OnSwitchItem(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                switchItemEvent.Invoke();
        }

        #endregion

        public void EnablePlayerInput()
        {
            EnablePlayerInput(_playerID);
        }
        
        public void EnablePlayerInput(PlayerID id)
        {
            // enable corresponding input for player id
            switch (id)
            {
                case PlayerID.Player1:
                    EnablePlayer1Input();
                    break;
                case PlayerID.Player2:
                    EnablePlayer2Input();
                    break;
                case PlayerID.Player3:
                    EnablePlayer3Input();
                    break;
                case PlayerID.Player4:
                    EnablePlayer4Input();
                    break;
                default:
                    Debug.LogError($"Invalid player id {id}");
                    DisableAllInput();
                    break;
            }
        }
        
        // Input Reader Controls
        public void EnablePlayer1Input() {
            DisableAllInput();
            _gameInput.Player1.Enable();
        }
        
        public void EnablePlayer2Input() {
            DisableAllInput();
            _gameInput.Player2.Enable();
        }
        
        public void EnablePlayer3Input() {
            DisableAllInput();
            _gameInput.Player3.Enable();
        }
        
        public void EnablePlayer4Input() {
            DisableAllInput();
            _gameInput.Player4.Enable();
        }

        public void DisableAllInput() {
            _gameInput.Player1.Disable();
            _gameInput.Player2.Disable();
            _gameInput.Player3.Disable();
            _gameInput.Player4.Disable();
        }
    }
}