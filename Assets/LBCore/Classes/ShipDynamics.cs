using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipDynamics : MonoBehaviour
{
    #region DataTypes
    [Serializable]
    public struct ShipDynamicsAttributes
    {
        public Vector3 Thrust;
        public float Torque;
        public float InertialDampenerMultiplier;
        public Vector3 DockingPortPosition;
    }
    #endregion

    #region Common
    [SerializeField]
    public ShipDynamicsAttributes Attributes;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        AvailableDock = null;
    }
    #endregion

    #region Public Methods
    public void AllowScaleSpaceUpdate()
    {

    }

    public void ToggleInertialDampeners()
    {
        useDampeners = !useDampeners;
    }
    public void ApplyThrust(Vector3 direction)
    {
        currentInput = direction;
        ProcessInput();
    }
    public void ApplyTorque(float direction)
    {
        if (!isDocked)
        {
            rb.AddRelativeTorque(Vector3.up * (direction * Mathf.Abs(Attributes.Torque))); //Mathf.Abs prevents accidental negative values inverting control direction
        }
    }
    #endregion

    #region Docking
    public DockingPort AvailableDock { get; private set; }
    public Vector3 DockingPortOffset;
    public bool dockAvailable = false;
    public bool isDocked { get; private set; }

    public void SetDockingPortOffset(Vector3 offset)
    {
        DockingPortOffset = offset;
    }
    public void UpdateAvailableDock(DockingPort dock)
    {
        AvailableDock = dock;
        if (dock != null)
        {
            dockAvailable = true;
        }
        else
        {
            dockAvailable = false;
        }
    }
    public void SwitchDock()
    {
        if (isDocked)
        {
            Undock();
            isDocked = false;
        }
        else
        {
            if (AvailableDock != null)
            {
                Dock();
            }
        }
    }
    private void Dock()
    {
        transform.position = AvailableDock.DockingPortOffset - DockingPortOffset;
        rb.velocity = Vector3.zero;
        isDocked = true;
    }
    private void Undock()
    {
        isDocked = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(rb.velocity + transform.position, 1);
    }
    #endregion

    #region Thrust Control and Inertial Dampeners
    private bool useDampeners;
    public bool UseDampeners
    {
        get
        {
            return useDampeners;
        }
    }

    private Vector3 currentInput;
    private Vector3 finalThrust;

    private void ProcessInput()
    {
        Vector3 velocity = transform.InverseTransformDirection(-rb.velocity.normalized);
        Vector3 dampenerTrust = Vector3.zero;

        if (useDampeners)
        {
            if (currentInput.x == 0)
            {
                dampenerTrust.x = velocity.x;
            }

            if (currentInput.y == 0)
            {
                dampenerTrust.y = velocity.y;
            }

            if (currentInput.z == 0)
            {
                dampenerTrust.z = velocity.z;
            }
        }

        dampenerTrust = Vector3.Scale(dampenerTrust, Attributes.Thrust) * Attributes.InertialDampenerMultiplier;
        currentInput = Vector3.Scale(currentInput, Attributes.Thrust);

        finalThrust = dampenerTrust + currentInput;

        DoFinalForce(finalThrust);

        currentInput = Vector3.zero;
        finalThrust = Vector3.zero;
    }
    private void DoFinalForce(Vector3 force)
    {
        if (!isDocked)
        {
            force = transform.TransformDirection(force);
            rb.AddForce(force);

            if(rb.velocity.magnitude < .2f && currentInput.magnitude == 0)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    #endregion

    #region ScaleSpaceUpdate
    public bool doScaleSpaceUpdate = false;
    public float FloatingOriginUpdateThreshhold = 50.0f;

    private void LateUpdate()
    {
        if (doScaleSpaceUpdate)
        {
            if(rb.position.magnitude > FloatingOriginUpdateThreshhold)
            {
                Vector3 oldVelocity = rb.velocity;
                Vector3 oldPosition = rb.position;
                ScaleSpaceManager.UpdateScaleSpaceOffset(oldPosition);
                rb.position = Vector3.zero;
                rb.velocity = oldVelocity;
            }
        }
    }
    #endregion
}
