using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelInstance : MonoBehaviour
{
    public SpawnerController spawnerController;

    Vector3 idleTarget;
    Vector3 idleDirection;

    Rigidbody rb;
    LineRenderer lr;
    Enemy myEnemy;

    public Vector3 offset;

    [Space]
    [Header("Properties:")]
    public int damage;
    public float distance;
    public float recoilPower;
    public float lifeTime;
    public float forcePower;
    [Range(0.2f, 5)] public float cooldown;

    [Header("Particles:")]
    public GameObject loopingSparks;
    public GameObject explosionSparks;
    public GameObject smokeParticles;

    [Header("GameObject References:")]
    public GameObject _parent;
    public GameObject _base;
    private Dissolve _myDissolve;
    private Dissolve _baseDissolve;

    private bool _lockRotation;
    private float lifeTimer = 0;
    private float cooldownTimer;
    private bool onTarget;
    private bool onShooting;
    private bool isDead;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        _myDissolve = GetComponent<Dissolve>();
        _baseDissolve = _base.GetComponent<Dissolve>();
        StartCoroutine(TryToAttack());
        StartCoroutine(LifeTimer());
    }

    private void Update()
    {
        //if (isDead)
        //    return;

        //(Transform target, float _distance) = spawnerController.GetClosestEnemy(transform);
        //if (target && _distance < distance)
        //{
        //    RotateToEnemy(target);
        //    onTarget = true;
        //}
        //else if (!_lockRotation)
        //{
        //    RotateToIdle();
        //    onTarget = false;
        //}
        //else if (onShooting)
        //{
        //    onShooting = false;
        //    RotateToIdle();
        //}
        //else
        //{
        //    transform.forward = -(Vector3.Slerp(-transform.forward, idleDirection, 0.03f));
        //    onTarget = false;
        //}
        //DrawLaser();
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
    IEnumerator LifeTimer()
    {
        while (lifeTimer < lifeTime)
        {
            lifeTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        DeathAnimation();
    }
    IEnumerator Failing()
    {
        float shakeTime = 0;

        Vector3 minShake = transform.localEulerAngles + Vector3.one * -7.5f;
        Vector3 maxShake = transform.localEulerAngles + Vector3.one * 7.5f;

        loopingSparks.SetActive(true);

        while (shakeTime < 1)
        {
            transform.localEulerAngles = new Vector3(Random.Range(minShake.x, maxShake.x), Random.Range(minShake.y, maxShake.y), Random.Range(minShake.z, maxShake.z));
            shakeTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Explosion();
    }

    void Attack()
    {
        if (myEnemy)
        {
            myEnemy.ReceiveDamage(damage);
            RecoilAnimation(myEnemy.transform);
        }
    }

    void RecoilAnimation(Transform _target)
    {
        Vector3 direction = _target.position + new Vector3(0, recoilPower, 0) - transform.position;
        transform.forward = -(Vector3.Slerp(-transform.forward, direction, 0.8f));
        onShooting = true;
    }

    void DeathAnimation()
    {
        isDead = true;

        StopAllCoroutines();

        Destroy(lr);

        StartCoroutine(Failing());
    }

    void Explosion()
    {
        explosionSparks.SetActive(true);
        loopingSparks.SetActive(false);

        rb.isKinematic = false;
        rb.AddForce(new Vector3(Random.Range(-45, 45), 45, Random.Range(-45, 45)) * forcePower, ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(-45, 45), 45, Random.Range(-45, 45)) * forcePower, ForceMode.Impulse);

        Invoke("ChangeMaterial", 1f);
    }

    void ChangeMaterial()
    {
        _myDissolve.StartLerp();
        _baseDissolve.StartLerp();
        DestroyObject();
    }

    void DestroyObject()
    {
        smokeParticles.SetActive(false);
        Destroy(_parent, 2f);
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

    void DrawLaser()
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

    public void SetDistance(float _distance)
    {
        distance = _distance;
    }
}