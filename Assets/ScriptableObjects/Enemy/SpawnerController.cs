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

    public List<Transform> GetNearbyEnemys(Transform position, float range, DistanceMode mode)
    {
        Vector3 position2D = position.position;

        List<Transform> nearbyEnemys = new List<Transform>();

        foreach (Transform t in activeEnemys)
        {
            float t_distance = 0;
            Vector3 tPostion2D = Vector3.one;

            if (mode == DistanceMode._2D)
            {
                tPostion2D = new Vector3(t.position.x, position2D.y, t.position.z);
                t_distance = Vector2.Distance(tPostion2D, position2D);
            }
            else
            {
                t_distance = Vector3.Distance(t.position, position.position);
            }

            if (t_distance < range)
            {
                nearbyEnemys.Add(t);
            }
        }

        return nearbyEnemys;
    }

    
}

public enum DistanceMode { _3D, _2D }