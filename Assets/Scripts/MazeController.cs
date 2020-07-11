using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public GameObject target;

    private Vector3 _tilt;

    //private bool _rotatingForward = false;
    //private bool _rotatingBack = false;
    //private bool _rotatingLeft = false;
    //private bool _rotatingRight = false;

    [SerializeField] private float _rotateSpeed = 80f;

    void Update()
    {
        transform.RotateAround(target.transform.position, _tilt, _tilt.magnitude * _rotateSpeed * Time.deltaTime);

        //transform.RotateAround(target.transform.position, Vector3.forward, (_rotatingForward ? _rotateSpeed : 0) * Time.deltaTime);
        //transform.RotateAround(target.transform.position, Vector3.back, (_rotatingBack ? _rotateSpeed : 0) * Time.deltaTime);
        //transform.RotateAround(target.transform.position, Vector3.left, (_rotatingLeft ? _rotateSpeed : 0) * Time.deltaTime);
        //transform.RotateAround(target.transform.position, Vector3.right, (_rotatingRight ? _rotateSpeed : 0) * Time.deltaTime);
    }

    public void SetTilt(Vector2 vector)
    {
        _tilt = new Vector3(vector.x, 0f, vector.y);
    }

    //void HandleInput()
    //{
    //    _rotatingForward = Input.GetKey(KeyCode.W);
    //    _rotatingBack = Input.GetKey(KeyCode.S);
    //    _rotatingLeft = Input.GetKey(KeyCode.A);
    //    _rotatingRight = Input.GetKey(KeyCode.D);
    //}
}
