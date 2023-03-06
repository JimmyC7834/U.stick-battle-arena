using UnityEngine;

namespace Game.Stage
{
    [RequireComponent(typeof(Collider2D))]
    public class WrapZone : MonoBehaviour
    {
        [SerializeField] private Vector3 _wrapOffset;
        [SerializeField] private bool _warpExactly;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_warpExactly)
            {
                col.transform.position = _wrapOffset;
                return;
            }

            col.transform.position += _wrapOffset;
        }
    }
}