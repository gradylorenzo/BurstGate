using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGCore;
using BGCore.Data;
using System;

[RequireComponent(typeof(ShipDynamics))]
[RequireComponent(typeof(ShipWeapons))]
public class ShipCore : MonoBehaviour
{
    public StatSheet BaseStats;

    private StatSheet CurrentStats;

    private ShipDynamics sd;
    private ShipWeapons sw;
    private Rigidbody rb;

    private void Start()
    {
        sd = GetComponent<ShipDynamics>();
        sw = GetComponent<ShipWeapons>();
        rb = GetComponent<Rigidbody>();

        CurrentStats = BaseStats;

        UpdateRigidbody();
        UpdateShipDynamicsMobility();
    }

    private void UpdateRigidbody()
    {
        rb.mass = CurrentStats.Core.mass;
    }

    private void UpdateShipDynamicsMobility()
    {
        sd.UpdateMobilityStats(CurrentStats.Mobility);
    }
}
