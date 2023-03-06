using Game.Player;
using UnityEngine;

namespace Game
{
    public abstract class MeleeWeapon : UsableItem
    {
        public int Damage => _damage;
        [SerializeField] private int _damage;
        [SerializeField] private float _rayCastRadius;
        [SerializeField] private Transform _hitBoxOrigin;

        protected void Attack(PlayerID attacker)
        {
            _service.AudioManager.PlayAudio(_audioOnUse);
            ReduceDurability(1);
            Collider2D col = Physics2D.OverlapCircle(_hitBoxOrigin.position, _rayCastRadius);
            if (col == null) return;
            
            // check if hit a player
            PlayerStat target = col.GetComponent<PlayerStat>();
            if (target != null && target.ID != attacker)
            {
                // Deduct health of the hit other player
                target.DeductHealth(
                    new DamageInfo(
                        attacker,
                        target.ID,
                        _damage,
                        this));
            }
        }
    }
}