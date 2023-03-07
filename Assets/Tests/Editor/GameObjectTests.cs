using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameObjectTests
{
    //Tests GameObject Naming Success
    [Test]
    public void GameObjectWillHaveTheSameName()
    {
        var go = new GameObject("MyGameObject");
        Assert.AreEqual("MyGameObject", go.name);
    }
    
    [Test]
    public void GameObjectIsActive()
    {
        var go = new GameObject();
        go.SetActive(true);
        Assert.IsTrue(go.activeSelf);
    }

    [Test]
    public void DefaultGameObjectEmptyTransform()
    {
        var go = new GameObject();
        Assert.IsEmpty(go.transform);
    }

    [Test]
    public void GameObjectRigidBodyHasPosition()
    {
        var go = new GameObject();
        go.AddComponent<Rigidbody2D>();
        Assert.IsNotNull(go.transform.position.x);
    }
}
