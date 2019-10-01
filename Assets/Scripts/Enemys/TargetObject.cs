using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetObject : MonoBehaviour
{
    public int priority;

    public float health;

    public Image healthBar;

    private float maxHealth;

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
        //throw new NotImplementedException();
    }
}
