using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public GameObject target;

    private Vector3 _tilt;

    [SerializeField] private float _rotateSpeed = 80f;
    [SerializeField] private float _maxTiltAngle = 15f;
    [SerializeField] private float _resetSpeed = 20f;

    void Update()
    {
        transform.RotateAround(target.transform.position, _tilt, _tilt.magnitude * _rotateSpeed * Time.deltaTime);

        // Restrict Rotation
        target.transform.eulerAngles = new Vector3(ClampAngle(target.transform.eulerAngles.x, -_maxTiltAngle, _maxTiltAngle), 0.0f, ClampAngle(target.transform.eulerAngles.z, -_maxTiltAngle, _maxTiltAngle));

        // TODO: Will probably need to change the conditional to some flag set by the InputVoteCollector (?)
        // If tilt isn't applied, reset back to zero, zero, zero rotation
        if (_tilt == Vector3.zero)
        {
            ResetRotation();
        }
    }

    public void SetTilt(Vector2 vector)
    {
        _tilt = new Vector3(vector.x, 0f, vector.y);
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f)
            angle = 360 + angle;
        if (angle > 180f)
            return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    private void ResetRotation()
    {
        ////find the vector pointing from our position to the target
        //Vector3 direction = (transform.position - transform.position).normalized;

        ////create the rotation we need to be in to look at the target
        //Quaternion lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * _resetSpeed);
    }
}
