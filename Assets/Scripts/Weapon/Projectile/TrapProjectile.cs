using System;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class TrapProjectile : Projectile
    {
        [SerializeField] private float _splashRadius;
        
        private void Start()
        {
            OnHitPlayer += Explode;
            OnHitStage += Stick;
        }
        
        private void Explode(DamageInfo damageInfo)
        {
            if (damageInfo.Dealer == damageInfo.Target) return;

            // Increase score of the dealer if hit
            _service.PlayerManager.IncreaseScore(damageInfo.Dealer, _score);
            // Deduct health of the hit player
            _service.PlayerManager.
                GetPlayerStat(damageInfo.Target).DeductHealth(damageInfo);
            ReturnToPool();
        }

        private void Stick()
        {
            Rigidbody2D _rigidbody;
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.gravityScale = 0f;
        }
    }
}