using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameController gameController;

    public int timeToSpawn;

    public int spawnCount;

    public GameObject[] enemyPrefabs;

    private int timer = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if(gameController.gameState == GameState.GAMEPLAY)
            {
                if (timer < timeToSpawn)
                {
                    timer += 1;
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    for(int i = 0; i < spawnCount; i++)
                    {
                        GameObject newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.position + GetRandomPosition(), Quaternion.identity);
                        yield return null;
                    }
                    Destroy(gameObject);
                }
                yield return null;
            }
            yield return null;
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-2, 2);
        float z = Random.Range(-2, 2);

        return new Vector3(x, 0, z);
    }

}