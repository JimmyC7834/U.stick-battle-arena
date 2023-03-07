using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class UI_HoverButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private GameService _gameService;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _gameService.AudioManager.PlayAudio(AudioID.Hover);
        }
    }
}