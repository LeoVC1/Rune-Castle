using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner Controller", menuName = "Scriptable Objects/Enemy/Spawner Controller")]
public class SpawnerController : ScriptableObject
{
    public float globalDifficultiesPoint = 10;
    public float statusPoint = 1;
    public float statusMultiplier = 1;
    public float goldPoint = 1;
    public float goldMultiplier = 1;
    public float spawnPoint = 1;
    public float spawnMultiplier = 1;
    public int minimumEnemyAmount = 5;

    public void Initialize()
    {
        globalDifficultiesPoint = 10;
        statusPoint = 1;
        statusMultiplier = 1;
        goldPoint = 1;
        goldMultiplier = 1;
        spawnPoint = 1;
        spawnMultiplier = 1;
    }

    public void CalculateByHealthLost(float startWaveHealth, float endWaveHealth)
    {
        float healthLost = endWaveHealth * 100 / startWaveHealth;

        if (healthLost == 0)
        {
            statusMultiplier += 0.07f;
            spawnMultiplier += 0.05f;
            goldMultiplier += -0.05f;
            statusPoint += 1;
            spawnPoint += 1;
        }
        else if (healthLost < 4)
        {
            statusMultiplier += -0.10f;
            spawnMultiplier += -0.08f;
            goldMultiplier += 0.07f;
            statusPoint += 0;
            spawnPoint += 0;
        }
        else if (healthLost < 8)
        {
            statusMultiplier += -0.12f;
            spawnMultiplier += -0.1f;
            goldMultiplier += 0.09f;
            statusPoint += -2;
            spawnPoint += -2;
        }
        else if (healthLost < 12)
        {
            statusMultiplier += -0.17f;
            spawnMultiplier += -0.14f;
            goldMultiplier += 0.12f;
            statusPoint += -3;
            spawnPoint += -3;
        }
        else
        {
            statusMultiplier += -0.2f;
            spawnMultiplier += -0.16f;
            goldMultiplier += 0.15f;
            statusPoint += -4;
            spawnPoint += -4;
        }
    }

    public void CalculateByEnemies(float startEnemiesAmount, float unstopableEnemies)
    {
        float enemiesMultiplier = unstopableEnemies * 100 / startEnemiesAmount;

        if (enemiesMultiplier == 0)
        {
            statusMultiplier += 0.06f;
            spawnMultiplier += 0.05f;
            goldMultiplier += -0.04f;
            globalDifficultiesPoint += 4;
        }
        else if (enemiesMultiplier < 4)
        {
            statusMultiplier += -0.04f;
            spawnMultiplier += -0.03f;
            goldMultiplier += 0.02f;
            globalDifficultiesPoint += -2;
        }
        else if (enemiesMultiplier < 10)
        {
            statusMultiplier += -0.05f;
            spawnMultiplier += -0.04f;
            goldMultiplier += 0.03f;
            globalDifficultiesPoint += -4;
        }
        else if (enemiesMultiplier < 20)
        {
            statusMultiplier += -0.07f;
            spawnMultiplier += -0.05f;
            goldMultiplier += 0.05f;
            globalDifficultiesPoint += -6;
        }
        else
        {
            statusMultiplier += -0.1f;
            spawnMultiplier += -0.075f;
            goldMultiplier += 0.075f;
            globalDifficultiesPoint += -8;
        }
    }

    public void UpdatePoints()
    {
        statusPoint *= statusMultiplier;
        spawnPoint *= spawnMultiplier;
        goldPoint *= goldMultiplier;
    }

    public float GetEnemiesAmount()
    {
        return minimumEnemyAmount + (30 * globalDifficultiesPoint / 100) + spawnPoint;
    }

    public float GetBonusGold()
    {
        return (50 * globalDifficultiesPoint / 100) + goldPoint * 2;
    }

    public float GetEnemiesMaxHealth(float baseHealth)
    {
        return baseHealth * statusPoint + (20 * globalDifficultiesPoint / 100);
    }

}