﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("References:")]
    public EnemyData enemyData;
    public GameObject deathParticle;
    public Animator anim;
    public Image healthBar;
    public TargetObject mainTarget;

    private TargetObject _target;
    private NavMeshAgent _agent;

    private List<TargetObject> nearbyTargets = new List<TargetObject>();

    [Header("Properties:")]
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

        if (nearbyTargets.Count > 0)
            GetTarget();
        else
            _target = mainTarget;

        float distance = Vector3.Distance(transform.position, _target.transform.position);

        if(distance < enemyData.range)
        {
            _agent.isStopped = true;
            transform.forward = _target.transform.position - transform.position;
            InvokeRepeating("Attack", 0, 1);
            if (anim)
                anim.SetTrigger("Attack");
        }
        else
        {
            _agent.isStopped = false;
            CancelInvoke("Attack");
        }

        if (_target)
            _agent.destination = _target.transform.position;
    }

    private void GetTarget()
    {
        int maxPriority = int.MinValue;
        for(int i = nearbyTargets.Count - 1; i >= 0; i--)
        {
            if(nearbyTargets[i].priority > maxPriority)
            {
                maxPriority = nearbyTargets[i].priority;
                _target = nearbyTargets[i];
            }
        }
    }

    public void Attack()
    {
        _target.ReceiveDamage(enemyData.damage);
    }

    public bool ReceiveDamage(int damage)
    {
        myHealth -= damage;
        healthBar.fillAmount = myHealth / enemyData.maxHealth;
        if (myHealth <= 0 && !dead)
        {
            OnDeath();
            dead = true;
            return true;
        }
        else
            return false;
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

    private void OnTriggerEnter(Collider other)
    {
        TargetObject target = other.GetComponent<TargetObject>();
        if (target)
        {
            if (!nearbyTargets.Contains(target))
                nearbyTargets.Add(target);
        }
    }
}
