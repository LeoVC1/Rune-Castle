using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    public GridManager SO;

    [Header("Gizmos:")]
    public bool showGrid;
    [ColorUsage(true)]
    public Color gizmoColor;

    private void OnDrawGizmos()
    {
        if (showGrid)
        {
            Gizmos.color = gizmoColor;

            SO.CalculatePoints(transform.position);

            Vector3 size = new Vector3(SO.cellSize, SO.cellSize, SO.cellSize);

            foreach (Vector3 p in SO.points)
            {
                Gizmos.DrawCube(p, size);
            }
        }
    }
}
