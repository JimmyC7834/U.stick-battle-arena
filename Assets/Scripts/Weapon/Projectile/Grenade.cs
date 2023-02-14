using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game 
{
    [SerializeField] private GameplayService _service;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private int _damage;
    [SerializeField] private float _score;
    
    public class Grenade : RangedWeapon
    {
        private void Awake()
    {
    _usableItem = GetComponent<UsableItem>();
    _usableItem.OnUseButtonDown += Throw;
    }
        private void Throw(PlayerController executor)
        {
            Projectile grenade = service.ProjectileManager.
                SpawnProjectile(_projectileID);

            grenade.transform.position = _shootingPoint.position;

                // flip velocity if facing different direction
                // do we want to have different velocity for grenade and if so
                // how do I go about that
                Vector2 velocity = BulletVelocity;
                if (_usableItem.Player.FacingLeft)
                    velocity = new Vector2(-velocity.x, velocity.y);
                
                grenade.Launch(_projectileID, velocity, executor, BulletGravity, BulletLifespan);
                // How do I go about changing a grenade's durability to be 1?
                _usableItem.ReduceDurability(1);
        }
    }
}