using UnityEngine;
using System;


namespace Game
{
    /*
    * Basic JumpPad script that cancels y-axis velocity and applies y-axis force to rigid bodies upon collision
    */
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class JumpPad : MonoBehaviour
    {

        // Strength of the jump pad, force added in the upwards direction
        [SerializeField] private float _jumpStrength = 500f;
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private float _visualShakeDistance;
        [SerializeField] private float _visualShakeReturnSpeed;
        private Vector3 _origin;

        private void Awake()
        {
            _origin = _visualTransform.position;
        }

        private void Update()
        {
            _visualTransform.position = Vector2.Lerp(
                _visualTransform.position, 
                _origin, 
                _visualShakeReturnSpeed * Time.deltaTime);
        }

        // Detect anything with rigidBody component
        private void OnTriggerEnter2D(Collider2D col)
        {
            Rigidbody2D target = col.gameObject.GetComponent<Rigidbody2D>();
            // Cancel out vertical velocity
            target.velocity = new Vector3(target.velocity.x, Math.Abs(target.velocity.y), 0);
            // Add upwards force
            target.AddForce(new Vector3(0, _jumpStrength, 0));
            _visualTransform.position += _visualShakeDistance * Vector3.down;
        }
    }
}

