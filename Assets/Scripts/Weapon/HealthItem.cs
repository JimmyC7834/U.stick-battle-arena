using Game.Player;
using UnityEngine;

namespace Game
{
    public class HealthItem : RangedWeapon
    {
        protected override void Initialize()
        {
            OnItemUseDown += Launch;
        }
    }
}