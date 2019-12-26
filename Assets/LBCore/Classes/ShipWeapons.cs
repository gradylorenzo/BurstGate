using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGCore;
using System;

public class ShipWeapons : MonoBehaviour
{
    public List<GameObject> turretPrefabs = new List<GameObject>();
    public List<TurretController> turrets = new List<TurretController>();
    public Hardpoint[] hardpoints;

    private void Start()
    {
        InitializeHardpoints(turretPrefabs);
    }

    private void InitializeHardpoints(List<GameObject> prefabs)
    {
        if (hardpoints.Length == prefabs.Count)
        {
            for(int i = 0; i < hardpoints.Length; i++)
            {
                if (prefabs[i] != null)
                {
                    TurretController tc = prefabs[i].GetComponent<TurretController>();

                    if (tc.Attributes.size == hardpoints[i].Size)
                    {
                        GameObject newTurret = Instantiate(prefabs[i], hardpoints[i].transform.position, hardpoints[i].transform.rotation);
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
