using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using BGCore.GlobalVariables;

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

    public float CurrentVelocity = 0;
    
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
            Vector3 dir = Vector3.up * (direction * Mathf.Abs(Attributes.Torque));
            rb.AddTorque(dir);
        }
    }

    public void WarpToPoint(Vector2 destination)
    {
        rb.position = destination - ScaleSpaceManager.ScaleSpaceOffset;
    }
    #endregion

    #region Docking
    private DockingPort AvailableDock;
    public Vector3 DockingPortPosition;
    private bool isDocked = false;
    private bool isNearStation = false;
    public void SetDockingPort(Vector3 port)
    {
        DockingPortPosition = port;
    }
    public void UpdateAvailableDock(DockingPort dock)
    {
        AvailableDock = dock;
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
        isDocked = true;
        rb.velocity = Vector3.zero;
    }
    private void Undock()
    {
        isDocked = false;
    }

    public enum DockingStates
    {
        None,
        OutOfRange,
        WithinRange,
        Docked
    }
    public DockingStates DockingState;
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
    public Vector3 currentForce;

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
            if (force.magnitude > 0.01f)
            {
                force = transform.TransformDirection(force);
                currentForce = force;
                rb.AddForce(force);
            }
            else
            {
                currentForce = Vector3.zero;
            }

            if(rb.velocity.magnitude < 1 && currentInput.magnitude == 0)
            {
                rb.velocity = Vector3.zero;
                currentForce = Vector3.zero;
            }

            //Speed Limit
            else if(rb.velocity.magnitude > Constants.SpeedLimit)
            {
                rb.velocity = rb.velocity.normalized * Constants.SpeedLimit;
            }

            CurrentVelocity = rb.velocity.magnitude;
        }
    }

    #endregion

    #region ScaleSpaceUpdate
    public bool doScaleSpaceUpdate = false;
    public float FloatingOriginUpdateThreshhold = 50.0f;

    private void FixedUpdate()
    {
        if (doScaleSpaceUpdate)
        {
            if (rb.position.magnitude > FloatingOriginUpdateThreshhold)
            {
                Vector3 oldVelocity = rb.velocity;
                rb.velocity = oldVelocity;

                Vector3 oldPosition = rb.position;
                ScaleSpaceManager.UpdateScaleSpaceOffset(oldPosition);
                Vector3 newPosition = Vector3.zero;
                newPosition.x = 0;
                newPosition.y = oldPosition.y;
                newPosition.z = 0;
                rb.position = newPosition;
            }
        }

        if (!isDocked)
        {
            if (rb.position.y > 500 || rb.position.y < 0)
            {
                Vector3 pos = rb.position;
                Vector3 vel = rb.velocity;
                pos.y = Mathf.Clamp(pos.y, 0, 500);
                vel.y = 0;
                rb.position = Vector3.MoveTowards(rb.position, pos, 0.01f);
                rb.velocity = vel;
            }
        }
        else
        {
            Vector3 pos = AvailableDock.transform.position - DockingPortPosition;
            rb.position = Vector3.MoveTowards(rb.position, pos, 0.01f);
            rb.velocity = Vector3.zero;
        }

        CorrectRotationalDrift();
    }

    private void CorrectRotationalDrift()
    {
        Vector3 rotation = rb.rotation.eulerAngles;

        rotation.x = 0;
        rotation.z = 0;

        rb.rotation = Quaternion.Euler(rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StationLimits"))
        {
            isNearStation = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("StationLimits"))
        {
            isNearStation = false;
        }
    }

    private void Update()
    {
        
        if (isNearStation)
        {
            if (AvailableDock != null)
            {
                if (isDocked)
                {
                    DockingState = DockingStates.Docked;
                }
                else
                {
                    DockingState = DockingStates.WithinRange;
                }
            }
            else
            {
                DockingState = DockingStates.OutOfRange;
            }
        }
        else
        {
            DockingState = DockingStates.None;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + DockingPortPosition, 0.5f);

        Gizmos.DrawLine(transform.position, transform.position + currentForce);
    }
    #endregion
}
