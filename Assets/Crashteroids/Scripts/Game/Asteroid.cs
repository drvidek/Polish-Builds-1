using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _spdMin = 4f;
    [SerializeField] float _spdMax = 10f;
    float _spdCurrent;
    ScoreKeeper _scoreKeeper;

    public void Initialise(ScoreKeeper scoreKeeper)
    {
        _spdCurrent = Random.Range(_spdMin, _spdMax);
        _scoreKeeper = scoreKeeper;
    }    

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
        transform.position += Vector3.down * _spdCurrent * Time.deltaTime;
    }

    void CheckOffscreen()
    {
        if (Camera.main != null)
        {
            Vector3 _screenPos = Camera.main.WorldToViewportPoint(transform.position);
            if (_screenPos.y < -0.05f)
            {
                Offscreen();
            }
        }
    }

    public void Offscreen()
    {
        _scoreKeeper.ResetCombo();
        EndOfLife(true);
    }

    public void AddScore()
    {
        _scoreKeeper.IncreaseScore(1, transform.position);
        _scoreKeeper.IncreaseCombo();
        Debug.Log("Score Update Sent");
    }

    public void EndOfLife(bool remove)
    {
        if (remove && GameManager._asteroidList.Contains(this))
            GameManager._asteroidList.Remove(this);
        Destroy(this.gameObject);
    }
}
