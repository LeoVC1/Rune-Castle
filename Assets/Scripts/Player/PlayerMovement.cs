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

    public Transform spineBone;

    public float maximumTorque = 0.15f;

    public Transform hips;

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

        Vector3 moveDir = (transform.forward * ver + transform.right * hor);

        if (moveDir.magnitude > 1)
            moveDir.Normalize();

        rb.velocity = new Vector3(moveDir.x * actualSpeed * Time.deltaTime, rb.velocity.y, moveDir.z * actualSpeed * Time.deltaTime);

        isMoving = (ver != 0 || hor != 0) ? true : false;

        Animate(hor, ver);
    }

    void RotateToForward()
    {
        Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        direction.y = transform.forward.y;
        transform.forward = (Vector3.Slerp(transform.forward, direction, 0.8f));
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

    public void FreezeMovement()
    {
        anim.SetMovementSpeed(0, 0);
        rb.velocity = Vector3.zero;
    }


}
