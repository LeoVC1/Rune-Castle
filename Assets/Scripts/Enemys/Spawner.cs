using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnerManager spawnerManager;

    public GameController gameController;

    public TargetObject mainTarget;

    public GameObject[] enemyPrefabs;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    public void StartSpawn(int enemiesCount, float timeToSpawn, float timeSpawning)
    {
        StartCoroutine(Spawn(enemiesCount, timeToSpawn, timeSpawning));
    }

    private IEnumerator Spawn(int enemiesCount, float timeToSpawn, float timeSpawning)
    {
        float timer = 0;
        bool finish = false;
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
                        GameObject newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.position + GetRandomPosition(), Quaternion.identity);
                        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
                        enemyScript.mainTarget = mainTarget;
                        enemyScript.spawnerManager = spawnerManager;
                        yield return new WaitForSeconds(Random.Range(1, 4));
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

}