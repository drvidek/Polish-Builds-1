using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test
{
    TestManager testManager;
    GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        testManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/TestManager").GetComponent<TestManager>());
        gameManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager").GetComponent<GameManager>());
        GameManager.gameState = GameState.game;
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(gameManager.gameObject);
        Object.Destroy(testManager.gameObject);
    }

    [UnityTest]
    public IEnumerator PlayerMoveRight()
    {
        PlayerMove player = testManager.GetPlayer();

        float _startXPos = player.transform.position.x;

        player.Move(1, 0);
        yield return null;

        Assert.Greater(player.transform.position.x, _startXPos);

        Object.Destroy(player.gameObject);
    }

    [UnityTest]
    public IEnumerator PlayerMoveLeft()
    {
        PlayerMove player = testManager.GetPlayer();

        float _startXPos = player.transform.position.x;

        player.Move(-1, 0);
        yield return null;

        Assert.Less(player.transform.position.x, _startXPos);

        Object.Destroy(player.gameObject);
    }

    [UnityTest]
    public IEnumerator BulletHitsAsteroid()
    {
        Bullet bullet = testManager.GetBullet();
        bullet.transform.position = Vector3.down * 3f;

        Asteroid asteroid = testManager.GetAsteroid();
        asteroid.transform.position = Vector3.up * 3f;

        yield return new WaitForSeconds(2f);

        Assert.IsTrue(asteroid == null);
        Assert.IsTrue(bullet == null);

        if (asteroid != null)
            Object.Destroy(asteroid.gameObject);
        if (bullet != null)
            Object.Destroy(bullet.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidHitsPlayer()
    {
        Asteroid asteroid = testManager.GetAsteroid();
        asteroid.transform.position = Vector3.up * 2f;
        PlayerMove player = testManager.GetPlayer();
        player.transform.position = Vector3.down;

        yield return new WaitForFixedUpdate();

        yield return new WaitForSeconds(2f);

        if (asteroid != null)
            Object.Destroy(asteroid.gameObject);
        if (player != null)
            Object.Destroy(player.gameObject);

        Assert.IsTrue(GameManager.gameState != GameState.game);

        GameManager.gameState = GameState.game;

        yield return null;

    }
}
