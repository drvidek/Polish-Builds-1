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
            _posOnCam.x = Mathf.Clamp(_posOnCam.x, 0, 1);
            Vector3 newPos = cam.ViewportToWorldPoint(_posOnCam);
            transform.position = newPos;
        }
    }

    private bool CheckAsteroidHit(Collider collider)
    {
        return collider.GetComponent<Asteroid>() != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckAsteroidHit(other))
            EndOfLife();
    }

    public void EndOfLife()
    {
        Debug.Log("Ending Round");
        GameManager.endGame = true;
    }
}
