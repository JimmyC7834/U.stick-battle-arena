#region

using Game.Player;
using UnityEngine;

#endregion

namespace Game
{
    public class Rocket : Projectile
    {
        [SerializeField] private float _splashRadius;
        [SerializeField] private GameObject _explosionVisual;

        private void Start()
        {
            OnHitPlayer += (_) => Explode();
            OnHitStage += Explode;
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
            
            ReturnToPool();
        }
    }
}