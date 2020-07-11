using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public GameObject target;

    private bool _rotatingForward = false;
    private bool _rotatingBack = false;
    private bool _rotatingLeft = false;
    private bool _rotatingRight = false;

    private const float _rotateSpeed = 80f;

    void Update()
    {
        HandleInput();

        transform.RotateAround(target.transform.position, Vector3.forward, (_rotatingForward ? _rotateSpeed : 0) * Time.deltaTime);
        transform.RotateAround(target.transform.position, Vector3.back, (_rotatingBack ? _rotateSpeed : 0) * Time.deltaTime);
        transform.RotateAround(target.transform.position, Vector3.left, (_rotatingLeft ? _rotateSpeed : 0) * Time.deltaTime);
        transform.RotateAround(target.transform.position, Vector3.right, (_rotatingRight ? _rotateSpeed : 0) * Time.deltaTime);
    }

    void HandleInput()
    {
        _rotatingForward = Input.GetKey(KeyCode.W);
        _rotatingBack = Input.GetKey(KeyCode.S);
        _rotatingLeft = Input.GetKey(KeyCode.A);
        _rotatingRight = Input.GetKey(KeyCode.D);
    }
}
