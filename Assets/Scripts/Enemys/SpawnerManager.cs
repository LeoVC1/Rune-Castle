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

    public int waveNumber = 0;
    public int enemiesAlive;

    public GameEvent startTimerEvent;


    private void Start()
    {
        spawnerController.Initialize();
        waveNumber++;
        spawnerController.wave = waveNumber;
        Invoke("StartTimer", 0);
    }

    private List<int> GetSpawners()
    {
        print(waveNumber);
        List<int> spawnersIndex = new List<int>();
        if(waveNumber <= 3)
        {
            spawnersIndex.Add(Random.Range(0, spawners.Length));
            return spawnersIndex;
        }
        else if(waveNumber <= 5)
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

            spawnersIndex.Add(0);
            spawnersIndex.Add(1);
            spawnersIndex.Add(2);
            return spawnersIndex;
        }
    }

    public void StartTimer()
    {
        startTimerEvent.Raise();
    }

    public void OnStartWave()
    {
        unstoppableEnemies = 0;
        startEnemiesAmount = 0;
        cristalStartLife = cristal.health;
        List<int> spawnerIndex = GetSpawners();

        int enemiesAmount = (int)spawnerController.GetEnemiesAmount();

        enemiesAlive = enemiesAmount;

        int enemiesBySpawner = enemiesAmount / spawnerIndex.Count;

        for (int i = 0; i < spawnerIndex.Count; i++)
        {
            spawners[spawnerIndex[i]].StartSpawn(enemiesBySpawner, 0, 1, waveNumber);
        }
    }

    public void OnEndWave()
    {
        spawnerController.CalculateByHealthLost(cristalStartLife, cristal.health);
        spawnerController.CalculateByEnemies(startEnemiesAmount, unstoppableEnemies);
        spawnerController.UpdatePoints();
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].ResetColor();
        }
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
            spawnerController.wave = waveNumber;
            OnEndWave();
            Invoke("StartTimer", 3);
        }
    }

}
