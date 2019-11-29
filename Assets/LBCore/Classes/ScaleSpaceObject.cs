using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSpaceObject : MonoBehaviour
{
    [System.Serializable]
    public enum ScaleSpaces
    {
        SS0 = 1,
        SS1 = 1000,
        SS2 = 1000000
    }

    public ScaleSpaces Scale = ScaleSpaces.SS0;
    private Vector3 originalPosition;

    public void Awake()
    {
        ScaleSpaceManager.UpdateScaleSpaceObjects += UpdateOffset;
        originalPosition = transform.position;
    }

    public void UpdateOffset(Vector3 offset)
    {
        transform.position = originalPosition - (offset / (int)Scale);
    }
}
