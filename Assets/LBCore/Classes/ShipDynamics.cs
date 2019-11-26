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

        public Vector3 DockingPortPosition;
    }
    #endregion

    #region Fields
    public string shipID;
    [SerializeField]
    public ShipDynamicsAttributes Attributes;
    #endregion

    #region Properties
    public Rigidbody rb { get; private set; }
    public bool isDocked { get; private set; }
    #endregion
    public DockingPort AvailableDock { get; private set; }
    public Vector3 DockingPortOffset;
    public bool dockAvailable = false;
    #region Methods
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        AvailableDock = null;
    }

    #region public methods
    public void ApplyThrust(Vector3 direction)
    {
        if (!isDocked)
        {
            rb.AddRelativeForce(Vector3.Scale(direction, Attributes.Thrust));
        }
    }

    public void ApplyTorque (float direction)
    {
        if (!isDocked)
        {
            rb.AddRelativeTorque(Vector3.up * (direction * Mathf.Abs(Attributes.Torque))); //Mathf.Abs prevents accidental negative values inverting control direction
        }
    }

    public void SetDockingPortOffset(Vector3 offset)
    {
        DockingPortOffset = offset;
    }

    public void UpdateAvailableDock(DockingPort dock)
    {
        AvailableDock = dock;
        if(dock != null)
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
            if(AvailableDock != null)
            {
                Dock();
            }
        }
    }
    #endregion

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
    #endregion
}
