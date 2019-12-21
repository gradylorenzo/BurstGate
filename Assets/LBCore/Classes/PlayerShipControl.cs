using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LBCore;

[RequireComponent(typeof(ShipDynamics))]
public class PlayerShipControl : MonoBehaviour
{
    private ShipDynamics sd;

    private void Start()
    {
        sd = GetComponent<ShipDynamics>();
        GameManagerCore.Events.EUpdatePlayerShip(sd);
    }

    private void FixedUpdate()
    {
        if (GameManagerCore.isControllingShip)
        {
            float x = Input.GetAxis("CONTROL_X");
            float y = Input.GetAxis("CONTROL_Y");
            float z = Input.GetAxis("CONTROL_Z");
            float t = Input.GetAxis("CONTROL_TORQUE");

            sd.ApplyThrust(new Vector3(x, y, z));
            sd.ApplyTorque(t);
        }
    }

    private void Update()
    {
        if (GameManagerCore.isControllingShip)
        {
            if (Input.GetButtonDown("CONTROL_DOCK"))
            {
                sd.SwitchDock();
            }

            if (Input.GetButtonDown("CONTROL_TOGGLE_INERTIAL_DAMPENERS"))
            {
                sd.ToggleInertialDampeners();
            }
        }
    }
}
