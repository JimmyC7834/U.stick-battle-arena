#region

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameplayService _service;

        [Header("Entries")]
        [SerializeField] private WeaponOdds[] _entries;
        [SerializeField] private Transform[] _positionEntries;
        
        [Header("Spawner Settings")]
        [SerializeField] private float _spawnInterval;
        [SerializeField] private int _maxItemNumber;

        private List<UsableItemID> _weaponIDList;
        private float _currentTime;
        private int _currItemNum;

        private void Start()
        {
            _weaponIDList = new List<UsableItemID>();
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
                SpawnWeapon();
                _currentTime = _spawnInterval;
            }
        }

        private void SpawnWeapon()
        {
            int rand = Random.Range(0, _weaponIDList.Count);
            int randPos = Random.Range(0, _positionEntries.Length);

            // Get a weapon from the pool and set to the current location
            UsableItem weapon = _service.UsableItemManager.SpawnProjectile(_weaponIDList[rand]);
            weapon.transform.position = _positionEntries[
                (int) Mathf.Pow(randPos, 7) % _positionEntries.Length].position;
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
