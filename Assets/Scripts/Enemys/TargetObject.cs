using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


[ExecuteInEditMode]
public class TargetObject : MonoBehaviour
{
    public int priority;

    public float health;

    public Image healthBar;

    public Transform[] targetPoint;

    public Transform lookPoint;

    private float maxHealth;

    public bool useEvent;

    public UnityEvent onDeathEvents;

    private void Start()
    {
        maxHealth = health;
    }

    public bool ReceiveDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            OnDeath();
            return true;
        }
        else
            return false;
    }

    private void OnDeath()
    {
        if (useEvent)
            onDeathEvents.Invoke();
        else
            Destroy(this.gameObject);
    }

    public float GetHealthPerc()
    {
        return health / maxHealth;
    }

    private void OnDrawGizmos()
    {
        foreach(Transform t in targetPoint)
        {
            Gizmos.DrawCube(t.position, Vector3.one);
        }
    }
}
