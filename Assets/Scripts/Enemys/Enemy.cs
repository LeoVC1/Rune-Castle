using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public EnemyTargetManager enemyTarget;
    public EnemyData enemyData;
    public SpawnerController spawnerController;
    public GameObject deathParticle;
    public Animator anim;

    private GameObject _target;
    private NavMeshAgent _agent;
    public float myHealth;

    public float deathAnimationTime;
    private bool dead;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        myHealth = enemyData.maxHealth;
    }

    void Update()
    {

        if (dead)
            return;

        GetTarget();

        float distance = Vector3.Distance(transform.position, _target.transform.position);

        if(distance < enemyData.range)
        {
            _agent.isStopped = true;
            if (anim)
                anim.SetTrigger("Attack");
        }
        else
        {
            _agent.isStopped = false;
        }

        if (_target)
            _agent.destination = _target.transform.position;
    }

    private void GetTarget()
    {
        SetTarget(enemyTarget.GetNewTarget(transform.position));
    }

    public bool ReceiveDamage(int damage)
    {
        myHealth -= damage;
        if (myHealth <= 0 && !dead)
        {
            OnDeath();
            dead = true;
            return true;
        }
        else
            return false;
    }

    public void SetTarget(GameObject newTarget)
    {
        _target = newTarget;
    }

    private void OnDeath()
    {
        Vector3 particlePoint = transform.position + new Vector3(0, Random.Range(-3f, 1.5f),0);
        GameObject particle = Instantiate(deathParticle, particlePoint, Quaternion.identity);
        Destroy(particle, 3.5f);
        StartCoroutine(DeathScale(particlePoint));
    }

    IEnumerator DeathScale(Vector3 point)
    {
        _agent.enabled = false;
        float t = 0;
        while(t <= 1)
        {
            transform.localScale = Vector3.Slerp(transform.localScale, Vector3.zero, t);
            t += Time.deltaTime / deathAnimationTime;
            transform.position = Vector3.Slerp(transform.position, point, t);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        spawnerController.RegisterEnemy(transform);
    }

    private void OnDisable()
    {
        spawnerController.UnregisterEnemy(transform);
    }
}
