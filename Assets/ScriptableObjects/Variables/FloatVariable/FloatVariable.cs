using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Scriptable Objects/Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float _value;
    [SerializeField] private float constantValue;
    public bool clamp;
    public float min;
    public float max;

    public float Value
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
    public float ConstantValue { get { return constantValue; } set { constantValue = value; } }
}
