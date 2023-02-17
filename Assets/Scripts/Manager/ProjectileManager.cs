using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectilePool = Game.GameObjectPool<Game.Projectile>;

namespace Game
{
    public enum ProjectileID
    {
        PistolBullet,
        Arrow,
        HandGrenade,
    }
    
    /**
     * Manage all the projectiles on scene
     */
    public class ProjectileManager : MonoBehaviour
    {
        private Dictionary<ProjectileID, ProjectilePool> _poolMap;

       [SerializeField]  private ProjectileIdPrefabPair[] _prefabEntries;

        private void Awake()
        {
            _poolMap = new Dictionary<ProjectileID, ProjectilePool>();

            for (int i = 0; i < _prefabEntries.Length; i++)
            {
                Transform spawnParent = new GameObject(
                    $"{_prefabEntries[i].Id} Pool").GetComponent<Transform>();
                spawnParent.SetParent(transform);
                
                _poolMap.Add(
                    _prefabEntries[i].Id,
                    new ProjectilePool(_prefabEntries[i].Prefab, spawnParent));
            }
        }

        /**
         * Get a projectile prefab from the pool and let the given
         */
        public Projectile SpawnProjectile(ProjectileID id)
        {
            ProjectilePool pool = _poolMap[id];
            Projectile projectile = pool.Get((_) => { });
            return projectile;
        }
        
        /**
         * Return the projectile gameObject to the pool
         */
        public void ReturnProjectile(ProjectileID id, Projectile projectile)
        {
            _poolMap[id].Release(projectile);
        }

        [Serializable]
        private struct ProjectileIdPrefabPair
        {
            public ProjectileID Id;
            public Projectile Prefab;
        }
    }
}