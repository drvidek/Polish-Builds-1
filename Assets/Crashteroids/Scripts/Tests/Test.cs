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
        gameManager.name = "GameManager";
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
    public IEnumerator BulletDestroysAsteroid()
    {
        testManager.SpawnAsteroidAndBullet(out Bullet bullet, out Asteroid asteroid);
        ScoreKeeper scoreKeeper = gameManager.GetComponent<ScoreKeeper>();

        asteroid.Initialise(scoreKeeper);
        yield return new WaitForSeconds(1f);

        Assert.IsTrue(asteroid == null);
        Assert.IsTrue(bullet == null);

        if (asteroid != null)
            Object.Destroy(asteroid.gameObject);
        if (bullet != null)
            Object.Destroy(bullet.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidHitIncreasesScore()
    {
        testManager.SpawnAsteroidAndBullet(out Bullet bullet, out Asteroid asteroid);

        ScoreKeeper scoreKeeper = gameManager.GetComponent<ScoreKeeper>();

        asteroid.Initialise(scoreKeeper);

        int _initialScore = scoreKeeper.Score;

        Debug.Log("Initial Score: " + _initialScore);

        yield return new WaitForSeconds(1f);

        Debug.Log("New Score: " + scoreKeeper.Score);
        Assert.IsTrue(_initialScore < scoreKeeper.Score);

        if (asteroid != null)
            Object.Destroy(asteroid.gameObject);
        if (bullet != null)
            Object.Destroy(bullet.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidHitIncreasesCombo()
    {
        testManager.SpawnAsteroidAndBullet(out Bullet bullet, out Asteroid asteroid);
        ScoreKeeper scoreKeeper = gameManager.GetComponent<ScoreKeeper>();
        asteroid.Initialise(scoreKeeper);

        int _initialCombo = scoreKeeper.Combo;

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(_initialCombo < scoreKeeper.Combo);

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

        ScoreKeeper scoreKeeper = gameManager.GetComponent<ScoreKeeper>();
        asteroid.Initialise(scoreKeeper);

        yield return new WaitForSeconds(1f);

        if (asteroid != null)
            Object.Destroy(asteroid.gameObject);
        if (player != null)
            Object.Destroy(player.gameObject);

        Assert.IsTrue(GameManager.gameState != GameState.game);
        GameManager.gameState = GameState.game;
    }

    [UnityTest]
    public IEnumerator AsteroidOffscreen()
    {
        Asteroid asteroid = testManager.GetAsteroid();
        ScoreKeeper scoreKeeper = gameManager.GetComponent<ScoreKeeper>();
        asteroid.Initialise(scoreKeeper);
        scoreKeeper.IncreaseCombo();

        asteroid.Offscreen();
        yield return null;

        Assert.IsTrue(asteroid == null);
        Assert.IsTrue(scoreKeeper.Combo == 1);
    }

    [UnityTest]
    public IEnumerator BulletOffscreen()
    {
        Bullet bullet = testManager.GetBullet();
        ScoreKeeper scoreKeeper = gameManager.GetComponent<ScoreKeeper>();
        scoreKeeper.IncreaseCombo();

        bullet.Offscreen();
        yield return null;
        
        Assert.IsTrue(bullet == null);
        Assert.IsTrue(scoreKeeper.Combo == 1);
    }
}
