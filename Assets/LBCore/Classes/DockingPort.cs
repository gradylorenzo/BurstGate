using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingPort : MonoBehaviour
{
    #region DataTypes
    public enum DockingPortTypes
    {
        playerShip,
        playerOnly,
        droneShip,
        dronesOnly
    }
    #endregion

    #region Fields
    
    public DockingPortTypes PortType;
    public Vector3 DockingPortOffset;
    public ShipDynamics sd;

    public void Awake()
    {
        gameObject.tag = "DockingPort";
        
        if(PortType == DockingPortTypes.playerOnly)
        {
            DockingPortOffset += transform.position;
        }
    }

    public void Start()
    {
        if (PortType == DockingPortTypes.playerShip)
        {
            if (sd == null)
            {
                Debug.LogWarning("Docking port has not been assigned a ShipDynamics component. Did you mean to set this port as PlayerOnly?");
            }
            else
            {
                sd.SetDockingPortOffset(DockingPortOffset);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(PortType == DockingPortTypes.playerShip)
        {
            if (sd != null)
            {
                if (other.gameObject.tag == "DockingPort")
                {
                    DockingPort newPort = other.GetComponent<DockingPort>();
                    if (newPort.PortType == DockingPortTypes.playerOnly)
                    {
                        sd.UpdateAvailableDock(newPort);
                        print("New Docking Port Found");
                    }
                }
            }
            else
            {
                print("NO SHIP DYNAMICS ASSIGNED!!");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (PortType == DockingPortTypes.playerShip)
        {
            if (other.gameObject.tag == "DockingPort")
            {
                sd.UpdateAvailableDock(null);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + DockingPortOffset, 0.5f);
    }
    #endregion
}
