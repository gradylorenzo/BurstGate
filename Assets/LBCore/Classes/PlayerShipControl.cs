using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LBCore;

[RequireComponent(typeof(ShipDynamics))]
public class PlayerShipControl : MonoBehaviour
{
    private ShipDynamics sd;
    private ShipWeapons sw;

    private void Start()
    {
        sd = GetComponent<ShipDynamics>();
        sw = GetComponent<ShipWeapons>();
        GameManagerCore.Events.EUpdatePlayerShip(sd);
    }

    private void FixedUpdate()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        float t = 0;

        if (!GameManagerCore.isUsingInterface)
        {
            x = Input.GetAxis("CONTROL_X");
            y = Input.GetAxis("CONTROL_Y");
            z = Input.GetAxis("CONTROL_Z");
            t = Input.GetAxis("CONTROL_TORQUE");
        }
        sd.ApplyThrust(new Vector3(x, y, z));
        sd.ApplyTorque(t);
    }

    private void Update()
    {
        if (!GameManagerCore.isUsingInterface)
        {
            if (Input.GetButtonDown("CONTROL_DOCK"))
            {
                sd.SwitchDock();
            }

            if (Input.GetButtonDown("CONTROL_TOGGLE_INERTIAL_DAMPENERS"))
            {
                sd.ToggleInertialDampeners();
            }

            if (Input.GetButtonDown("CONTROL_TOGGLE_FIRING"))
            {
                sw.ToggleTurretFire();
            }
        }
    }
}
