using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _spd = 5f;
    Vector3 _moveDir;
    [SerializeField] Camera cam;
    Vector3 _homePos;
    BoxCollider _collider;

    private void Start()
    {
        _homePos = transform.position;
        _collider = GetComponent<BoxCollider>();
        GameManager.Player = this;
        Initialise();
    }

    public void Initialise()
    {
        transform.position = _homePos;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (GameManager.IsPlaying())
        {
            float _horMove = Input.GetAxis("Horizontal");
            //float _verMove = Input.GetAxis("Vertical");
            Move(_horMove, 0); //_verMove);
            StayOnScreen();
            CheckCollision();
        }
    }

    public void Move(float hor, float ver)
    {
        _moveDir = new Vector3(hor, ver, 0);
        _moveDir *= _spd * Time.deltaTime;
        transform.position += _moveDir;
    }

    public void StayOnScreen()
    {
        if (cam != null)
        {
            Vector3 _posOnCam = cam.WorldToViewportPoint(transform.position);
            Vector3 newPos = transform.position;
            _posOnCam.x = Mathf.Clamp(_posOnCam.x, 0, 1);
            newPos = cam.ViewportToWorldPoint(_posOnCam);
            transform.position = newPos;
        }
    }

    private void CheckCollision()
    {
        Debug.Log("Checking collision");

        Collider[] colliders = Physics.OverlapBox(transform.position, _collider.size / 2f);
        if (colliders.Length > 1)
        {
            Debug.Log("Found colliders");
            if (CheckColliders(colliders))
                EndOfLife();
        }
    }

    private bool CheckColliders(Collider[] colliders)
    {
        bool hitFound = false;
        foreach (Collider collider in colliders)
        {
            Debug.Log("Checking for asteroid");
            hitFound = (collider.GetComponent<Asteroid>() != null);
        }
        return hitFound;
    }

    public void EndOfLife()
    {
        Debug.Log("Ending Round");
        GameManager.endGame = true;
    }
}
