using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapons : MonoBehaviour
{
    public TurretController[] turrets;

    public void ToggleTurretFire()
    {
        foreach(TurretController turret in turrets)
        {
            turret.ToggleFiring();
        }
    }
}
