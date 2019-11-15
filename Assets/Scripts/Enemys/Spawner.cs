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
                        float perc = waveNumber * 0.05f + enemiesCount * 0.03f;
                        perc = Mathf.Clamp(perc, 0, 4);
                        perc *= 10;
                        int randomEnemy = Random.Range(0, 100);
                        int enemy = randomEnemy > perc ? 0 : 1;

                        Debug.Log("Random: " + randomEnemy + "enemy: " + enemy + " Perc: " + perc);

                        GameObject newEnemy = Instantiate(enemyPrefabs[enemy], transform.position + GetRandomPosition(), Quaternion.identity);
                        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
                        enemyScript.mainTarget = mainTarget;
                        enemyScript.spawnerManager = spawnerManager;
                        yield return new WaitForSeconds(Random.Range(0, 1.5f));
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