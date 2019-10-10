using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalColor : MonoBehaviour
{
    public TargetObject cristalHealth;
    public Material cristalColor;
    public rotation orbitParticles;

    public ParticleSystem[] particles;

    public Particles[] speedParticles;

    public ShakeCrystal[] cristals;
    public float speed = 1, amount = 1;

    private void Update()
    {
        float alpha = cristalHealth.GetHealthPerc();

        SetCristalNoise(alpha);

        SetCristalColor(alpha);

        SetOrbitParticlesSpeed(alpha);

        SetParticlesColor(alpha);

        SetParticlesSpeed(alpha);

        foreach (ShakeCrystal cristal in cristals)
        {
            cristal.speed = Mathf.Lerp(2, 0, alpha);
            cristal.amount = Mathf.Lerp(1.6f, 0, alpha);
        }
    }

    private void SetParticlesSpeed(float alpha)
    {
        foreach(Particles ps in speedParticles)
        {
            ps.particle.startSpeed = Mathf.Lerp(ps.maxSpeed, ps.minSpeed, alpha);
        }
    }

    private void SetParticlesColor(float alpha)
    {
        foreach(ParticleSystem ps in particles)
        {
            ps.startColor = Color.Lerp(Color.red + Color.yellow, Color.blue + Color.red, alpha);
        }
    }

    private void SetOrbitParticlesSpeed(float alpha)
    {
        orbitParticles.yRotation = Mathf.Lerp(8, 3, alpha);
    }

    private void SetCristalColor(float alpha)
    {
        Color color = cristalColor.GetColor("_Color");
        color = Color.Lerp(Color.red + Color.yellow, Color.blue + Color.red, alpha);
        color.r = Mathf.Lerp(4, 1, alpha);
        cristalColor.SetColor("_Color", color);
    }

    private void SetCristalNoise(float alpha)
    {
        Vector4 noiseSpeed = cristalColor.GetVector("_NoiseSpeed");
        noiseSpeed.x = Mathf.Lerp(2, 0.1f, alpha);
        cristalColor.SetVector("_NoiseSpeed", noiseSpeed);
    }
}

    [System.Serializable]
    public struct Particles
    {
        public ParticleSystem particle;
        public float minSpeed, maxSpeed;
    }
