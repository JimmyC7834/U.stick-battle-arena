using Game.Player;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Projectile))]
    public class ShotgunBullet : MonoBehaviour
    {
        [SerializeField] private GameplayService _service;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _damage;
        [SerializeField] private float _score;
        
        private void Awake()
        {
            _projectile = GetComponent<Projectile>();
            _projectile.OnHitPlayer += HandleHitPlayer;
            _projectile.OnHitStage += HandleHitStage;
            _projectile.OnHitProjectile += HandleHitProjectile;
        }

        private void HandleHitPlayer(PlayerController player, PlayerController executor)
        {
            if (player != executor)
            {
                // Increase score of the dealer if hit
                _service.PlayerManager.IncreaseScore(executor.Stat.ID, _score);
                // Deduct health of the hit player
                player.Stat.DeductHealth(executor.Stat.ID, _damage);
                ReturnToPool();
            }
        }
        
        private void ReturnToPool()
        {
            // added this if statement to prevent multi collisions (e.g., floor and player)
            // that causes release of the same object twice
            if (gameObject.activeSelf)
            {
                _projectile.ReturnToPool();
            }
        }

        private void HandleHitStage() => ReturnToPool();

        private void HandleHitProjectile(Projectile other)
        {
            // does not return to pool if collide with shotgun bullets
            if (other.gameObject.GetComponent<ShotgunBullet>() == null)
                ReturnToPool();
        }
    }
}