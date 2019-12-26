using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using BGCore;
using BGCore.Data;

[RequireComponent(typeof(Rigidbody))]
public class ShipDynamics : MonoBehaviour
{
    #region Common
    [SerializeField]
    public MobilityStats mobility
    {
        get; private set;
    }
    public Rigidbody rb;
    public float CurrentVelocity
    {
        get; private set;
    }
    private bool b_isControlled;
    public bool isControlled
    {
        get { return b_isControlled; }
        set { b_isControlled = value; }
    }

    private void Awake()
    {
        GameManager.Events.EFloatingOriginOffsetDelta += EFloatingOriginOffsetDelta;
        GameManager.Events.EUpdatePlayerShip += EUpdatePlayerShip;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        AvailableDock = null;
    }

    private void FixedUpdate()
    {
        FloatingOriginUpdate();

        Docking();

        CorrectRotationalDrift();
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

    #region Public Methods

    public void UpdateMobilityStats(MobilityStats m)
    {
        mobility = m;
        Notify.Log(Notify.Intent.Success, "Mobility Stats Updated!");
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
            Vector3 dir = Vector3.up * (direction * Mathf.Abs(mobility.Torque));
            rb.AddTorque(dir);
        }
    }

    public void WarpToPoint(Vector2 destination)
    {
        throw new NotImplementedException();
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
    private void Docking()
    {
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

        dampenerTrust = Vector3.Scale(dampenerTrust, mobility.Thrust) * mobility.InertialDampenerMultiplier;
        currentInput = Vector3.Scale(currentInput, mobility.Thrust);

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
    private void CorrectRotationalDrift()
    {
        Vector3 rotation = rb.rotation.eulerAngles;

        rotation.x = 0;
        rotation.z = 0;

        rb.rotation = Quaternion.Euler(rotation);
    }
    #endregion

    #region Floating Origin Update
    public bool doScaleSpaceUpdate = false;
    public float FloatingOriginUpdateThreshhold = 50.0f;

    private void FloatingOriginUpdate()
    {
        if (b_isControlled)
        {
            Vector2 rbpositionv2 = new Vector2(rb.position.x, rb.position.z);

            if(rbpositionv2.magnitude > Constants.FloatingOriginUpdateThreshhold)
            {
                Vector3 oldVelocity = rb.velocity;
                Vector3 oldPosition = rb.position;
                GameManager.FloatingOrigin.UpdateFloatingOriginOffset(oldPosition, GameManager.FloatingOrigin.UpdateOffsetMode.Additive);
                Vector3 newPosition = Vector3.zero;
                newPosition.x = 0;
                newPosition.y = oldPosition.y;
                newPosition.z = 0;
                rb.position = newPosition;
                rb.velocity = oldVelocity;
            }
        }
        else
        {
            //Allow this ship to recieve calls from FOM
        }
    }

    private void EFloatingOriginOffsetDelta(DoubleVector2 v)
    {
        if (!isControlled)
        {
            Vector2 single = DoubleVector2.ToVector2(v);
            Vector3 offset = new Vector3(single.x, 0, single.y);
            transform.position -= offset;
        }
    }
    #endregion

    private void EUpdatePlayerShip(ShipDynamics sd)
    {
        isControlled = this == sd;
    }
}
