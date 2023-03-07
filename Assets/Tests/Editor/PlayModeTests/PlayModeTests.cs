using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

public class PlayModeTests
{
    [UnityTest]
    public IEnumerator GameObjectRigidBodyAffectedByPhysics()
    {
        var go = new GameObject();
        go.AddComponent<Rigidbody2D>();
        var originalPosition = go.transform.position.y;

        yield return new WaitForFixedUpdate();

        Assert.AreNotEqual(originalPosition, go.transform.position.y);
    }
}
