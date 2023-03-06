#region

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace Game.UI
{
    public class UI_PlayerLobbyPanel : MonoBehaviour
    {
        public event UnityAction<PlayerReadyInfo> OnReady = (_) => { };
        public event UnityAction OnJoin = () => { };
        public bool IsReady { get; private set; } = false;
        public bool Joined { get => _joined; }

        [SerializeField] private GameService _gameService;
        [SerializeField] private PlayerInputReader _input;
        [SerializeField] private PlayerID _playerID;
        [SerializeField] private Sprite[] _playerAccessories;
        [SerializeField] private Color[] _playerColors;
        [SerializeField] private Image _playerAccDisplay;
        [SerializeField] private Image _playerDisplay;
        [SerializeField] private RectTransform _readyText;
        [SerializeField] private GameObject _joinPrompt;
        [SerializeField] private bool _joined;

        [Header("Arrows Visual")]
        
        [SerializeField] private RectTransform _arrowUp;
        [SerializeField] private RectTransform _arrowDown;
        [SerializeField] private RectTransform _arrowLeft;
        [SerializeField] private RectTransform _arrowRight;
        private Vector3 _arrowUpOrigin;
        private Vector3 _arrowDownOrigin;
        private Vector3 _arrowLeftOrigin;
        private Vector3 _arrowRightOrigin;
        [SerializeField] private float _returnSpeed;
        [SerializeField] private int _moveDistance;
        
        private ArraySelector<Sprite> _spriteSelector;
        private ArraySelector<Color> _colorSelector;
        
        private void Awake()
        {
            _input = new PlayerInputReader();
            _input.DisableAllInput();
            Invoke(nameof(Activate), 0.5f);

            _spriteSelector = new ArraySelector<Sprite>(_playerAccessories);
            _colorSelector = new ArraySelector<Color>(_playerColors);
            _arrowUpOrigin = _arrowUp.anchoredPosition;
            _arrowDownOrigin = _arrowDown.anchoredPosition;
            _arrowLeftOrigin = _arrowLeft.anchoredPosition;
            _arrowRightOrigin = _arrowRight.anchoredPosition;

            _input.moveEvent += HandleMoveInput;
            _input.useItemDownEvent += HandleReady;
        }

        private void Update()
        {
            _arrowUp.anchoredPosition = Vector2.Lerp(
                _arrowUp.anchoredPosition, _arrowUpOrigin, _returnSpeed * Time.deltaTime);
            
            _arrowDown.anchoredPosition = Vector2.Lerp(
                _arrowDown.anchoredPosition, _arrowDownOrigin, _returnSpeed * Time.deltaTime);
            
            _arrowLeft.anchoredPosition = Vector2.Lerp(
                _arrowLeft.anchoredPosition, _arrowLeftOrigin, _returnSpeed * Time.deltaTime);
            
            _arrowRight.anchoredPosition = Vector2.Lerp(
                _arrowRight.anchoredPosition, _arrowRightOrigin, _returnSpeed * Time.deltaTime);
        }

        private void Activate() => _input.EnablePlayerInput(_playerID);

        private void OnDestroy()
        {
            Destroy(_input);
        }

        private void HandleReady()
        {
            // check if the player has joined
            if (!_joined)
            {
                _joined = true;
                _joinPrompt.SetActive(false);
                OnJoin.Invoke();
                return;
            }
            
            // handle ready and un ready
            if (IsReady)
            {
                IsReady = false;
                _readyText.gameObject.SetActive(false);
                return;
            }
            
            _gameService.AudioManager.PlayAudio(AudioID.Click1);
            IsReady = true;
            _readyText.gameObject.SetActive(true);
            OnReady.Invoke(
                new PlayerReadyInfo(
                    _playerID, 
                    _spriteSelector.GetCurrent(),
                    _colorSelector.GetCurrent()));
        }

        private void HandleMoveInput(Vector2 vec)
        {
            if (!_joined) return;
            if (IsReady) return;
            MoveSelector(vec);
            _playerAccDisplay.sprite = _spriteSelector.GetCurrent();
            _playerDisplay.color = _colorSelector.GetCurrent();
        }
        
        private void MoveSelector(Vector2 vec)
        {
            if (vec.x > 0)
            {
                _gameService.AudioManager.PlayAudio(AudioID.Select2);
                _spriteSelector.Next();
                _arrowRight.anchoredPosition = 
                    _arrowRightOrigin + Vector3.right * _moveDistance;
                return;
            }

            if (vec.x < 0)
            {
                _gameService.AudioManager.PlayAudio(AudioID.Select1);
                _spriteSelector.Prev();
                _arrowLeft.anchoredPosition = 
                    _arrowLeftOrigin + Vector3.left * _moveDistance;
                return;
            }
            
            if (vec.y > 0)
            {
                _gameService.AudioManager.PlayAudio(AudioID.Select2);
                _colorSelector.Next();
                _arrowUp.anchoredPosition = 
                    _arrowUpOrigin + Vector3.up * _moveDistance;
                return;
            }
            
            if (vec.y < 0)
            {
                _gameService.AudioManager.PlayAudio(AudioID.Select1);
                _colorSelector.Prev();
                _arrowDown.anchoredPosition = 
                    _arrowDownOrigin + Vector3.down * _moveDistance;
                return;
            }
        }
    }
    
    public struct PlayerReadyInfo
    {
        public readonly PlayerID PlayerID;
        public readonly Color Color;
        public readonly Sprite Accessory;
            
        public PlayerReadyInfo(PlayerID id, Sprite accessory, Color color)
        {
            PlayerID = id;
            Color = color;
            Accessory = accessory;
        }
    }
}