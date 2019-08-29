using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridCell : MonoBehaviour
{
    public GridManager gridManager;
    public Vector3 ID;
    public Vector3 possibleID;

    private Vector3 originalScale;
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

    public void GetNewPosition(bool checkPresence = false)
    {
        //if (checkPresence)
        //{
        //    gridManager.GetClosestPoint(transform.position, this, false, true);
        //    if (!gridManager.UsingID(possibleID))
        //    {
        //        transform.position = gridManager.GetClosestPoint(transform.position, this, true);
        //    }
        //}
        //else
        //{
        //    transform.position = gridManager.GetClosestPoint(transform.position, this, true);
        //}
        gridManager.usingID.Remove(ID);
        (Vector3 newPosition, Vector3 newID) = gridManager.GetClosestPoint(transform.position, this, true);
        ID = newID;
        transform.position = newPosition;
    }

    private void OnEnable()
    {
        //if (!_lock)
        //{
        //    originalScale = transform.localScale;
        //    _lock = true;
        //}
        gridManager.usingID.Remove(ID);
        (Vector3 newPosition, Vector3 newID) = gridManager.GetClosestPoint(transform.position, this, true);
        ID = newID;
        gridManager.RegisterCell(this);
        Resize();
    }

    private void OnDisable()
    {
        gridManager.UnRegisterCell(this);
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
