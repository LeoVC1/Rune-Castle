using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAreaSkill : MonoBehaviour
{
    public int damage;

    public float startDelay;

    private List<Collider> collided = new List<Collider>();

    private bool _lock;

    private void Start()
    {
        Invoke("OnImpact", startDelay);
    }

    void OnImpact()
    {
        _lock = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_lock == false)
            return;

        if(!collided.Contains(other))
        {
            collided.Add(other);
            other.GetComponentInChildren<Enemy>().ReceiveDamage(damage);
        }
    }
}
