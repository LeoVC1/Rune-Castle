using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public SpawnerController spawnerController;

    public TargetObject cristal;

    private float cristalStartLife;

    public void OnStartWave()
    {
        cristalStartLife = cristal.health;
    }

    public void OnEndWave()
    {
        spawnerController.CalculateByHealthLost(cristalStartLife, cristal.health);
    }

}
