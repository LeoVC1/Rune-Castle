using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public float vel = 10;

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
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        float speed = vel * Time.deltaTime;

        transform.Translate(hor * speed, 0, ver * speed);
    }

    void RotateToForward()
    {
        //Vector3 camF = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
        //Vector3 moveDir = transform.right * Input.GetAxis("Horizontal") + camF * Input.GetAxis("Vertical");
        //if (moveDir.magnitude > 0)
        //    transform.forward = Vector3.Lerp(transform.forward, moveDir, 0.1f);
    }

}
