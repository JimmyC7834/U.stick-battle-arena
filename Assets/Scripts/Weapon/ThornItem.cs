using Game.Player;
using UnityEngine;

namespace Game
{
    public class ThornItem : RangedWeapon
    {
        protected override void Initialize()
        {
            OnItemUseDown += Launch;
        }
    }
}