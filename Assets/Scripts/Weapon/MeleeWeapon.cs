#region

using System.Collections;
using Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace Game
{
    public abstract class MeleeWeapon : UsableItem
    {
        public int Damage => _damage;
        public int Score => _score;
        public bool IsAvailable = true;
        [SerializeField] private float _cooldownDuration;
        [SerializeField] private int _damage;
        [SerializeField] private int _score;
        [SerializeField] private float _thrust;
        [SerializeField] private float _rayCastRadius;
        [SerializeField] private Transform _hitBoxOrigin;
        
        private Vector2 _knockbackDirection;

        protected void Attack(PlayerID attacker)
        {
            if (IsAvailable == false) return;
            _service.AudioManager.PlayAudio(_audioOnUse);
            ReduceDurability(1);
            
            Collider2D[] cols = Physics2D.OverlapCircleAll(_hitBoxOrigin.position, _rayCastRadius);
            if (cols == null) return;

            foreach (Collider2D col in cols)
            {
                // check if hit a player
                PlayerStat target = col.GetComponent<PlayerStat>();
                if (target != null && target.ID != attacker)
                {
                    // Deduct health of the hit other player
                    DamageInfo damageInfo = new DamageInfo(
                        attacker,
                        target.ID,
                        _damage,
                        this);
                    target.DeductHealth(damageInfo);
                
                    // Add Score to the attacker
                    _service.PlayerManager.IncreaseScore(attacker, _score);
                    KnockBack(damageInfo);
                }
            }
            
            StartCoroutine((StartCooldown()));
        }

        private void KnockBack(DamageInfo damageInfo)
        {
            
            PlayerMovement target = _service.PlayerManager.
                GetPlayerStat(damageInfo.Target).GetComponent<PlayerMovement>();
            PlayerMovement attacker = _service.PlayerManager.
                GetPlayerStat(damageInfo.Dealer).GetComponent<PlayerMovement>();

            Vector2 force;
            
            if (!attacker.IsFacingLeft)
            {
                force = Vector2.right * _thrust;
            }
            else
            {
                force = Vector2.left * _thrust;
            }
            
            target.RB.velocity += force * _thrust;
        }

        private IEnumerator StartCooldown()
        {
            IsAvailable = false;
            yield return new WaitForSeconds(_cooldownDuration);
            IsAvailable = true;
        }
    }
}