using System;
using UnityEngine;

namespace Game.Stage
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform[] _targetPoints;
        [SerializeField] private bool _cycle;
        [SerializeField] private float _speed;
        private ArraySelector<Transform> _pointSelector;
        private Transform _prev;
        private Transform _transform;
        private float _startTime;
        private float _distance;

        private void Awake()
        {
            _transform = transform;
            
            if (_targetPoints.Length < 2)
            {
                Debug.LogError("Invalid Moving Platform setup");
                return;
            }
            
            if (!_cycle)
            {
                // setup back and forth route
                Transform[] returnPointArr = new Transform[_targetPoints.Length * 2 - 2];
                for (int i = 0; i < _targetPoints.Length; i++)
                    returnPointArr[i] = _targetPoints[i];
                for (int i = 1; i < _targetPoints.Length - 1; i--)
                    returnPointArr[_targetPoints.Length + i] = 
                        _targetPoints[_targetPoints.Length - 1 - i];
                _targetPoints = returnPointArr;
            }

            _pointSelector = new ArraySelector<Transform>(_targetPoints);
            NextPoint();
        }

        private void FixedUpdate()
        {
            float rate = (Time.time - _startTime) * _speed / _distance;
            if (rate >= 1)
            {
                NextPoint();
                rate = 0;
            }

            _transform.position = 
                Vector2.Lerp(
                    _prev.position, 
                    _pointSelector.GetCurrent().transform.position, 
                    rate);
        }

        private void NextPoint()
        {
            // _transform.position = _pointSelector.GetCurrent().transform.position;
            _prev = _pointSelector.GetCurrent();
            _pointSelector.Next();
            _distance = Vector2.Distance(
                _prev.position,
                _pointSelector.GetCurrent().position);
            _startTime = Time.time;
        }

        private void OnDrawGizmos()
        {
            if (_targetPoints == null) return;
            for (int i = 0; i < _targetPoints.Length - 1; i++)
                 Gizmos.DrawLine(_targetPoints[i].position, _targetPoints[i + 1].position);
            if (_cycle)
                Gizmos.DrawLine(_targetPoints[0].position,
                    _targetPoints[_targetPoints.Length - 1].position);
        }
    }
}