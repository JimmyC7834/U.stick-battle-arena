#region

using UnityEngine;

#endregion

namespace Game
{
    public class Sniper : RangedWeapon
    {
        protected override void Initialize()
        {
            OnItemUseDown += (id) =>
            { 
                Launch(id);
            };
        }
    }
}