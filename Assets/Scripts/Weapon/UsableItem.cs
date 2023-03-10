#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Game
{
    /**
     * Represent a item that can be used by the player using the
     * "use" button
     * Rely on Pickable component to be pick up by a player and to be used
     * Every item and weapon on stage should inherit this class and operate on
     * the provided events calls.
     * The derived class should override the Initialize function to hook up events as needed
     */
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public abstract class UsableItem : MonoBehaviour
    {
        public UsableItemID ID => _id;
        public Sprite Icon => _icon;
        public int Durability => _durability;
        public float DurabilityPercent => (float) _durability / _maxDurability;
        public Transform PlayerTrans => transform.parent.parent;
        public Rigidbody2D PlayerRD => PlayerTrans.GetComponent<Rigidbody2D>();
        
        /**
         * Invoked when this item is picked up
         */
        public event UnityAction OnPickedUp = () => { };
        /**
         * Invoked when the item use button on this item is down
         */
        public event UnityAction<PlayerID> OnItemUseDown = (_) => { };
        /**
         * Invoked when the item use button on this item is up
         */
        public event UnityAction<PlayerID> OnItemUseUp = (_) => { };
        /**
         * Invoked when this item is held by the player, which is switched into the inventory
         */
        public event UnityAction<PlayerID> OnHold = (_) => { };
        /**
         * Invoked when this item is equipped by the player
         * either when picked up or switched out of the inventory for use
         */
        public event UnityAction<PlayerID> OnEquip = (_) => { };
        /**
         * Invoked when the durability has changed
         */
        public event UnityAction<UsableItem> OnDurabilityChange = (_) => { };
        /**
         * Invoked when this item is returned to the pool
         */
        public event UnityAction OnReturn = () => { };
        /**
         * Invoked when this item breaks, which is when durability <= 0
         */
        public event UnityAction<UsableItem> OnBreak = (_) => { };

        [SerializeField] protected GameplayService _service;
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected GameObject _visual;
        [SerializeField] protected AudioID _audioOnUse;
        [SerializeField] protected AudioID _audioOnEquip;
		// Controls the max angle the gun can recoil to
		[SerializeField] private float _maxTilt = 70f;
		// Controls how fast the player recovers from the recoil
        [SerializeField] private float _tiltSpeed = 100f;
		// Controls the angle added to the weapon per shot
		[SerializeField] private float _recoilAmount = 20f;
        // Force applied to player for recoil
		[SerializeField] private Vector2 _recoilForce;

        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private Transform _transform;
		private Vector3 _targetAngle;

        [SerializeField] private UsableItemID _id;
        [SerializeField] private int _maxDurability;
        [SerializeField] private int _durability;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _transform = transform;

            OnHold += (_) => gameObject.SetActive(false);
            OnEquip += (_) => gameObject.SetActive(true);
            
            Reset();
        }

        /**
         * Every derived class should override this function to
         * set up the needed event calls for its unique behavior
         */
        protected abstract void Initialize();

        private void Reset()
        {
            _durability = _maxDurability;
            // clear all event calls to prevent left over functions
            // from the previous player, etc
            OnPickedUp = () => { };
            OnItemUseDown = (_) => { };
            OnItemUseUp = (_) => { };
            OnHold = (_) => { };
            OnEquip = (_) => { };
            OnReturn = () => { };
            OnDurabilityChange = (_) => { };
            OnBreak = (_) => { };

            EnablePhysics();
            Initialize();
        }

        /**
         * set the parent of this gameObject and reset local position
         */
        public void SetAndMoveToParent(Transform parent)
        {
            _transform.SetParent(parent, false);
            _transform.localPosition = Vector3.zero;
        }
        
        private void DisablePhysics()
        {
            _collider.enabled = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
        }
        
        private void EnablePhysics()
        {
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        /**
         * called by the player inventory when the item use is down
         * to trigger the event
         */
        public void ItemUseDown(PlayerID itemUser) => OnItemUseDown.Invoke(itemUser);
        
        /**
         * called by the player inventory when the item is held
         * to trigger the event
         */
        public void Hold(PlayerID itemUser) => OnHold.Invoke(itemUser);

        /**
         * called by the player inventory when the item is equipped
         * to trigger the event
         */
        public void Equip(PlayerID itemUser)
        {
            _service.AudioManager.PlayAudio(_audioOnEquip);
            OnEquip.Invoke(itemUser);
        }
        
        /**
         * called by the player inventory when the item use is up
         * to trigger the event
         */
        public void ItemUseUp(PlayerID itemUser) => OnItemUseUp.Invoke(itemUser);

        /**
         * called by the player inventory to set the item object
         * state to attach to the player object
         */
        public void PickUpBy(Transform itemHolder)
        {
            DisablePhysics();
            SetAndMoveToParent(itemHolder);
            OnPickedUp.Invoke();
        }

        /**
         * Reduce the durability of the item by the given value
         */
        public void ReduceDurability(int value)
        {
			ApplyRecoil();
            _durability -= value;
            OnDurabilityChange.Invoke(this);
            if (_durability > 0) return;
            OnBreak.Invoke(this);
            OnReturn.Invoke();
            _service.UsableItemManager.ReturnUsableItem(_id, this);
            Reset();
        }

		/**
         * Adds recoil with _maxTilt considered when the gun durability decreases
         */
		private void ApplyRecoil() {
			
            // If player is facing right
            if (PlayerTrans.localScale.x < 0)
            {
                // Add recoil from current shot
                _targetAngle = new Vector3(0, 0, Mathf.Lerp(
                    -_maxTilt,
                    _maxTilt,
                    Mathf.InverseLerp(-359, 359, _recoilAmount + _targetAngle.z)));
                PlayerRD.AddForce(-_recoilForce, ForceMode2D.Impulse);
            }
            // If player facing left
            else
            {
                // Negative angles get converted to 360 + angle, so it converts it back here when calculating recoil
                if (_targetAngle.z > 0) _targetAngle.z = -(359 - _targetAngle.z);
                // "Add" recoil
                _targetAngle = new Vector3(0, 0, Mathf.Lerp(
                    -_maxTilt,
                    _maxTilt,
                    Mathf.InverseLerp(-359, 359, _targetAngle.z - _recoilAmount)));
                PlayerRD.AddForce(_recoilForce, ForceMode2D.Impulse);
            }
            // Set new target angle
			_visual.transform.rotation = Quaternion.Euler(_targetAngle);
        }

		/**
         * Update in this class controls the smooth recovery from recoil
         */
		public void Update() {
            _visual.transform.rotation = Quaternion.RotateTowards(_visual.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), _tiltSpeed * Time.deltaTime);
            _targetAngle.z = _visual.transform.rotation.eulerAngles.z;
        }

        public void MakeInvisible()
        {
            _visual.SetActive(false);
        }

        public void MakeVisible()
        {
            _visual.SetActive(true);
        }
    }
}