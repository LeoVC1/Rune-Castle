using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dissolve : MonoBehaviour
{
    private Material[] dissolveMaterials;
    public bool dissolveInStart;
    public RendererMaterial rendererType;
    public float delayTime;


    public UnityEvent onEndDissolve;

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
            case RendererMaterial.SKINNED:
                dissolveMaterials = GetComponent<SkinnedMeshRenderer>().materials;
                break;
            default:
                dissolveMaterials = GetComponent<Renderer>().materials;
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
        onEndDissolve.Invoke();
    }
}

public enum RendererMaterial
{
    MESH,
    PARTICLE,
    SKINNED
}