﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Grid Manager", menuName = "Scriptable Objects/Grid/Grid Manager")]
public class GridManager : ScriptableObject
{
    public int cellWidth;
    public int cellHeight = 5;
    public int cellLength = 5;
    public float cellSize;
    public float cellSizeOffset;

    public Vector3[,,] points;

    List<GridCell> allCells;

    List<Vector3> allIDs;

    public void CalculatePoints(Vector3 position)
    {
        points = new Vector3[cellHeight, cellLength, cellWidth];

        for (int i = 0; i < cellHeight; i++)
        {
            for (int j = 0; j < cellLength; j++)
            {
                for (int k = 0; k < cellWidth; k++)
                {
                    points[i, j, k].x = (position.x + (k * cellSize));
                    points[i, j, k].z = (position.z + (j * cellSize));
                    points[i, j, k].y = (position.y + (i * cellSize));
                }
            }
        }
    }

    public void RegisterCell(GridCell cell)
    {
        allCells.Add(cell);
        cell.GetNewPosition();
    }

    public void UnRegisterCell(GridCell cell)
    {
        allIDs.Remove(cell.GetConstantID());
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
        float distance = float.MaxValue;
        Vector3 newPosition = Vector3.zero;
        Vector3 ID = new Vector3();

        for (int i = 0; i < cellHeight; i++)
        {
            for (int j = 0; j < cellLength; j++)
            {
                for (int k = 0; k < cellWidth; k++)
                {
                    Vector3 aux = new Vector3(i, j, k);
                    if (allIDs.Contains(aux))
                        continue;

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

        allIDs.Remove(cell.GetConstantID());
        cell.SetConstantID(ID);
        allIDs.Add(ID);
        return newPosition;
    }
}
