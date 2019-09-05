﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public SpawnerController spawnerController;
    public GameObject deathParticle;

    private Transform _target;
    private NavMeshAgent _agent;
    private float myHealth;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        myHealth = enemyData.maxHealth;
    }

    void Update()
    {
        if (myHealth <= 0)
        {
            OnDeath();
            return;
        }

        float distance = Vector3.Distance(transform.position, _target.position);
        if(distance < enemyData.range)
        {
            _agent.isStopped = true;
            Debug.Log("Skill!");
        }
        else
        {
            _agent.isStopped = false;
        }

        if (_target)
            _agent.destination = _target.position;
    }

    public void ReceiveDamage(int damage)
    {
        myHealth -= damage;
        print("Damage: " + damage + "/Health: " + myHealth);
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    private void OnDeath()
    {
        GameObject particle = Instantiate(deathParticle, transform.position + new Vector3(0, Random.Range(-3f, 1.5f), 0), Quaternion.identity);
        Destroy(particle, 3.5f);
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
