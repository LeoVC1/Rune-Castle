using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;

    public SpawnerController spawnerController;

    public TargetObject cristal;

    public CameraFollow cameraFollow;

    public Spawner[] spawners;

    private float cristalStartLife;

    private float unstoppableEnemies;
    private float startEnemiesAmount;

    public int waveNumber = 0;
    public int enemiesAlive;

    public GameEvent startTimerEvent;

    public int[] golens = new int[3];
    [TextArea(0, 3)]
    public string[] golensDescriptions = new string[3];

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spawnerController.Initialize();
        waveNumber++;
        spawnerController.wave = waveNumber;
        Invoke("StartTimer", 0);
    }

    private List<int> GetSpawners()
    {
        List<int> spawnersIndex = new List<int>();
        if (waveNumber <= 3)
        {
            spawnersIndex.Add(Random.Range(0, spawners.Length));
            return spawnersIndex;
        }
        else if (waveNumber <= 5)
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

        int enemiesAmount = (int)spawnerController.GetEnemiesAmount() + (int)(waveNumber * 0.5f);

        enemiesAlive = enemiesAmount;

        int enemiesBySpawner = enemiesAmount / spawnerIndex.Count;
        int rest = enemiesAmount % spawnerIndex.Count;

        for (int i = 0; i < spawnerIndex.Count; i++)
        {
            if (i != 0)
                rest = 0;
            spawners[spawnerIndex[i]].StartSpawn(enemiesBySpawner + rest, 0, 1, waveNumber);
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
        if (enemiesAlive == 0)
        {
            waveNumber++;
            spawnerController.wave = waveNumber;
            OnEndWave();
            Invoke("StartTimer", 3);
        }
    }

    public void OnSpawnNewEnemy(int index, GameObject newEnemy)
    {
        golens[index] = 1;
        cameraFollow.ChangeTarget(newEnemy);
        cameraFollow.ChangeDescription(golensDescriptions[index]);
    }


}
