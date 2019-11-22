using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAreaSkill : MonoBehaviour
{
    public int damage;

    public float startDelay;
    public float impactLifetime;
    public float lifeTime;
    public bool dontDestroy;
    public bool onStart;

    private List<Collider> collided = new List<Collider>();

    private bool _lock;

    public AudioSource audio;

    private void Start()
    {
        if (onStart)
            return;

        Invoke("OnImpact", startDelay);
        Invoke("StopImpact", startDelay + impactLifetime);
        if(!dontDestroy)
            Destroy(gameObject, lifeTime);
    }

    public void Explode()
    {
        Invoke("OnImpact", startDelay);
        Invoke("StopImpact", startDelay + impactLifetime);
        if (!dontDestroy)
            Destroy(gameObject, lifeTime);
    }

    void OnImpact()
    {
        _lock = true;
        audio.Play();
    }

    void StopImpact()
    {
        _lock = false;
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
