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
    }

    public void Initialise()
    {
        //_rad = transform.localScale.x / 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckOffscreen();
    }

    void Move()
    {
        float _travelDist = _spd * Time.deltaTime;
        Vector3 newPos = transform.position + (Vector3.up * _travelDist);
        RaycastHit hit;
        Ray hitRay = new Ray(transform.position, Vector3.up);

        Debug.DrawRay(hitRay.origin, hitRay.direction);

        if (Physics.Raycast(hitRay, out hit, _scanDist))
        {
            Debug.Log("Checking hit");
            CheckHit(hit);
        }

        transform.position = newPos;
    }

    void CheckHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Asteroid>(out Asteroid hitObj))
        {
            Debug.Log("Hit found");
            hitObj.EndOfLife();
            transform.position = hit.point;
            EndOfLife();
        }
    }

    void CheckOffscreen()
    {
        if (!_rend.isVisible)
        {
            EndOfLife();
        }
    }

    void EndOfLife()
    {
        Destroy(this.gameObject);
    }
}
