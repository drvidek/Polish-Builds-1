using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _spd = 15f;
    [SerializeField] private float _rad = 0.5f;
    [SerializeField] private Renderer _rend;
    [SerializeField] private float _scanDist;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        Initialise();
    }

    public void Initialise()
    {
        _rad = transform.localScale.x / 2f;
        GameManager._bulletList.Add(this);
    }

    // Update is called once per frame
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
        float _travelDist = _spd * Time.fixedDeltaTime;
        Vector3 newPos = transform.position + (Vector3.up * _travelDist);
        RaycastHit hit;
        Ray hitRay = new Ray(transform.position, Vector3.up);

        if (Physics.SphereCast(hitRay, _rad, out hit, _scanDist))
        {
            CheckHit(hit);
        }

        transform.position = newPos;
    }

    void CheckHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Asteroid>(out Asteroid hitObj))
        {
            hitObj.AddScore();
            hitObj.EndOfLife(true);
            transform.position = hit.point;
            EndOfLife(true);
        }
    }

    void CheckOffscreen()
    {
        if (Camera.main != null)
        {
            Vector3 _screenPos = Camera.main.WorldToViewportPoint(transform.position);
            if (_screenPos.y > 1.05f)
            {
                Offscreen();
            }
        }
    }

    public void Offscreen()
    {
        ScoreKeeper scoreKeeper = GameObject.Find("GameManager").GetComponent<ScoreKeeper>();
        scoreKeeper.ResetCombo();
        EndOfLife(true);
    }

    public void EndOfLife(bool remove)
    {
        if (remove && GameManager._bulletList.Contains(this))
            GameManager._bulletList.Remove(this);
        Destroy(this.gameObject);
    }
}
