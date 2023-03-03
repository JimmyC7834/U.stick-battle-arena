using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameplayService _service;

        [Header("Entries")]
        [SerializeField] private WeaponOdds[] _entries;
        
        [Header("Spawner Settings")]
        [SerializeField] private float _spawnInterval;
        [SerializeField] private int _maxItemNumber;
        [SerializeField] private float _randomXPosLowerBound;
        [SerializeField] private float _randomXPosUpperBound;

        private List<UsableItemID> _weaponIDList;
        private Vector3 _originalPosition;
        private float _currentTime;
        private int _currItemNum;

        private void Awake()
        {
            _weaponIDList = new List<UsableItemID>();
            _originalPosition = transform.position;
            _service.UsableItemManager.OnReturnUsableItem += ReturnUsableItem;

            for (int i = 0; i < _entries.Length; i++)
            {
                for (int j = 0; j < _entries[i].odds; j++)
                {
                    _weaponIDList.Add(_entries[i].id);
                }
            }
        }

        private void Update()
        {
            if (_currItemNum >= _maxItemNumber)
                return;
            
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            if (_currentTime > 0)
            {
                // update current count down time
                _currentTime -= Time.deltaTime;
            }
            else
            {
                // update the position of the spawner and spawn weapon
                SpawnWeapon();
                UpdatePosition();
                _currentTime = _spawnInterval;
            }
        }

        private void UpdatePosition()
        {
            // random x position
            transform.position = _originalPosition + new Vector3(
                Random.Range(_randomXPosLowerBound, _randomXPosUpperBound),
                0);
        }

        private void SpawnWeapon()
        {
            int rand = Random.Range(0, _weaponIDList.Count);
            
            // Get a weapon from the pool and set to the current location
            UsableItem weapon = _service.UsableItemManager.SpawnProjectile(_weaponIDList[rand]);
            weapon.transform.position = transform.position;
            _currItemNum++;
        }

        private void ReturnUsableItem()
        {
            _currItemNum--;
        }

        [Serializable]
        private struct WeaponOdds
        {
            public UsableItemID id;
            public int odds;
        }
    }
}
