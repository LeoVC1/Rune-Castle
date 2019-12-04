﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Jogador
{
    public int id;
    public string nome;
    public string nick;
    public object[] jogada;

    public override string ToString()
    {
        return $"id:{id} nome:{nome}";
    }
}