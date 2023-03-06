using UnityEngine;

namespace Game
{
    public class Sniper : RangedWeapon
    {
        [SerializeField] private float _recoilForce;
         
        protected override void Initialize()
        {
            OnItemUseDown += (id) =>
            {
                _flipParent.
                    GetComponent<Rigidbody2D>().AddForce(
                         Vector2.one * _flipParent.localScale.x * _recoilForce);
                Launch(id);
            };
        }
    }
}