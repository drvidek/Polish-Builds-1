using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerPrefab;
    [SerializeField] private float _asteroidSpawnTimerMax = 2f;
    float _asteroidSpawnTimer;
    [SerializeField] GameObject _asteroidPrefab;

    public PlayerMove GetPlayer()
    {
        return Instantiate<PlayerMove>(_playerPrefab);
    }

    private void Update()
    {
        _asteroidSpawnTimer = MathExt.Approach(_asteroidSpawnTimer, 0, Time.deltaTime);
        if (_asteroidSpawnTimer == 0)
        {
            Vector3 _viewPoint = new Vector3(Random.Range(0f, 1f), 1.1f);
            Vector3 _spawnPos = Camera.main.ViewportToWorldPoint(_viewPoint);
            _spawnPos.z = 0;
            Instantiate(_asteroidPrefab, _spawnPos, new Quaternion(0, 0, 0, 0));
            _asteroidSpawnTimer = _asteroidSpawnTimerMax;
        }
    }
}
