using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerPrefab;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Asteroid _asteroidPrefab;
    
    public PlayerMove GetPlayer()
    {
        return Instantiate<PlayerMove>(_playerPrefab);
    }

    public Bullet GetBullet()
    {
        return Instantiate<Bullet>(_bulletPrefab);
    }

    public Asteroid GetAsteroid()
    {
        return Instantiate<Asteroid>(_asteroidPrefab);
    }
}
