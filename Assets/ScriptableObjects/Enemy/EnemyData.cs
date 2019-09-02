using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Objects/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHealth;
    public float range;
}
