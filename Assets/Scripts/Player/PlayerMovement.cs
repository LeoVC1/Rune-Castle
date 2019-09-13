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

    void BasicMovement()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        Vector3 horizontalSpeed = (transform.forward * ver + transform.right * hor) * speed;

        rb.velocity = new Vector3(horizontalSpeed.x, rb.velocity.y, horizontalSpeed.z);

        float clampSpeed = Mathf.Clamp01(Mathf.Abs(ver) + Mathf.Abs(hor));

        anim.SetMovementSpeed(clampSpeed);
    }

    void RotateToForward()
    {
        Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        direction.y = transform.forward.y;
        transform.forward = (Vector3.Slerp(transform.forward, direction, 0.8f));
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


}
