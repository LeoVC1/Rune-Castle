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
        //Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        //direction.y = transform.forward.y;
        //Vector3 newForward = (Vector3.Slerp(spineBone.forward, direction, 0.8f));

        //newForward.x = Mathf.Clamp(newForward.x, hips.forward.x - maximumTorque, hips.forward.x + maximumTorque);

        //newForward.z = Mathf.Clamp(newForward.z, hips.forward.z - maximumTorque, hips.forward.z + maximumTorque);

        //spineBone.forward = newForward;
    }

    void BasicMovement()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetMovementSpeed(hor, 2);
        }
        else
        {
            anim.SetMovementSpeed(hor, ver);
        }

        Vector3 moveDir = (transform.forward * ver + transform.right * hor);

        if (moveDir.magnitude > 1)
            moveDir.Normalize();

        rb.velocity = new Vector3(moveDir.x * speed * Time.deltaTime, rb.velocity.y, moveDir.z * speed * Time.deltaTime);
    }

    void RotateToForward()
    {
        Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        direction.y = transform.forward.y;
        transform.forward = (Vector3.Slerp(transform.forward, direction, 0.8f));
        //Vector3 camF = Vector3.Scale(camT.forward, new Vector3(1, 0, 1)).normalized;
        //Vector3 moveDir = camT.right * Input.GetAxis("Horizontal") + camF * Input.GetAxis("Vertical");
        //if (moveDir.magnitude > 0)
        //    transform.forward = Vector3.Lerp(transform.forward, moveDir, 0.5f);
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
