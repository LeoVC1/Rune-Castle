using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public SpawnerController spawnerController;

    public TargetObject cristal;

    public Spawner[] spawners;

    private float cristalStartLife;

    private float unstoppableEnemies;
    private float startEnemiesAmount;

    public int waveNumber;
    public int enemiesAlive;


    private void Start()
    {
        spawnerController.Initialize();
        Invoke("OnStartWave", 0);
    }

    private List<int> GetSpawners()
    {
        List<int> spawnersIndex = new List<int>();
        if(waveNumber <= 5)
        {
            spawnersIndex.Add(Random.Range(0, spawners.Length));
            return spawnersIndex;
        }
        else if(waveNumber <= 10)
        {
            spawnersIndex.Add(Random.Range(0, spawners.Length));
            int number2 = 0;
            do
            {
                number2 = Random.Range(0, spawners.Length);
            } while (spawnersIndex.Contains(number2));
            spawnersIndex.Add(number2);
            return spawnersIndex;
        }
        else
        {
            spawnersIndex.Add(Random.Range(0, spawners.Length));
            int number2 = 0;
            do
            {
                number2 = Random.Range(0, spawners.Length);
            } while (spawnersIndex.Contains(number2));
            int number3 = 0;
            do
            {
                number3 = Random.Range(0, spawners.Length);
            } while (spawnersIndex.Contains(number3));
            spawnersIndex.Add(number3);
            return spawnersIndex;
        }
    }

    public void OnStartWave()
    {
        unstoppableEnemies = 0;
        startEnemiesAmount = 0;
        cristalStartLife = cristal.health;
        List<int> spawnerIndex = GetSpawners();

        int enemiesAmount = (int)spawnerController.GetEnemiesAmount();

        enemiesAlive = enemiesAmount;

        print(enemiesAmount);

        for (int i = 0; i < spawnerIndex.Count; i++)
        {
            int remainingEnemies = 0;
            int enemies = 0;

            //if (spawnerIndex.Count - i != 0)
            //{
            //    print("A");
            //    remainingEnemies = enemiesAmount / (spawnerIndex.Count - i);
            //    enemies = Random.Range(remainingEnemies - 1, remainingEnemies + 2);
            //}
            //else
            //{
            //    print("B");
            //    enemies = enemiesAmount;
            //}
            enemies = enemiesAmount;

            enemiesAmount -= enemies;
            spawners[spawnerIndex[i]].StartSpawn(enemies, 0, 1);
        }
    }

    public void OnEndWave()
    {
        spawnerController.CalculateByHealthLost(cristalStartLife, cristal.health);
        spawnerController.CalculateByEnemies(startEnemiesAmount, unstoppableEnemies);
        spawnerController.UpdatePoints();
    }

    public void OnEnemiesGetCrystal()
    {
        unstoppableEnemies++;
    }

    public void OnEnemySpawn()
    {
        startEnemiesAmount++;
    }

    public void OnEnemyDie()
    {
        enemiesAlive--;
        if(enemiesAlive == 0)
        {
            waveNumber++;
            OnEndWave();
            Invoke("OnStartWave", 5);
        }
    }

}
