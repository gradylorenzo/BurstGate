using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipDynamics : MonoBehaviour
{
    [Serializable]
    public struct ShipDynamicsAttributes
    {
        public Vector3 Thrust;
        public float Torque;

        public Vector3 DockingPortPosition;
    }

    [SerializeField]
    public ShipDynamicsAttributes Attributes;
    
    private Rigidbody rb;
    [Space(20)]
    [Header("MAKE THESE PRIVATE AFTER TESTING")]
    //MAKE THESE PRIVATE AFTER TESTING
    public bool isDocked;

    //

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    public void UpdateAvailablePort(GameObject port)
    {

    }

    public void Dock()
    {

    }

    public void Undock()
    {

    }
    #endregion
}
