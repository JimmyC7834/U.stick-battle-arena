using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI
{
    public class UI_SceneTransition : MonoBehaviour
    {
        [SerializeField] private GameService _gameService;
        [SerializeField] private RectTransform _cutOutTrans;
        [SerializeField] private Vector2 _openSize;
        [SerializeField] private float _speed;

        private void Awake()
        {
            _gameService.ProvideSceneTransition(this);
        }

        public void CloseScene(UnityAction callback)
        {
            StartCoroutine(PerformCloseScene(callback));
        }
        
        public void OpenScene(UnityAction callback)
        {
            StartCoroutine(PerformOpenScene(callback));
        }

        private IEnumerator PerformCloseScene(UnityAction callback)
        {
            _cutOutTrans.sizeDelta = _openSize;
            while (_cutOutTrans.sizeDelta.x > 1)
            {
                _cutOutTrans.sizeDelta = Vector3.Lerp(
                    _cutOutTrans.sizeDelta, Vector3.zero, _speed * Time.deltaTime);
                yield return null;
            }
            
            _cutOutTrans.sizeDelta = Vector3.zero;
            callback?.Invoke();
        }
        
        private IEnumerator PerformOpenScene(UnityAction callback)
        {
            _cutOutTrans.sizeDelta = Vector3.zero;
            while (_cutOutTrans.sizeDelta.x < _openSize.x * .9f)
            {
                _cutOutTrans.sizeDelta = Vector3.Lerp(
                    _cutOutTrans.sizeDelta, _openSize, _speed * Time.deltaTime);
                yield return null;
            }
            
            _cutOutTrans.sizeDelta = _openSize;
            callback?.Invoke();
        }
    }
}