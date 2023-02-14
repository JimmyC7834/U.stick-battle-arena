using Game.Player;
using UnityEngine;

namespace Game 
{
 public class HandGrenadeWeapon : RangedWeapon
    {
        private void Awake()
        {
            _usableItem = GetComponent<UsableItem>();
            _usableItem.OnUseButtonDown += ThrowGrenade;
        }

        private void ThrowGrenade(PlayerController executor)
        {
            Projectile grenade = _service.ProjectileManager.SpawnProjectile(_projectileID);
            grenade.transform.position = _shootingPoint.position;

            // flip velocity if facing different direction
            Vector2 velocity = BulletVelocity;
            if (_usableItem.Player.FacingLeft)
            {
                velocity = new Vector2(-velocity.x, velocity.y);
            }
            grenade.Launch(_projectileID, velocity, executor, BulletGravity, BulletLifespan);

            // Set grenade properties
            HandGrenade grenadeComponent = grenade.GetComponent<HandGrenade>();
            grenadeComponent.ExplosionRadius = 5f;
            grenadeComponent.ExplosionForce = 1000f;
            grenadeComponent.TimeToExplode = Time.time + 3f;
            grenadeComponent.Damage = 50;
            grenadeComponent.Score = 10;

            _usableItem.ReduceDurability(1);
        }
    }
}