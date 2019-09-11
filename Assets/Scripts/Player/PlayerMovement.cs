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

    void Start()
    {
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
        //float speed = 0;
        //speed += Mathf.Clamp(Mathf.Abs(ver) + Mathf.Abs(hor), 0, 1);
        //speed = Mathf.Abs(speed);

        //transform.position += (transform.forward * ver + transform.right * hor) * (/*Time.deltaTime **/ speed);
        print(rb);
        print((transform.forward * ver + transform.right * hor) * speed);
        rb.velocity = (transform.forward * ver + transform.right * hor) * speed;
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
