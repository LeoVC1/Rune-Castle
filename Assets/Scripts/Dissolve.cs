using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private Material[] dissolveMaterials;
    public bool dissolveInStart;
    public RendererMaterial rendererType;
    public float delayTime;

    private void Start()
    {
        switch (rendererType)
        {
            case RendererMaterial.MESH:
                dissolveMaterials = GetComponent<MeshRenderer>().materials;
                break;
            case RendererMaterial.PARTICLE:
                dissolveMaterials = GetComponent<ParticleSystemRenderer>().materials;
                break;
            default:
                dissolveMaterials = GetComponent<MeshRenderer>().materials;
                break;
        }
        if (dissolveInStart)
            Invoke("StartLerp", delayTime);
    }

    public void StartLerp()
    {
        StartCoroutine(LerpMaterial());
    }

    IEnumerator LerpMaterial()
    {
        float t = -1;

        while (t <= 1)
        {
            foreach (Material material in dissolveMaterials)
            {
                material.SetFloat("_Dissolve", t += Time.deltaTime);
            }

            yield return null;
        }
    }
}

public enum RendererMaterial
{
    MESH,
    PARTICLE
}