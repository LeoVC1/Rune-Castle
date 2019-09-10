using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Target Manager", menuName = "Scriptable Objects/Managers/Enemy Target Manager")]
public class EnemyTargetManager : ScriptableObject
{
    public List<GameObject> enemyTarget = new List<GameObject>();

    public void Register(GameObject gameObject)
    {
        if(!enemyTarget.Contains(gameObject))
            enemyTarget.Add(gameObject);
    }

    public void Unregister(GameObject gameObject)
    {
        enemyTarget.Remove(gameObject);
    }

    public void Initialize()
    {
        enemyTarget.Clear();
    }

    public GameObject GetNewTarget(Vector3 position)
    {
        if (enemyTarget.Count == 0)
            return null;

        float distance = float.MaxValue;

        GameObject newTarget = null;

        for(int i = enemyTarget.Count - 1; i >= 0; i--)
        {
            float newDistance = Vector3.Distance(position, enemyTarget[i].transform.position);

            if (newDistance < distance)
            {
                distance = newDistance;
                newTarget = enemyTarget[i];
            }
        }

        return newTarget;
    }
}
