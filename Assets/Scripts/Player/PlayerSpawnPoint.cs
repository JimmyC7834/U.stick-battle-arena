#region

using System.Collections;
using Game.Player;
using UnityEngine;

#endregion

namespace Game
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public PlayerStat PlayerStatStat => _playerStat;
        
        [SerializeField] private GameplayService _service;
        [SerializeField] private PlayerID _playerID;
        [SerializeField] private float _spawnDelay;
        private PlayerStat _playerStat;

        public void Initialize(PlayerStat playerStat)
        {
            _playerStat = playerStat;
            _playerStat.transform.position = transform.position;
            _playerStat.OnDeath += Respawn;
        }

        /**
         * Change the position of the player to the position of the spawner
         */
        private void Respawn(int life)
        {
            if (life == 0) return;
            StartCoroutine(RespawnDelay());
        }

        private IEnumerator RespawnDelay()
        {
            yield return new WaitForSecondsRealtime(_spawnDelay);
            _playerStat.transform.position = transform.position;
            _playerStat.Respawn();
        }
    }
}
