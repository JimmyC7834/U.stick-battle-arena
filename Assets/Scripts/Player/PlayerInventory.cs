using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

namespace Game.Player
{
    /**
     * Represent the inventory of a player
     */
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Transform _itemHolderTrans;
        [SerializeField] private UsableItem _equippedItem;
        [SerializeField] private UsableItem _holdItem;
        [SerializeField] private PlayerController _playerController;
        
        /**
         * Called when the item is switched
         * 1st arg: the currently equipping item after switching
         * 2nd arg: the hold item in inventory after switching
         */
        public event UnityAction<UsableItem, UsableItem> OnItemSwitched = (_, _) => { };
        /**
         * Called when an item is equipped to the player
         * 1st arg: the equipped item
         */
        public event UnityAction<UsableItem> OnItemEquip = (_) => { };
        /**
         * Called when an item is unequipped from the player
         * 1st arg: the unequipped item
         */
        public event UnityAction<UsableItem> OnItemUnEquip = (_) => { };
        /**
         * Called when picked an item
         * 1st arg: the picked item
         */
        public event UnityAction<UsableItem> OnItemPick = (_) => { };

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();

            _playerController.OnMovement += CheckAndFlip;
            _playerController.OnSwitchItem += SwitchItem;
        }
        
        // flip the item holder when the player is facing different directions
        private void CheckAndFlip()
        {
            _itemHolderTrans.localScale = new Vector3(
                _playerController.FacingLeft ? 1 : -1, 1, 1);
        }

        // handle player's switch action, switch items in inventory
        private void SwitchItem()
        {
            // do nothing if don't have another item
            if (Object.Equals(_holdItem, null)) return;
            
            // switch
            if (!Object.Equals(_equippedItem, null))
            {
                _equippedItem.UnEquip();
                OnItemUnEquip.Invoke(_equippedItem);
            }

            _holdItem.Equip();
            OnItemEquip.Invoke(_holdItem);
            (_equippedItem, _holdItem) = (_holdItem, _equippedItem);
            OnItemSwitched.Invoke(_equippedItem, _holdItem);
        }

        // handle if the item is broken
        // automatically equip the remaining item
        public void DumpItem(UsableItem item)
        {
            // disable item
            item.UnEquip();
            OnItemUnEquip.Invoke(item);

            if (item == _equippedItem)
            {
                _equippedItem = null;
            }
            else
            {
                _holdItem = null;
            }

            // switch the inventory weapon out if the player has one
            SwitchItem();
        }
        
        /**
         * Set the item to the player's item holder
         * and updates player's inventory
         */
        public void PickUpItem(UsableItem item)
        {
            // check if currently holding an Item
            if (_equippedItem != null)
            {
                if (_holdItem == null)
                {
                    // holding one item, but got an empty slot
                    // set equipped item to holding equip pick up item 
                    _holdItem = _equippedItem;
                    _holdItem.UnEquip();
                    OnItemUnEquip.Invoke(_holdItem);
                }
                else
                {
                    // no empty slot, replace the item with equipped one
                    _equippedItem.UnEquip();
                    OnItemUnEquip.Invoke(_equippedItem);
                    _equippedItem.ReturnToPool();
                }
            }
            
            _equippedItem = item;
            item.Equip();
            OnItemEquip.Invoke(item);

            // move the gameObject under the player's holder
            item.SetAndMoveToParent(_itemHolderTrans);
            OnItemPick.Invoke(item);
        }
    }
}