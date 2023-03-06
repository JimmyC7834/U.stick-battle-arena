#region

using UnityEngine;

#endregion

namespace Game.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimator : MonoBehaviour
    {
        // maxTilt and tiltSpeed control rate and maximum pitch of rotation effect while moving
        [SerializeField] private float _maxTilt = 4;

        [SerializeField] private float _tiltSpeed = 20;

        // LandAnimDuration controls the length of the landing animation
        [SerializeField] private float _landAnimDuration = .45f;

        // Grab renderer
        [SerializeField] private SpriteRenderer _renderer;

        // Grab animator
        [SerializeField] private Animator _anim;

        private PlayerMovement _player;
        private float _lockedTill;
        
        private void Awake()
        {
            if (!TryGetComponent(out PlayerMovement player))
            {
                Destroy(this);
                return;
            }

            _player = player;
        }

        private void Update()
        {
            // Apply rotation to model dependant of speed and time
            var targetRotVector = new Vector3(
                0, 0,
                Mathf.Lerp(
                    -_maxTilt,
                    _maxTilt,
                    Mathf.InverseLerp(-1, 1, _player.MovementInput.x)));

            _anim.transform.rotation = Quaternion.RotateTowards(_anim.transform.rotation,
                Quaternion.Euler(targetRotVector), _tiltSpeed * Time.deltaTime);
            var state = GetState();
            if (state == _currentState) return;
            // Apply animation changes
            _anim.CrossFade(state, 0, 0);
            _currentState = state;
        }

        private int GetState()
        {
            // Catch the lock
            if (Time.time < _lockedTill) return _currentState;

            // Animations sorted by priority
            // if (_player.LandingThisFrame) return LockState(Land, _landAnimDuration);
            if (_player.MovementInput.y < 0) return Land;
            if (_player.MovementInput.y > 0) return Jump;
            if (_player.MovementInput.x == 0) return Idle;
            if (_player.MovementInput.x != 0) return Walk;

            // Lock the animation for t amount of time, used when you do not want to be interrupted
            int LockState(int s, float t)
            {
                _lockedTill = Time.time + t;
                return s;
            }

            return Idle;
        }

        #region Cached Properties

        private int _currentState;

        private static readonly int Idle = Animator.StringToHash("PlayerIdle16");
        private static readonly int Land = Animator.StringToHash("PlayerDrop16");
        private static readonly int Jump = Animator.StringToHash("PlayerJump16");
        private static readonly int Walk = Animator.StringToHash("PlayerWalk16");

        #endregion
    }
}