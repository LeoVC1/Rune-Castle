using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public Transform camT;

    public float speed = 300;
    public float runSpeed = 600;

    private float actualSpeed;

    bool isRunning = false;
    bool isMoving = false;

    Rigidbody rb;
    PlayerAnimation anim;

    public float maximumTorque = 0.15f;

    private bool onEnemy;

    private void Awake()
    {
        anim = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (!inputManager.isMovementLocked)
        {
            RunningMovement();
            RotateToForward();
            BasicMovement();
        }
    }

    void BasicMovement()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        Vector3 moveDir = (transform.forward * ver + transform.right * hor);

        if (moveDir.magnitude > 1)
            moveDir.Normalize();

        rb.velocity = new Vector3(moveDir.x * actualSpeed * Time.deltaTime, rb.velocity.y, moveDir.z * actualSpeed * Time.deltaTime);

        isMoving = (ver != 0 || hor != 0) ? true : false;

        Animate(hor, ver);
    }

    void RotateToForward()
    {
        if(isRunning || isMoving)
        {
            Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
            direction.y = transform.forward.y;
            transform.forward = (Vector3.Slerp(transform.forward, direction, 0.8f));
        }
    }

    void RunningMovement()
    {
        if (Input.GetKey(inputManager.sprintInput))
        {
            actualSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            actualSpeed = speed;
            isRunning = false;
        }
    }

    void Animate(float hor, float ver)
    {
        if (!isMoving)
        {
            anim.SetMovementSpeed(0, 0);
        }
        else if (isRunning)
        {
            anim.SetMovementSpeed(hor * 2, ver * 2);
        }
        else
        {
            anim.SetMovementSpeed(hor, ver);
        }
    }

    void VerifyEnemy()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, float.MaxValue, )
    }

    public void FreezeMovement()
    {
        anim.SetMovementSpeed(0, 0);
        rb.velocity = Vector3.zero;
    }

}