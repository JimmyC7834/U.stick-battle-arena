using System;
using System.Drawing;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class Mine : Projectile
    {
        [SerializeField] private float _durationTime;
        [SerializeField] private float _thrust;
        [SerializeField] private float _splashRadius;

        private float _currTime;
        private Rigidbody2D _rigidbody;
        
        private void Start()
        {
            OnHitStage += Stick;
            OnHitPlayer += (_) => Explode();
            
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

        private void Explode()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                transform.position, 
                _splashRadius);
            Debug.Log(hits);
            foreach (Collider2D hit in hits)
            {
                PlayerStat player = hit.GetComponent<PlayerStat>();
                if (player == null) continue;
                
                DamageInfo damageInfo = CreateDamageInfo(player.ID);
                // Deduct health of the player
                if (damageInfo.Target != damageInfo.Dealer)
                    _service.PlayerManager.IncreaseScore(damageInfo.Dealer, _score);

                _service.PlayerManager.
                    GetPlayerStat(damageInfo.Target).DeductHealth(damageInfo);
                
                KnockBack(damageInfo);
            }
            ReturnToPool();
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