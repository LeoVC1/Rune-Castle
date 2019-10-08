using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TargetObject : MonoBehaviour
{
    public int priority;

    public float health;

    public Image healthBar;

    public Transform targetPoint;

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
}
