using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { pregame, game, postgame, pause }

public class GameManager : MonoBehaviour
{
    public static GameState gameState;
    public static bool endGame;
    [Header("Player")]
    [SerializeField] private static PlayerMove _player;
    public static PlayerMove Player { set { _player = value; } }
    public static List<Bullet> _bulletList = new List<Bullet>();
    [Header("Asteroid Spawn")]
    [SerializeField] private float _asteroidSpawnTimerMax = 2f;
    float _asteroidSpawnTimer;
    [SerializeField] private GameObject _asteroidPrefab;
    public static List<Asteroid> _asteroidList = new List<Asteroid>();
    [Header("UI")]
    [SerializeField] private GameObject _endPanel;

    private void Start()
    {
        if (_endPanel != null)
            _endPanel.SetActive(false);
        gameState = GameState.game;
    }

    private void Update()
    {
        if (endGame)
        {
            EndRound();
        }

        if (IsPlaying() && Camera.main != null)
        {
            _asteroidSpawnTimer = MathExt.Approach(_asteroidSpawnTimer, 0, Time.deltaTime);
            if (_asteroidSpawnTimer == 0)
            {
                Vector3 _viewPoint = new Vector3(Random.Range(0f, 1f), 1.1f);
                Vector3 _spawnPos = Camera.main.ViewportToWorldPoint(_viewPoint);
                _spawnPos.z = 0;
                Asteroid asteroid = Instantiate(_asteroidPrefab, _spawnPos, new Quaternion(0, 0, 0, 0)).GetComponent<Asteroid>();
                _asteroidList.Add(asteroid);
                _asteroidSpawnTimer = _asteroidSpawnTimerMax;
            }
        }
    }

    public static bool IsPlaying()
    {
        return gameState == GameState.game;
    }

    public void StartRound()
    {
        if (_endPanel != null)
            _endPanel.SetActive(false);
        gameState = GameState.game;
        _player.Initialise();
    }

    public void EndRound()
    {
        endGame = false;
        foreach (Asteroid asteroid in _asteroidList)
        {
            if (asteroid != null)
                asteroid.EndOfLife(false);
        }
        _asteroidList.Clear();

        foreach (Bullet bullet in _bulletList)
        {
            if (bullet != null)
                bullet.EndOfLife(false);
        }
        _bulletList.Clear();

        if (_endPanel != null)
            _endPanel.SetActive(true);

        gameState = GameState.postgame;
    }

}
