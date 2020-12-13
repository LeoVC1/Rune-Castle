using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttackPoint : MonoBehaviour
{
    public InputManager inputManager;

    public float CameraMoveSpeed = 120.0f;

    public Vector3 offSet;
    public float clampAngle = 80.0f;

    public float verticalInputSensitivity = 150.0f;
    public float horizontalInputSensitivityX = 150.0f;

    public GameObject _target;

    public Transform point;
    Vector3 pointStartPosition;

    void Start()
    {
        pointStartPosition = point.localPosition;
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        CameraUpdater();
        RotateToForward();
    }


    void RotateToForward()
    {
        Vector3 direction = Camera.main.GetCenterDirection();
        direction.y = transform.forward.y;
        transform.forward = (Vector3.Slerp(transform.forward, direction, 0.8f));
    }

    void CameraUpdater()
    {
        Transform target = _target.transform;

        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position + (offSet.y * Vector3.up), step);
    }

    public void SetNewOffset(float newOffset)
    {
        offSet.z = newOffset;
        point.localPosition = pointStartPosition + Vector3.right * newOffset;
    }
}
