using System;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class Thorn : Projectile
    {
        [SerializeField] private float _durationTime;
        [SerializeField] private float _thrust;

        private float _currTime;
        private Rigidbody2D _rigidbody;
        
        private void Start()
        {
            OnHitStage += Stick;
            OnHitPlayer += DealDamage;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _currTime = _durationTime;
        }

        private void Update()
        {
            if (_currTime > 0)
            {
                _currTime -= Time.deltaTime;
            }
            else
            {
                ReturnToPool();
            }
        }

        private void Stick()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            _rigidbody.gravityScale = 0f;
            _hit = false; // let the collider still active to detect collision
        }

        private void DealDamage(DamageInfo damageInfo)
        {
            // Increase score of the dealer if hit
            _service.PlayerManager.IncreaseScore(damageInfo.Dealer, _score);
            // Deduct health of the hit player
            _service.PlayerManager.
                GetPlayerStat(damageInfo.Target).DeductHealth(damageInfo);
            
            KnockBack(damageInfo);

            _hit = false;
        }

        private void KnockBack(DamageInfo damageInfo)
        {
            PlayerMovement target = _service.PlayerManager.
                GetPlayerStat(damageInfo.Target).GetComponent<PlayerMovement>();

            Vector2 force;
            
            if (target.IsFacingLeft)
            {
                force = Vector2.right * _thrust;
            }
            else
            {
                force = Vector2.left * _thrust;
            }
            
            target.RB.AddForce(force, ForceMode2D.Impulse);
        }
    }
}