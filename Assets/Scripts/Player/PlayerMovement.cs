using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Transform cam;
    public InputManager inputManager;
    public float vel = 10;

    float moveSpeed = 8f;
    float jumpHeight = 10f;         


    Vector3 moveDirection;

    Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!inputManager.isMovementLocked)
        {
            Move();
            RotateToForward();
        }
    }

    void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis);
        rb.position += movement * moveSpeed * Time.deltaTime;
    }

    void RotateToForward()
    {
        Vector3 camF = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camR = Vector3.Scale(cam.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDir = camR * Input.GetAxisRaw("Horizontal") + camF * Input.GetAxisRaw("Vertical");
        if (moveDir.magnitude > 0)
            transform.forward = Vector3.Slerp(transform.forward, moveDir, 0.1f);
    }


}
