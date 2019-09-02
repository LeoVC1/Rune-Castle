using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Controller", menuName = "Scriptable Objects/Managers/Game Controller")]
public class GameController : ScriptableObject
{
    public GameState gameState;
}

public enum GameState
{
    GAMEPLAY,
    PAUSED
}
