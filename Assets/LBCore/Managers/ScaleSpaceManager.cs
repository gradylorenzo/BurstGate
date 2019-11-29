using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScaleSpaceManager
{
    private static Vector3 scaleSpaceOffset;
    public static Vector3 ScaleSpaceOffset
    {
        get { return scaleSpaceOffset; }
        private set
        {
            scaleSpaceOffset += value;
        }
    }

    public static void UpdateScaleSpaceOffset (Vector3 offset)
    {
        ScaleSpaceOffset = offset;
        UpdateScaleSpaceObjects(ScaleSpaceOffset);
    }

    public delegate void UpdateSSObjects(Vector3 offset);
    public static UpdateSSObjects UpdateScaleSpaceObjects;
}
