using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGCore;
using System;

public class FloatingOriginObject : MonoBehaviour
{
    private Vector3 originalPosition;

    private void Awake()
    {
        GameManager.Events.EFloatingOriginOffsetUpdated += EFloatingOriginOffsetUpdated;
    }

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void EFloatingOriginOffsetUpdated(DoubleVector2 v)
    {
        Vector3 oldPosition = transform.position;

        Vector2 offset = DoubleVector2.ToVector2(v);
        Vector3 localOffset = new Vector3(offset.x, 0, offset.y);

        Vector3 newPosition = oldPosition - localOffset;
        transform.position = newPosition;
    }
}
