using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _spd = 5f;
    Vector3 _moveDir;
    [SerializeField] Camera cam;

    // Update is called once per frame
    void Update()
    {
        float _horMove = Input.GetAxis("Horizontal");
        //float _verMove = Input.GetAxis("Vertical");
        Move(_horMove, 0); //_verMove);
        StayOnScreen();
    }

    public void Move(float hor, float ver)
    {
        _moveDir = new Vector3(hor, ver, 0);
        _moveDir *= _spd * Time.deltaTime;
        transform.position += _moveDir;
    }

    public void StayOnScreen()
    {
        Vector3 _posOnCam = cam.WorldToViewportPoint(transform.position);
        Vector3 newPos = transform.position;
        _posOnCam.x = Mathf.Clamp(_posOnCam.x, 0, 1);
        newPos = cam.ViewportToWorldPoint(_posOnCam);
        transform.position = newPos;
    }
}
