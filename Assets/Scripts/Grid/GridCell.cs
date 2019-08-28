using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridCell : MonoBehaviour
{
    public GridManager gridManager;

    Vector3 originalScale;

    private bool _lock;

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
        //if (!_lock)
        //{
        //    originalScale = transform.localScale;
        //    _lock = true;
        //}
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
        Vector3 newSize = gridManager.cellSize * Vector3.one/* * originalScale*/;
        transform.localScale = newSize;
    }

    public void Rotate(float angle)
    {
        transform.localEulerAngles += new Vector3(0, angle, 0);
    }
}
