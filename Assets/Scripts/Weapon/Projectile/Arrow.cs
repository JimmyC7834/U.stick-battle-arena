﻿using System;
using Game.Player;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Projectile))]
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private GameplayService _service;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Pickable _pickable;
        [SerializeField] private float _damage;
        [SerializeField] private float _score;
        [SerializeField] private Transform _visualTransform;
        
        private void Awake()
        {
            _projectile = GetComponent<Projectile>();
            _pickable = GetComponent<Pickable>();
            _projectile.OnHitPlayer += HandleHitPlayer;
            _projectile.OnHitStage += HandleHitStage;
        }

        private void Update()
        {
            // rotate arrow based on velocity
            Vector2 v = _pickable.Rigidbody.velocity;
            _visualTransform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg, 
                Vector3.forward);
        }

        private void HandleHitPlayer(PlayerController player, PlayerController executor)
        {
            // Increase score of the dealer if hit
            _service.PlayerManager.IncreaseScore(executor.Stat.ID, _score);
            // Deduct health of the hit player
            player.Stat.DeductHealth(executor.Stat.ID, _damage);
            ReturnToPool();
        }
        
        private void ReturnToPool()
        {
            _projectile.ReturnToPool();
        }
        
        // wall / floor hits
        private void HandleHitStage() => ReturnToPool();
    }
}