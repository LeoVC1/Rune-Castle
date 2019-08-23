using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueReferenceUI : MonoBehaviour
{
    public GameManager manager;
    public ResourceReference[] resources;
    [HideInInspector] public ResourceReference activeResource;

    public void Start()
    {
        UpdateResource();
    }

    public void UpdateResource()
    {
        foreach (ResourceReference resource in resources)
        {
            if (resource.myClass == manager.characterClass)
                activeResource = resource;
        }
    }
}
