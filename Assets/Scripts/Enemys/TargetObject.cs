using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using MyBox;

public class TargetObject : MonoBehaviour
{
    public int priority;

    public float health;

    public Image healthBar;

    public Transform[] targetPoint;

    public Transform lookPoint;

    private float maxHealth;

    public bool useEvent;

    [ConditionalField("useEvent")]
    public UnityEvent onDeathEvents;

    public TextMeshProUGUI myLifeNumber;

    private void Start()
    {
        maxHealth = health;
    }

    public bool ReceiveDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        myLifeNumber.text = health.ToString() + "/" + maxHealth.ToString();
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

    public void IncreaseMaximumLifeByRune()
    {
        maxHealth += 200;
        health += 200;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthBar.fillAmount = health / maxHealth;
        myLifeNumber.text = health.ToString() + "/" + maxHealth.ToString();
    }

    public void RegenLifeByRune()
    {
        health += 400;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthBar.fillAmount = health / maxHealth;
        myLifeNumber.text = health.ToString() + "/" + maxHealth.ToString();
    }

    private void OnDrawGizmos()
    {
        foreach(Transform t in targetPoint)
        {
            Gizmos.DrawCube(t.position, Vector3.one);
        }
    }
}
