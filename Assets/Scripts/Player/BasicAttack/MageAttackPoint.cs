using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttackPoint : MonoBehaviour
{
    public float CameraMoveSpeed = 120.0f;

    public Vector3 offSet;
    public float clampAngle = 80.0f;

    public float verticalInputSensitivity = 150.0f;
    public float horizontalInputSensitivityX = 150.0f;

    private float rotY;
    private float rotX;

    public GameObject _target;

   
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * horizontalInputSensitivityX * Time.deltaTime;
        rotX += -mouseY * verticalInputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    void RotateToForward()
    {
        Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        direction.y = transform.forward.y;
        transform.forward = (Vector3.Slerp(transform.forward, direction, 0.8f));
    }

    void LateUpdate()
    {
        CameraUpdater();
        RotateToForward();
    }

    void CameraUpdater()
    {
        Transform target = _target.transform;

        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position + offSet, step);
    }

    public void SetNewOffset(float newOffset)
    {
        offSet.z = newOffset;
    }
}
