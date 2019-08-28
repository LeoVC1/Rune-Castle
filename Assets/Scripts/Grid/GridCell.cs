using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridCell : MonoBehaviour
{
    public GridManager gridManager;

    public Vector3 ConstantID;

    void Update()
    {
#if UNITY_EDITOR
        if (transform.hasChanged)
        {
            GetNewPosition();
            transform.hasChanged = false;
        }
#endif
    }

    public void GetNewPosition()
    {
        transform.position = gridManager.GetClosestPoint(transform.position, this);
    }

    public void SetConstantID(Vector3 newID)
    {
        ConstantID = newID;
    }

    public Vector3 GetConstantID()
    {
        return ConstantID;
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
