#region

using System;
using Game.Player;
using UnityEngine;

#endregion

namespace Game
{
    public class Grenade : Projectile
    {
        [SerializeField] private float _splashRadius;
        [SerializeField] private float _explodeTime;
        [SerializeField] private GameObject _explosionVisual;

        private float _currTime;
        
        private void Start()
        {
            _currTime = _explodeTime;
            OnHitStage += HandleHitStage;
            OnHitProjectile += _ => HandleHitStage();
            OnHitPlayer += (_) => Explode();
        }

        private void Update()
        {
            if (_currTime > 0)
            {
                _currTime -= Time.deltaTime;
            }
            else
            {
                Explode();
                _currTime = _explodeTime;
            }
        }

        private void HandleHitStage()
        {
            _hit = false;
        }

        private void Explode()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                transform.position, 
                _splashRadius);
            foreach (Collider2D hit in hits)
            {
                PlayerStat player = hit.GetComponent<PlayerStat>();
                if (player == null) continue;
                
                DamageInfo damageInfo = CreateDamageInfo(player.ID);
                // Deduct health of the player
                if (damageInfo.Dealer != damageInfo.Target)
                    _service.PlayerManager.IncreaseScore(damageInfo.Dealer, _score);
                _service.PlayerManager.
                    GetPlayerStat(damageInfo.Target).DeductHealth(damageInfo);
            }
            
            GameObject explosion = Instantiate(_explosionVisual, transform).gameObject;
            explosion.transform.position = transform.position;
            explosion.transform.SetParent(null);
            _service.AudioManager.PlayAudio(AudioID.Explosion);

            ReturnToPool();
        }
    }
}