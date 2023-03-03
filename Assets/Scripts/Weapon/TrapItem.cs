using Game.Player;
using UnityEngine;

namespace Game
{
    public class TrapItem : RangedWeapon
    {
        protected override void Initialize()
        {
            OnItemUseDown += Launch;
        }
    }
}