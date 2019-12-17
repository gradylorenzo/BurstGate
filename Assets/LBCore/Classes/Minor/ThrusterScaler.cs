using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScaler : MonoBehaviour
{
    public ShipDynamics playerShip;
    public float defaultScale = 0.1f;
    public float scaleMultiplier = 1.0f;
    private AnimationCurve scalingCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.6f, 0), new Keyframe(1, 1));

    private Vector3 thrustDirectionPosition;
    private Quaternion thrustDirection;
    private float zScale = 0.0f;

    public void Update()
    {
        if (playerShip.currentForce.magnitude > 0.1)
        {
            thrustDirectionPosition = playerShip.currentForce;
        }
        else
        {
            thrustDirectionPosition = -Vector3.forward;
        }
        thrustDirection = Quaternion.LookRotation(thrustDirectionPosition - transform.position);
        float thrustAngle = Vector3.Angle(transform.forward, thrustDirectionPosition);
        zScale = Mathf.Lerp(zScale, defaultScale + (scalingCurve.Evaluate(thrustAngle / 180) * scaleMultiplier), 0.01f);
        Vector3 scale = new Vector3(transform.localScale.x, transform.localScale.y, zScale);

        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * scaleMultiplier);
        Gizmos.DrawLine(transform.position, transform.position + thrustDirectionPosition);
    }
}
