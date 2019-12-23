using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LBCore;
using System;

public class ShipWeapons : MonoBehaviour
{
    public List<GameObject> turretPrefabs = new List<GameObject>();
    public List<TurretController> turrets = new List<TurretController>();
    public Hardpoint[] hardpoints;

    private void Start()
    {
        InitializeHardpoints();
    }

    private void InitializeHardpoints()
    {
        if (hardpoints.Length == turretPrefabs.Count)
        {
            for(int i = 0; i < hardpoints.Length; i++)
            {
                if (turretPrefabs[i] != null)
                {
                    TurretController tc = turretPrefabs[i].GetComponent<TurretController>();

                    if (tc.Attributes.Size == hardpoints[i].Size)
                    {
                        GameObject newTurret = Instantiate(turretPrefabs[i], hardpoints[i].transform.position, hardpoints[i].transform.rotation);
                        newTurret.transform.parent = hardpoints[i].transform;
                        turrets.Add(newTurret.GetComponent<TurretController>());
                    }
                    else
                    {
                        Debug.LogError("HARDPOINT/TURRET MISTMATCH  ::  SIZE");
                    }
                }
            }
        }
    }

    public void ToggleTurretFire()
    {
        foreach(TurretController turret in turrets)
        {
            turret.ToggleFiring();
        }
    }

    public void UpdateSelectedTarget(Transform t)
    {
        foreach(TurretController turret in turrets)
        {
            turret.SetTarget(t);
        }
    }
}
