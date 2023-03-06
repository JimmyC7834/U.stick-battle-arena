using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class JumpPad : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            Rigidbody2D target = col.gameObject.GetComponent<Rigidbody2D>();
            target.AddForce(new Vector3(0, 100, 0));
        }
    }
}

