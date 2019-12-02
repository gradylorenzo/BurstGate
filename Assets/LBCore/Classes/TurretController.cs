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
            else
            {
                doBaseDefaultRotation();
                doBarrelDefaultRotation();
            }
        }
        else
        {
            Debug.LogWarning("One of the variables of TurretController has not been assigned.");
        }
    }

    private void doBaseRotation()
    {
        var directionToTarget = (Target.position - transform.position).normalized;
        var worldAxis = transform.up;
        var flattenedDirection = Vector3.ProjectOnPlane(directionToTarget, worldAxis);

        //var rotation = Quaternion.RotateTowards(TurretBase.rotation, Quaternion.LookRotation(flattenedDirection, worldAxis), Attributes.LookSpeed);
        var rotation = Quaternion.LookRotation(flattenedDirection, worldAxis);
        TurretBase.rotation = rotation;
    }

    private void doBaseDefaultRotation()
    {
        var directionToTarget = (transform.position + new Vector3(0, 0, 1) - transform.position).normalized;
        var worldAxis = transform.up;
        var flattenedDirection = Vector3.ProjectOnPlane(directionToTarget, worldAxis);

        var rotation = Quaternion.RotateTowards(TurretBase.rotation, Quaternion.LookRotation(flattenedDirection, worldAxis), Attributes.LookSpeed);
        //var rotation = Quaternion.LookRotation(flattenedDirection, worldAxis);
        TurretBase.rotation = rotation;
    }

    private void doBarrelRotation()
    {
        var directionToTarget = (Target.position - TurretBarrel.position).normalized;
        var axis = TurretBase.right;
        var flattenedDirection = Vector3.ProjectOnPlane(directionToTarget, axis);
        var signedAngle = Vector3.SignedAngle(TurretBase.forward, flattenedDirection, axis);
        signedAngle = Mathf.Clamp(signedAngle, MinBarrelAngle, MaxBarrelAngle);

        /*var rotation = Quaternion.RotateTowards(TurretBarrel.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(signedAngle, axis) * TurretBase.forward,
            TurretBarrel.up), Attributes.LookSpeed);*/
        var rotation = Quaternion.LookRotation(Quaternion.AngleAxis(signedAngle, axis) * TurretBase.forward,
            TurretBarrel.up);
        TurretBarrel.rotation = rotation;
    }

    private void doBarrelDefaultRotation()
    {
        var directionToTarget = (TurretBarrel.position + new Vector3(0, 0, 1) - TurretBarrel.position).normalized;
        var axis = TurretBase.right;
        var flattenedDirection = Vector3.ProjectOnPlane(directionToTarget, axis);
        var signedAngle = Vector3.SignedAngle(TurretBase.forward, flattenedDirection, axis);
        signedAngle = Mathf.Clamp(signedAngle, MinBarrelAngle, MaxBarrelAngle);

        /*var rotation = Quaternion.RotateTowards(TurretBarrel.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(signedAngle, axis) * TurretBase.forward,
            TurretBarrel.up), Attributes.LookSpeed);*/
        var rotation = Quaternion.LookRotation(Quaternion.AngleAxis(signedAngle, axis) * TurretBase.forward,
            TurretBarrel.up)
        ;
        TurretBarrel.rotation = rotation;
    }
}
