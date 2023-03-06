using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class UI_MapButton : MonoBehaviour
    {
        public SceneID SceneID { get => _sceneID; }
        public Sprite PreviewImage { get => _previewImage; }
        public Button Button { get => _button; }
        [SerializeField] private Button _button;
        [SerializeField] private SceneID _sceneID;
        [SerializeField] private Sprite _previewImage;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
    }
}