using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapInstance : MonoBehaviour
{
    public UnityEvent OnTriggerEvent;

    [Header("Fire Atributes:")]
    public GameObject fireParticles;
    public ParticleSystem detectionCilinder;
    public float cilinderDelay;
    private bool _lock;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 16)
        {
            if (_lock)
                return;
            _lock = true;
            OnTriggerEvent.Invoke();
        }
    }

    /// <summary>
    /// Event of fire version of trap when it was triggered
    /// </summary>
    public void FireVersion()
    {
        StartCoroutine(FireVersionDetection());
    }

    IEnumerator FireVersionDetection()
    {
        detectionCilinder.Play();
        yield return new WaitForSeconds(cilinderDelay);
        Instantiate(fireParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
