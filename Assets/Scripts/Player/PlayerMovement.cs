using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public Transform camT;

    public float speed = 2;
    public float runSpeed = 1;
    public float rotationSpeed = 50;

    bool isRunning = false;

    Rigidbody rb;
    PlayerAnimation anim;

    public Transform spineBone;

    public Vector3 forward;

    public float maximumTorque = 0.15f;

    public Transform hips;

    void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    void Update()
    {
        if (!inputManager.isMovementLocked)
        {
            RotateToForward();
            BasicMovement();
            RunningMovement(0);
        }
    }

    private void LateUpdate()
    {
        Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        direction.y = transform.forward.y;
        Vector3 newForward = (Vector3.Slerp(spineBone.forward, direction, 0.8f));

        Debug.DrawRay(hips.position, hips.forward * 100);

        newForward.x = Mathf.Clamp(newForward.x, hips.forward.x - maximumTorque, hips.forward.x + maximumTorque);

        newForward.z = Mathf.Clamp(newForward.z, hips.forward.z - maximumTorque, hips.forward.z + maximumTorque);

        spineBone.forward = newForward;
    }

    void BasicMovement()
    {
        float ver = Mathf.Abs(Input.GetAxis("Vertical"));
        float hor = Mathf.Abs(Input.GetAxis("Horizontal"));

        Vector3 horizontalSpeed = (transform.forward * Mathf.Clamp01((ver + hor))) * speed;

        rb.velocity = new Vector3(horizontalSpeed.x, rb.velocity.y, horizontalSpeed.z);

        anim.SetMovementSpeed(ver, hor);
    }

    void RotateToForward()
    {
        //Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        //direction.y = transform.forward.y;
        //transform.forward = (Vector3.Slerp(transform.forward, direction, 0.8f));
        Vector3 camF = Vector3.Scale(camT.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDir = camT.right * Input.GetAxis("Horizontal") + camF * Input.GetAxis("Vertical");
        if (moveDir.magnitude > 0)
            transform.forward = Vector3.Lerp(transform.forward, moveDir, 0.5f);
    }

    void RunningMovement(float speed)
    {
        if (Input.GetKey(KeyCode.LeftShift) && speed != 0)
        {
            isRunning = true;
            runSpeed = 2.5f;
        }
        else
        {
            isRunning = false;
            runSpeed = 1;
        }
    }

    public void FreezeMovement()
    {
        anim.SetMovementSpeed(0, 0);
        rb.velocity = Vector3.zero;
    }


}
