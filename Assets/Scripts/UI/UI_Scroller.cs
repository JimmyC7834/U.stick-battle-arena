#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Game.UI
{
    public class UI_Scroller : MonoBehaviour
    {
        [SerializeField] private RawImage _image;
        [SerializeField] private float _speed;
        [SerializeField] private float _x, _y;

        private void Update()
        {
            _image.uvRect = new Rect(
                _image.uvRect.position + 
                _speed * Time.deltaTime * new Vector2(_x, _y).normalized,
                _image.uvRect.size);
        }
    }
}