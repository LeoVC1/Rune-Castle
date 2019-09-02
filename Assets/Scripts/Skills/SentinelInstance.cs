using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelInstance : MonoBehaviour
{
    public SpawnerController spawnerController;

    public int damage;
    public float distance;
    private bool _lockRotation;

    Vector3 idleTarget;
    Vector3 idleDirection;

    LineRenderer lr;
    Enemy myEnemy;

    public Vector3 offset;

    [Range(0.2f, 5)]
    public float cooldown;
    private float cooldownTimer;
    private bool onTarget;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        StartCoroutine(TryToAttack());
    }

    public void Update()
    {
        (Transform target, float _distance) = spawnerController.GetClosestEnemy(transform);
        if (target && _distance < distance)
        {
            RotateToEnemy(target);
            onTarget = true;
        }
        else if (!_lockRotation)
        {
            RotateToIdle();
            onTarget = false;
        }
        else
        {
            transform.forward = -(Vector3.Slerp(-transform.forward, idleDirection, 0.03f));
            onTarget = false;
        }
        DrawLaser();
    }

    IEnumerator TryToAttack()
    {
        while (true)
        {
            if (cooldownTimer < cooldown)
            {
                cooldownTimer += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                cooldownTimer = 0;
                Attack();
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }

    void Attack()
    {
        if (myEnemy)
        {
            myEnemy.ReceiveDamage(damage);
        }
    }

    void RotateToEnemy(Transform _target)
    {
        Vector3 direction = _target.position - transform.position;
        direction.y = transform.forward.y;
        transform.forward = -(Vector3.Slerp(-transform.forward, direction, 0.1f));
    }

    void RotateToIdle()
    {
        _lockRotation = true;

        idleTarget = new Vector3(Random.Range(-360, 360), 0, Random.Range(-360, 360));
        idleDirection = idleTarget - transform.position;
        idleDirection.y = transform.forward.y;

        Invoke("Timer", Random.Range(1f, 2.5f));
    }

    void Timer()
    {
        _lockRotation = false;
    }

    public void SetDistance(float _distance)
    {
        distance = _distance;
    }

    private void DrawLaser()
    {
        lr.SetPosition(0, transform.position - new Vector3(offset.x, offset.y, offset.z));
        RaycastHit hit;
        if (onTarget)
        {
            lr.enabled = true;
            if (Physics.Raycast(transform.position, -transform.forward, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                    myEnemy = hit.collider.GetComponentInChildren<Enemy>();
                }
            }
            else
            {
                lr.SetPosition(1, -transform.forward * 5000);
                myEnemy = null;
            }
        }
        else
        {
            lr.enabled = false;
        }
    }
}
