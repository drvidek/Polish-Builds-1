using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _spd = 4f;

    void Update()
    {
        Move();
        CheckOffscreen();
    }

    void Move()
    {
        transform.position += Vector3.down * _spd * Time.deltaTime;
    }

    void CheckOffscreen()
    {
        Vector3 _screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (_screenPos.y < -0.1f)
            EndOfLife();
    }

    public void EndOfLife()
    {
        Destroy(this.gameObject);
    }
}
