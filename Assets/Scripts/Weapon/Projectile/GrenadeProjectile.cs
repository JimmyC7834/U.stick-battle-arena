using System;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class GrenadeProjectile : Projectile
    {
        [SerializeField] private float _splashRadius;
        
        private void Start()
        {
            OnHitPlayer += (_) => Explode();
            OnHitStage += Explode;
        }

        private void Explode()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _splashRadius);
            foreach (Collider2D hit in hits)
            {
                PlayerStat player = hit.GetComponent<PlayerStat>();
                if (player != null) continue;
                
                DamageInfo damageInfo = CreateDamageInfo(player.ID);
                // Deduct health of the player
                _service.PlayerManager.IncreaseScore(damageInfo.Dealer, _score);
                _service.PlayerManager.
                    GetPlayerStat(damageInfo.Target).DeductHealth(damageInfo);
            }
            ReturnToPool();
        }
    }
}