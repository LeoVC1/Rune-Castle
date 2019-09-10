using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public EnemyTargetManager enemyTargetManager;

    private void OnEnable()
    {
        enemyTargetManager.Register(this.gameObject);
    }

    private void OnDisable()
    {
        enemyTargetManager.Unregister(this.gameObject);
    }
}
