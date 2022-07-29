using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test
{
    GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        gameManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager").GetComponent<GameManager>());
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(gameManager.gameObject);
    }

    [UnityTest]
    public IEnumerator PlayerMoveRight()
    {
        PlayerMove player = gameManager.GetPlayer();

        float _startXPos = player.transform.position.x;

        player.Move(1, 0);
        yield return null;

        Assert.Greater(player.transform.position.x, _startXPos);
        
        Object.Destroy(player.gameObject);
    }

    [UnityTest]
    public IEnumerator PlayerMoveLeft()
    {
        PlayerMove player = gameManager.GetComponent<GameManager>().GetPlayer();

        float _startXPos = player.transform.position.x;

        player.Move(-1, 0);
        yield return null;

        Assert.Less(player.transform.position.x, _startXPos);
     
        Object.Destroy(player.gameObject);
    }

    [UnityTest]
    public IEnumerator TestNull()
    {
        PlayerMove player = null;

        yield return null;

        Assert.IsNull(player);
    }
}
