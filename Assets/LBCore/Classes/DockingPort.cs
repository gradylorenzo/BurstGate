using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingPort : MonoBehaviour
{
    public ShipDynamics connectedShip;

    public void OnTriggerEnter(Collider other)
    {
        if (connectedShip != null)
        {
            if (other.gameObject.tag == "BaseDockingPort")
            {
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (connectedShip != null)
        {
            if (other.gameObject.tag == "BaseDockingPort")
            {

            }
        }
    }
}
