#region

using Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Game.UI
{
    public class UI_PlayerStatusDisplay : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO _gameSettings;
        [SerializeField] private GameplayService _service;

        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private TMP_Text _lifeCountLabel;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _itemDurabilityBar;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private Image _currItemIcon;
        [SerializeField] private Image _playerDisplay;
        [SerializeField] private Image _playerAcc;

        [SerializeField] private PlayerID _playerID;
        
        /**
         * Initialize the UI by hooking to corresponded events
         * Should ONLY be called by the corresponding manager
         */
        public void Initialize()
        {
            gameObject.SetActive(false);
            // deactivate player status display if the player is not in the gameplay
            if (!_gameSettings.PlayerIDInGameplay(_playerID)) return;
            gameObject.SetActive(true);

            // set up event hooks
            PlayerStat playerStat = _service.PlayerManager.GetPlayerStat(_playerID);
            playerStat.OnHealthChange += UpdateHealthBarVisual;
            playerStat.OnDeath += UpdateLifeCountVisual;

            PlayerInventory playerInventory = playerStat.GetComponent<PlayerInventory>();
            playerInventory.OnItemSwitch += UpdateInventoryIcon;
            playerInventory.OnItemEquip += HookToItemDurabilityChange;
            playerInventory.OnItemHold += UnHookToItemDurabilityChange;
            playerInventory.OnItemPick += UpdateEquipItemIcon;

            _lifeCountLabel.text = _service.PlayerManager
                .GetRemainingLife(_playerID).ToString();

            PlayerReadyInfo info = _gameSettings.GetPlayerSettings(_playerID);
            _playerDisplay.color = info.Color;
            _playerAcc.sprite = info.Accessory;
            
            _service.PlayerManager.OnScoreChange += UpdateScore;
        }

        private void UpdateHealthBarVisual(PlayerStat playerStat)
        {
            _healthBar.fillAmount = playerStat.HealthPercentage;
        }

        private void HookToItemDurabilityChange(PlayerInventory inventory)
        {
            inventory.EquippedItem.OnDurabilityChange += UpdateDurabilityBar;
            inventory.EquippedItem.OnBreak += EmptyWeaponSlot;
            UpdateDurabilityBar(inventory.EquippedItem);
        }

        private void EmptyWeaponSlot(UsableItem _)
        {
            _currItemIcon.gameObject.SetActive(false);
        }

        private void UnHookToItemDurabilityChange(PlayerInventory inventory)
        {
            inventory.HeldItem.OnDurabilityChange -= UpdateDurabilityBar;
            inventory.HeldItem.OnBreak -= EmptyWeaponSlot;
        }

        private void UpdateDurabilityBar(UsableItem item)
        {
            _itemDurabilityBar.fillAmount = item.DurabilityPercent;
        }

        private void UpdateEquipItemIcon(PlayerInventory inventory)
        {
            if (inventory.EquippedItem != null)
            {
                _currItemIcon.gameObject.SetActive(true);
                _currItemIcon.sprite = inventory.EquippedItem.Icon;
            }
            else
            {
                _currItemIcon.gameObject.SetActive(false);
            }
        }
        
        private void UpdateInventoryIcon(PlayerInventory inventory)
        {
            UpdateEquipItemIcon(inventory);
            if (inventory.HeldItem != null)
            {
                _itemIcon.gameObject.SetActive(true);
                _itemIcon.sprite = inventory.HeldItem.Icon;
            }
            else
            {
                _itemIcon.gameObject.SetActive(false);
            }
        }
        
        private void UpdateLifeCountVisual(int lifeCount)
        {
            _lifeCountLabel.text = $"{lifeCount}";
        }

        private void UpdateScore(PlayerID id)
        {
            if (id != _playerID) return;
            _scoreLabel.text = $"{_service.PlayerManager.GetScore(id)}";
        }
    }
}