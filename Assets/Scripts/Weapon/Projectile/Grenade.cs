using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

namespace Game
{
    [RequireComponent(typeof(Projectile))]
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private GameplayService _service;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _damage;
        [SerializeField] private float _score;
        private float _explosionRadius = 5f;
        private float _explosionForce = 1000f;
        private float _timeToExplode = 3f;
        private bool _exploded = false;

        private void Awake()
        {
            _projectile = GetComponent<Projectile>();
            _projectile.OnHitPlayer += HandleHitPlayer;
            _projectile.OnHitStage += HandleHitStage;
        }

        private void Update()
        {
            if (!_exploded && Time.time >= _timeToExplode)
            {
                Explode();
            }
        }

        private void HandleHitPlayer(PlayerController target, PlayerController dealer)
        {
            // Grenade doesn't detonate on player hit
            ReturnToPool();
        }

        private void HandleHitStage()
        {
            // Grenade detonates on stage hit
            Explode();
        }

        private void Explode()
        {
            _exploded = true;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    PlayerController target = hit.GetComponent<PlayerController>();
                    PlayerController dealer = _projectile.Owner;
                    
                    // Increase score of the dealer if hit
                    _service.PlayerManager.IncreaseScore(dealer.Stat.ID, _score);
                    // Deduct health of the hit player
                    target.Stat.DeductHealth(
                        dealer.Stat.ID, 
                        new DamageInfo(
                            dealer.Stat.ID,
                            target.Stat.ID,
                            _damage,
                            null));
                }
                else if (hit.CompareTag("Stage"))
                {
                    Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                    }
                }
            }

            ReturnToPool();
        }

        private void ReturnToPool()
        {
            _projectile.ReturnToPool();
        }
    }
}