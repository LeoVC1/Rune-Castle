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

    private void Start()
    {
        gridManager.RegisterCell(this);
    }

    void Update()
    {
        if ((Application.isPlaying || gridManager._lock))
            return;

        if (transform.hasChanged)
        {
            GetNewPosition();
            transform.hasChanged = false;
        }

    }

    public void GetNewPosition(bool checkPresence = false)
    {
        gridManager.usingID.Remove(ID);
        (Vector3 newPosition, Vector3 newID) = gridManager.GetClosestPoint(transform.position, this, true);
        ID = newID;
        transform.position = newPosition;
    }

    private void OnEnable()
    {
        if (gridManager._lock)
            return;

        gridManager.usingID.Remove(ID);
        (Vector3 newPosition, Vector3 newID) = gridManager.GetClosestPoint(transform.position, this, true);
        ID = newID;
        gridManager.RegisterCell(this);
        Resize();
    }

    private void OnDisable()
    {
        gridManager.UnRegisterCell(this);
        gridManager.RemoveID(ID);
    }

    public void Resize()
    {
        Vector3 newSize = gridManager.cellSize * Vector3.one;
        transform.localScale = newSize;
    }

    public void Rotate(float angle)
    {
        transform.localEulerAngles += new Vector3(0, angle, 0);
    }
}
