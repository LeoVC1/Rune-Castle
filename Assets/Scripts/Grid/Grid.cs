using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    [Range(0, 10)]
    public int cellWidth = 5;
    [Range(0, 10)]
    public int cellHeight = 5;
    [Range(0, 10)]
    public int cellLength = 5;

    [Range(0, 5)]
    public float cellSize;

    public Vector3[,,] points;

    [Header("Gizmos:")]
    public bool showGrid;
    [ColorUsage(true)]
    public Color gizmoColor;

    public void CalculatePoints()
    {
        points = new Vector3[cellHeight,cellLength, cellWidth];

        for(int i = 0; i < cellHeight; i++)
        {
            for(int j = 0; j < cellLength; j++)
            {
                for (int k = 0; k < cellWidth; k++)
                {
                    points[i, j, k].x = (transform.position.x + (i * cellSize));
                    points[i, j, k].z = (transform.position.z + (j * cellSize));
                    points[i, j, k].y = (transform.position.y + (k * cellSize));
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(showGrid)
        {
            Gizmos.color = gizmoColor;

            CalculatePoints();

            Vector3 size = new Vector3(cellSize, cellSize, cellSize);

            foreach (Vector3 p in points)
            {
                Gizmos.DrawCube(p, size);
            }
        }
    }
}
