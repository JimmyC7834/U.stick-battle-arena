#region

using System;
using System.Collections.Generic;
using Game.DataSet;
using UnityEngine;
using ProjectilePool = Game.GameObjectPool<Game.Projectile>;

#endregion

namespace Game
{
    public enum ProjectileID
    {
        PistolBullet = 1,
        Arrow = 2,
        SniperBullet = 3,
        SMGBullet = 4,
        ShotgunBullet = 5,
        HandGrenade = 6,
        MinigunBullet = 7,
        Thorn = 8,
        Mine = 9,
        Rocket = 10,
        Grenade = 11,
    }

    /**
     * Manage all the projectiles on scene
     */
    public class ProjectileManager : MonoBehaviour
    {
        public static ProjectileID[] ProjectileIDs =
        {
            ProjectileID.Arrow, ProjectileID.HandGrenade, 
            ProjectileID.PistolBullet, ProjectileID.ShotgunBullet, 
            ProjectileID.SniperBullet, ProjectileID.SMGBullet, ProjectileID.MinigunBullet,
            ProjectileID.Thorn, ProjectileID.Mine, ProjectileID.Rocket, ProjectileID.Grenade
        };
        
        private Dictionary<ProjectileID, ProjectilePool> _poolMap;

        [SerializeField]  private ProjectileDataSetSO _projectileData;

        private void Awake()
        {
            _poolMap = new Dictionary<ProjectileID, ProjectilePool>();

            foreach (ProjectileID id in ProjectileIDs)
            {
                Transform spawnParent = new GameObject(
                    $"{id} Pool").GetComponent<Transform>();
                spawnParent.SetParent(transform);
                
                _poolMap.Add(id, new ProjectilePool(_projectileData[id], spawnParent));
            }
        }

        /**
         * Get a projectile prefab from the pool and launch it on the stage with the given launch info
         */
        public Projectile SpawnAndLaunch(ProjectileID id, RangedWeapon.LaunchInfo launchInfo)
        {
            ProjectilePool pool = _poolMap[id];
            return pool.Get((projectile) => { 
                projectile.transform.position = launchInfo.Origin;
                projectile.Launch(launchInfo.Velocity, launchInfo.Gravity, launchInfo.Shooter);
            });
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