﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Grid Manager", menuName = "Scriptable Objects/Grid/Grid Manager")]
public class GridManager : ScriptableObject
{
    [HideInInspector] public int cellWidth;
    [HideInInspector] public int cellHeight = 5;
    [HideInInspector] public int cellLength = 5;
    [HideInInspector] public float cellSize;
    public float cellSizeOffset;
    public Vector3 gridStartPosition;

    public Vector3[,,] points;

    List<GridCell> allCells = new List<GridCell>();

    public void CalculatePoints()
    {
        points = new Vector3[cellHeight, cellLength, cellWidth];

        for (int i = 0; i < cellHeight; i++)
        {
            for (int j = 0; j < cellLength; j++)
            {
                for (int k = 0; k < cellWidth; k++)
                {
                    points[i, j, k].x = (gridStartPosition.x + (k * cellSize));
                    points[i, j, k].z = (gridStartPosition.z + (j * cellSize));
                    points[i, j, k].y = (gridStartPosition.y + (i * cellSize));
                }
            }
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
        Debug.Log("Points calculated sucessfully.");
        Debug.Log("Point:" + points[0, 0, 0]);
    }

    public void RegisterCell(GridCell cell)
    {
        allCells.Add(cell);
        cell.GetNewPosition();
    }

    public void UnRegisterCell(GridCell cell)
    {
        allCells.Remove(cell);
    }

    public void ResizeCells()
    {
        for(int i = 0; i < allCells.Count; i++)
        {
            if(allCells[i])
                allCells[i].Resize();
        }
    }

    public Vector3 GetClosestPoint(Vector3 previousPosition, GridCell cell)
    {
        if (points == null)
            CalculatePoints();

        float distance = float.MaxValue;
        Vector3 newPosition = Vector3.zero;
        Vector3 ID = new Vector3();

        int i = 0;
        int j = 0;
        int k = 0;

        for (i = 0; i < cellHeight; i++)
        {
            for (j = 0; j < cellLength; j++)
            {
                for (k = 0; k < cellWidth; k++)
                {
                    Vector3 aux = new Vector3(i, j, k);

                    float newDistance = Vector3.Distance(previousPosition, points[i, j, k]);

                    if (newDistance < distance)
                    {
                        distance = newDistance;
                        newPosition = points[i, j, k];
                        ID = aux;
                    }
                }
            }
        }

        return newPosition;
    }
}
