using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform Target;

    public Transform TurretBase;
    public Transform TurretBarrel;

    [Serializable]
    public struct TurretAttributes
    {
        public float LookSpeed;
    }

    public TurretAttributes Attributes;

    public bool useSmoothMovement = true;
    private bool isAligned
    {
        get
        {
            if (Target != null)
            {
                if (Vector3.Angle(TurretBarrel.rotation.eulerAngles, Quaternion.LookRotation(Target.position - TurretBarrel.position).eulerAngles) < 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    private Vector3 targetPos
    {
        get
        {
            if (Target != null)
            {
                return Target.transform.position;
            }
            else
            {
                return new Vector3(0, 0, 1);
            }
        }
    }
    private float MinBarrelAngle = -180;
    private float MaxBarrelAngle = 0;

    public void Update()
    {
        if(TurretBase != null && TurretBarrel != null)
        {
            if (Target != null)
            {
                doBaseRotation();
                doBarrelRotation();
            }
        }
        else
        {
            Debug.LogWarning("One of the variables of TurretController has not been assigned.");
        }
    }

    private void doBaseRotation()
    {
        var directionToTarget = (targetPos - transform.position).normalized;
        var worldAxis = transform.up;
        var flattenedDirection = Vector3.ProjectOnPlane(directionToTarget, worldAxis);

        Quaternion rotation;
        if (useSmoothMovement)
        {
            rotation = Quaternion.RotateTowards(TurretBase.rotation, Quaternion.LookRotation(flattenedDirection, worldAxis), Attributes.LookSpeed);
        }
        else
        {
            rotation = Quaternion.LookRotation(flattenedDirection, worldAxis);
        }
        TurretBase.rotation = rotation;
    }

    private void doBarrelRotation()
    {
        var directionToTarget = (targetPos - TurretBarrel.position).normalized;
        var axis = TurretBase.right;
        var flattenedDirection = Vector3.ProjectOnPlane(directionToTarget, axis);
        var signedAngle = Vector3.SignedAngle(TurretBase.forward, flattenedDirection, axis);
        signedAngle = Mathf.Clamp(signedAngle, MinBarrelAngle, MaxBarrelAngle);

        Quaternion rotation;

        if (useSmoothMovement)
        {
            rotation = Quaternion.RotateTowards(TurretBarrel.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(signedAngle, axis) * TurretBase.forward,
                TurretBarrel.up), Attributes.LookSpeed);
        }
        else
        {
            rotation = Quaternion.LookRotation(Quaternion.AngleAxis(signedAngle, axis) * TurretBase.forward,
            TurretBarrel.up);
        }
        TurretBarrel.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Ray(TurretBarrel.position, TurretBarrel.forward * 2));
        Gizmos.DrawRay(new Ray(TurretBase.position, TurretBase.forward * 2));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(TurretBarrel.position, TurretBarrel.right * 2));
        Gizmos.DrawRay(new Ray(TurretBase.position, TurretBase.right * 2));

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(TurretBarrel.position, TurretBarrel.up * 5));
    }
}
