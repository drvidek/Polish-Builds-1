using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _shotDelayMax = 0.5f;
    float _shotDelay = 0;
    [SerializeField] GameObject _bulletPrefab;

    void Update()
    {
        if (GameManager.IsPlaying())
        {
            if (!Shoot())
                ManageShotDelay();
        }

    }

    bool Shoot()
    {
        if (Input.GetButton("Shoot") && _shotDelay == 0)
        {
            Bullet bullet = Instantiate(_bulletPrefab, transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<Bullet>();
            bullet.Initialise();
            _shotDelay = _shotDelayMax;
            return true;
        }
        else
            return false;
    }

    void ManageShotDelay()
    {
        _shotDelay = MathExt.Approach(_shotDelay, 0, Time.deltaTime);
    }
}
