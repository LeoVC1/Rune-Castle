﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Scriptable Objects/Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
    public float Value;
    public float ConstantValue;
}
