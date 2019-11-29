using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScaleSpaceManager
{
    private static Vector2 scaleSpaceOffset;
    public static Vector2 ScaleSpaceOffset
    {
        get { return scaleSpaceOffset; }
        private set
        {
            scaleSpaceOffset += value;
            Debug.Log("New Offset = " + scaleSpaceOffset.ToString());
        }
    }

    public static void UpdateScaleSpaceOffset (Vector2 offset)
    {
        ScaleSpaceOffset = offset;
        UpdateScaleSpaceObjects(ScaleSpaceOffset);
    }

    public static void UpdateScaleSpaceOffset(Vector3 offset)
    {
        Vector2 newOffset = new Vector2(offset.x, offset.z);
        ScaleSpaceOffset = newOffset;
        UpdateScaleSpaceObjects(ScaleSpaceOffset);
    }

    public delegate void UpdateSSObjects(Vector2 offset);
    public static UpdateSSObjects UpdateScaleSpaceObjects;
}
