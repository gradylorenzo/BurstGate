using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScaleSpaceManager
{
    public delegate void UpdateSSOffset(Vector3 offset);
    public static UpdateSSOffset UpdateScaleSpaceOffset;
}
