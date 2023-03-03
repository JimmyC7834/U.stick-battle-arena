using System;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class HealthProjectile : Projectile
    {
        [SerializeField] private float _splashRadius;
        
        private void Start()
        {
            OnHitPlayer += HandleHealPlayer;
        }

        private void HandleHealPlayer(DamageInfo damageInfo)
        {
            // Can only heal yourself
            if (damageInfo.Dealer != damageInfo.Target) return;
            
            // Increase health of the hit player
            _service.PlayerManager.
                GetPlayerStat(damageInfo.Target).IncreaseHealth(damageInfo);
            ReturnToPool();
        }
    }
}