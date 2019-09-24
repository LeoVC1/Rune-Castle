using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : Interactable
{
    [Header("Managers:")]
    public InventoryManager inventoryManager;

    public GameObject particles;
    public GameObject rune;

    public int runeID;

    Rigidbody rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    public void GetRune()
    {
        
    }

    public override void TriggerEnter(Collider other)
    {
        base.TriggerEnter(other);
        if (other.gameObject.layer == 8)
        {
            rb.isKinematic = true;
            particles.SetActive(true);
        }
    }
}

