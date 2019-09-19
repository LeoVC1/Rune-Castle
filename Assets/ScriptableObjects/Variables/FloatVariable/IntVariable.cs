using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Scriptable Objects/Variables/Int Variable")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int _value;
    [SerializeField] private int constantValue;
    public bool clamp;
    public int min;
    public int max;

    public int Value
    {
        get { return _value; }
        set
        {
            if (clamp)
            {
                _value = (value >= 0 && value <= constantValue) ? value : (value <= 0) ? 0 : constantValue;
            }
            else
            {
                _value = value;
            }
        }
    }
    public int ConstantValue { get { return constantValue; } set { constantValue = value; } }
}
