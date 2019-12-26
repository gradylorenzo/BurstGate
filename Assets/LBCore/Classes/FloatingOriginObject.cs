using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGCore;
using BGCore.Data;

public class FloatingOriginObject : MonoBehaviour
{

    private void Awake()
    {
        GameManager.Events.EFloatingOriginOffsetDelta += EFloatingOriginOffsetDelta;
    }

    private void EFloatingOriginOffsetDelta(DoubleVector2 v)
    {
        Vector2 single = DoubleVector2.ToVector2(v);
        Vector3 offset = new Vector3(single.x, 0, single.y);

        transform.position -= offset;
    }
}
