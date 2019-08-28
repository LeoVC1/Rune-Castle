using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridCell : MonoBehaviour
{
    public GridManager gridManager;

    void Update()
    {
        if (Application.isPlaying)
            return;

        if (transform.hasChanged)
        {
            GetNewPosition();
            transform.hasChanged = false;
        }

    }

    public void GetNewPosition()
    {
        transform.position = gridManager.GetClosestPoint(transform.position, this);
    }

    private void OnEnable()
    {
        gridManager.RegisterCell(this);
        Resize();
    }

    private void OnDisable()
    {
        gridManager.RegisterCell(this);
        Resize();
    }

    public void Resize()
    {
        float newSize = gridManager.cellSize + (gridManager.cellSize * gridManager.cellSizeOffset);
        transform.localScale = new Vector3(newSize, newSize, newSize);
    }
}
