#region

using System;
using UnityEngine;

#endregion

namespace Game
{
    public class FlameThrower : MeleeWeapon
    {
        [SerializeField] private float _fireInterval;
        [SerializeField] private ParticleSystem _fireEffect;
        private float _currTime;
        private bool _shooting;
        private PlayerID _shooter;

        protected override void Initialize()
        {
            OnItemUseDown += Shoot;
            OnItemUseUp += (_) => Stop();
            OnBreak += (_) => Stop();
            OnHold += (_) => Stop();
            _fireEffect.gameObject.SetActive(false);
        }

        private void Update()
        {
            base.Update();
            if (!_shooting) return;
            
            if (_currTime > 0)
            {
                _currTime -= Time.deltaTime;
            }
            else
            {
                Attack(_shooter);
                _currTime = _fireInterval;
            }
        }

        private void Shoot(PlayerID shooter)
        {
            _shooting = true;
            _shooter = shooter;
            _currTime = 0; // immediate shoot
            _fireEffect.gameObject.SetActive(true);
        }

        private void Stop()
        {
            _shooting = false;
            _fireEffect.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _fireEffect.gameObject.SetActive(false);
        }
    }
}