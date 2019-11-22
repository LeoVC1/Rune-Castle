using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnerManager spawnerManager;

    public GameController gameController;

    public TargetObject mainTarget;

    public GameObject[] enemyPrefabs;

    public MeshRenderer rune;
    private Color runeStartEmission;

    private void Start()
    {
        runeStartEmission = rune.material.GetColor("_EmissionColor");
        Color aux = rune.material.color;
        aux.r = 0;
        rune.material.SetColor("_EmissionColor", aux);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    public void StartSpawn(int enemiesCount, float timeToSpawn, float timeSpawning, int waveNumber)
    {
        rune.material.SetColor("_EmissionColor", runeStartEmission);
        StartCoroutine(Spawn(enemiesCount, timeToSpawn, timeSpawning, waveNumber));
    }

    private IEnumerator Spawn(int enemiesCount, float timeToSpawn, float timeSpawning, int waveNumber)
    {
        float timer = 0;
        bool finish = false;

        int maxHealer = 2;
        int maxBoss = 1;
        while (!finish)
        {
            if(gameController.gameState == GameState.GAMEPLAY)
            {
                if (timer < timeToSpawn)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
                else
                {
                    for(int i = 0; i < enemiesCount; i++)
                    {
                        float percHealer = 0, percBoss = 0;

                        if(waveNumber > 3)
                        {
                            percHealer = waveNumber * 2;
                            percHealer = Mathf.Clamp(percHealer, 0, 25);
                        }

                        if(waveNumber > 5)
                        {
                            percBoss = (waveNumber / 15) * 0.5f;
                            percBoss = Mathf.Clamp(percBoss, 0, 3);
                            percBoss *= 10;
                        }

                        int randomEnemy = Random.Range(2, 100);
                        int enemy = randomEnemy > percBoss ? randomEnemy > percHealer ? 0 : 2 : 1;

                        if (enemy == 2 && maxHealer > 0)
                            maxHealer--;
                        else if (enemy == 1 && maxBoss > 0)
                            maxBoss--;
                        else
                            enemy = 0;

                        GameObject newEnemy = Instantiate(enemyPrefabs[enemy], transform.position + GetRandomPosition(), Quaternion.identity);
                        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
                        enemyScript.mainTarget = mainTarget;
                        enemyScript.spawnerManager = spawnerManager;
                        yield return new WaitForSeconds(Random.Range(0.3f, 2f));
                    }
                    finish = true;
                }
                yield return null;
            }
            yield return null;
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-3, 3);
        float z = Random.Range(-3, 3);

        return new Vector3(x, 0, z);
    }

    public void ResetColor()
    {
        Color aux = rune.material.color;
        aux.r = 0;
        rune.material.SetColor("_EmissionColor", aux);
    }

}