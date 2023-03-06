using Game.Player;
using UnityEngine;

namespace Game
{
    public class MineItem : RangedWeapon
    {
        protected override void Initialize()
        {
            OnItemUseDown += Launch;
        }
    }
}