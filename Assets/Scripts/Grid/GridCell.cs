using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridCell : MonoBehaviour
{
    public GridManager gridManager;

    void Update()
    {
        if (transform.hasChanged)
        {
            print("The transform has changed!");
            GetNewPosition();
            transform.hasChanged = false;
        }
    }

    public void GetNewPosition()
    {
        transform.position = gridManager.GetClosestPoint(transform.position);
    }

    private void OnEnable()
    {
        gridManager.RegisterCell(this);
        Resize(gridManager.cellSize);
    }

    private void OnDisable()
    {
        gridManager.RegisterCell(this);
        Resize(gridManager.cellSize);
    }

    public void Resize(float newSize)
    {
        transform.localScale = new Vector3(newSize, newSize, newSize);
    }
}
