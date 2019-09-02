using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner Controller", menuName = "Scriptable Objects/Enemy/Spawner Controller")]
public class SpawnerController : ScriptableObject
{
    private List<Transform> activeEnemys;

    public void RegisterEnemy(Transform enemy)
    {
        activeEnemys.Add(enemy);
    }

    public void UnregisterEnemy(Transform enemy)
    {
        activeEnemys.Remove(enemy);
    }

    public (Transform, float) GetClosestEnemy(Transform position)
    {
        float distance = float.MaxValue;
        Transform closestEnemy = null;

        foreach(Transform t in activeEnemys)
        {
            float t_distance = Vector3.Distance(t.position, position.position);
            if (t_distance < distance)
            {
                distance = t_distance;
                closestEnemy = t;
            }
        }

        return (closestEnemy, distance);
    }
}
