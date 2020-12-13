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

    public bool isRunning = false;
    private bool isMoving = false;

    private Rigidbody rb;
    private PlayerAnimation anim;

    public LayerMask layerMask;

    public bool onEnemy;
    private Transform target;

    public AudioSource run;

    public AudioClip[] clips;

    private void Awake()
    {
        anim = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!inputManager.isMovementLocked)
        {
            VerifyEnemy();
            RunningMovement();
            RotateToForward();
            BasicMovement();
            MovingAudio();
        }
    }

    void BasicMovement()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        Vector3 moveDir = (transform.forward * ver + transform.right * hor);

        if (moveDir.magnitude > 1)
            moveDir.Normalize();

        moveDir *= actualSpeed;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        isMoving = (ver != 0 || hor != 0) ? true : false;

        Animate(hor, ver);
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

    void MovingAudio()
    {
        if (Input.GetKeyDown(inputManager.sprintInput))
        {
            run.clip = clips[1];
        }
        if (Input.GetKeyUp(inputManager.sprintInput))
        {
            run.clip = clips[0];
        }

        if (isMoving)
        {
            if (!run.isPlaying)
                run.Play();
        }
        else
        {
            if (!run.isPlaying)
                run.Stop();
        }
        

    }

    void RotateToForward()
    {
        Vector3 direction = Camera.main.GetCenterDirection();
        direction.y = transform.forward.y;
        transform.forward = (Vector3.Slerp(transform.forward, direction, 0.8f));
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
        Ray mouseRay = Camera.main.GetCenterRay();

        if (Physics.Raycast(mouseRay, out RaycastHit hit, float.MaxValue, layerMask))
        {
            if (hit.collider)
            {
                onEnemy = true;
                target = hit.collider.gameObject.transform;
            }
            else
            {
                onEnemy = false;
                target = null;
            }
        }
        else
        {
            onEnemy = false;
            target = null;
        }
    }

    public void FreezeMovement()
    {
        anim.SetMovementSpeed(0, 0);
        rb.velocity = Vector3.zero;
    }
}