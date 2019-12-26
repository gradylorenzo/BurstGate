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
    #region References
    private ShipDynamics sd;
    private ShipWeapons sw;
    private Rigidbody rb;
    #endregion

    #region stats
    public StatSheet BaseStats;

    #region ModiferStack
    public List<CoreModifier> coreModifiers = new List<CoreModifier>();
    private List<BatteryModifier> batteryModifiers = new List<BatteryModifier>();
    private List<DefenseModifier> defenseModifiers = new List<DefenseModifier>();
    private List<OffenseModifier> offenseModifiers = new List<OffenseModifier>();
    public List<MobilityModifier> mobilityModifiers = new List<MobilityModifier>();
    #endregion

    public StatSheet CurrentStats;

    #endregion

    #region Common
    private void Start()
    {
        sd = GetComponent<ShipDynamics>();
        sw = GetComponent<ShipWeapons>();
        rb = GetComponent<Rigidbody>();

        CalculateAllBonuses();
    }

    

    #endregion

    #region CalculateBonues
    private void CalculateAllBonuses()
    {
        CalculateCoreBonuses();
        CalculateBatteryBonuses();
        CalculateDefenseBonuses();
        CalculateOffenseBonuses();
        CalculateMobilityBonus();
        ApplyStats();
    }

    private void CalculateCoreBonuses()
    {
        CoreStats allPercentBonuses = new CoreStats(1, Size.small, 1, 1);
        CoreStats allFlatBonuses = new CoreStats();
        
        foreach(CoreModifier m in coreModifiers)
        {
            if(m.type == ModifierType.Percentage)
            {
                allPercentBonuses += m.modifier;
            }
            else
            {
                allFlatBonuses += m.modifier;
            }
        }

        CurrentStats.Core = BaseStats.Core * allPercentBonuses;
        CurrentStats.Core += allFlatBonuses;
    }

    private void CalculateBatteryBonuses()
    {

    }

    private void CalculateDefenseBonuses()
    {

    }

    private void CalculateOffenseBonuses()
    {

    }

    private void CalculateMobilityBonus()
    {
        MobilityStats allPercentBonuses = new MobilityStats(new Vector3(1, 1, 1), 1, 1, 1, 1, 1);
        MobilityStats allFlatBonues = new MobilityStats();

        foreach(MobilityModifier m in mobilityModifiers)
        {
            if(m.type == ModifierType.Percentage)
            {
                allPercentBonuses += m.modifier;
            }
            else
            {
                allFlatBonues += m.modifier;
            }
        }

        CurrentStats.Mobility = BaseStats.Mobility * allPercentBonuses;
        CurrentStats.Mobility += allFlatBonues;
    }

    private void ApplyStats()
    {
        rb.mass = CurrentStats.Core.mass;

        sd.UpdateMobilityStats(CurrentStats.Mobility);
    }

    #endregion

    private void UpdateRigidbody()
    {
        rb.mass = CurrentStats.Core.mass;
    }

    private void UpdateShipDynamicsMobility()
    {

    }
}