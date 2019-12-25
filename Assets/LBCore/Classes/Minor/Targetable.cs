using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGCore;

public class Targetable : MonoBehaviour
{
    public float timeToLock = 2.0f;
    public float mouseDownTime = 0;
    public bool isBeingTargeted = false;

    private void OnMouseDown()
    {
        mouseDownTime = Time.time;
        isBeingTargeted = true;
    }

    private void OnMouseUp()
    {
        isBeingTargeted = false;
        GameManager.Events.EUpdatePendingSelection(null, 1, 1);
    }

    private void Update()
    {
        if (isBeingTargeted)
        {
            if (Time.time > mouseDownTime + timeToLock)
            {
                GameManager.Events.EUpdateSelectedTarget(transform);
            }
            else
            {
                GameManager.Events.EUpdatePendingSelection(transform, ((mouseDownTime + timeToLock) - Time.time), timeToLock);
            }
        }
    }
}
