﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentinel : PlayerSkill
{
    [Header("Sentinel Properties:")]
    public GridManager gridManager;
    public GameObject sentinelPrefab;
    public  GameEventListener listener;

    private Vector3 sentinelLocation;

    [Range(1, 30)]
    public float sentinelRange;

    private void Update()
    {
        WaitingConfirmation();
    }

    public override void CastSkill()
    {
        (Vector3 newPosition, Vector3 ID) = gridManager.GetClosestPoint(transform.position + transform.forward * gridManager.cellSize);
        if (!gridManager.UsingID(ID))
        {
            Instantiate(sentinelPrefab, newPosition, Quaternion.identity);
            waitingConfirmation = false;
        }
    }

    public override void WaitingConfirmation()
    {
        if (waitingConfirmation)
        {
            listener.enabled = true;
        }
        else
        {
            listener.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (waitingConfirmation)
        {
            (Vector3 newPosition, Vector3 ID) = gridManager.GetClosestPoint(transform.position + transform.forward * gridManager.cellSize);

            if (!gridManager.UsingID(ID))
            {
                Gizmos.DrawWireCube(newPosition, new Vector3(gridManager.cellSize, gridManager.cellSize, gridManager.cellSize));

                float theta = 0;
                float x = sentinelRange * Mathf.Cos(theta);
                float y = sentinelRange * Mathf.Sin(theta);
                Vector3 pos = newPosition + new Vector3(x, 0, y);
                Vector3 newPos = pos;
                Vector3 lastPos = pos;
                for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
                {
                    x = sentinelRange * Mathf.Cos(theta);
                    y = sentinelRange * Mathf.Sin(theta);
                    newPos = newPosition + new Vector3(x, 0, y);
                    Gizmos.DrawLine(pos, newPos);
                    pos = newPos;
                }
                Gizmos.DrawLine(pos, lastPos);
            }

        }
    }

}
