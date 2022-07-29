using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _spd = 4f;

    void FixedUpdate()
    {
        if (GameManager.IsPlaying())
        {
            Move();
            CheckOffscreen();
        }
    }

    void Move()
    {
        transform.position += Vector3.down * _spd * Time.deltaTime;
    }

    void CheckOffscreen()
    {
        if (Camera.main != null)
        {
            Vector3 _screenPos = Camera.main.WorldToViewportPoint(transform.position);
            if (_screenPos.y < -0.1f)
                EndOfLife(true);
        }
    }

    public void EndOfLife(bool remove)
    {
        if (remove)
            GameManager._asteroidList.Remove(this);
        Destroy(this.gameObject);
    }
}
