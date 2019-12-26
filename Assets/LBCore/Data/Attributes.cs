using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGCore;
using BGCore.Data;

namespace BGCore.Data
{
    [Serializable]
    public struct StatSheet
    {
        public CoreStats Core;
        public BatteryStats Battery;
        public DefenseStats Defense;
        public OffenseStats Attack;
        public MobilityStats Mobility;
    }

    [Serializable]
    public struct CoreStats
    {
        #region Members
        public float mass;
        public Size size;
        public float signatureRadius;
        public float sensorResolution;
        #endregion

        #region Properties
        public float LockingSpeed(float otherSigRadius, float thisSensorResolution)
        {
            //This should eventually contain an equation for calculating locking speeds, given the
            //target's signature radius relative to the user's sensor resolution
            return 1;
        }
        #endregion

        #region Constructors
        public CoreStats(float m, Size s, float rad, float res)
        {
            mass = m;
            size = s;
            signatureRadius = rad;
            sensorResolution = res;
        }
        #endregion

        #region Operators
        public static CoreStats operator +(CoreStats c1, CoreStats c2)
        {
            CoreStats output;
            output.mass = c1.mass + c2.mass;
            output.size = c1.size;
            output.signatureRadius = c1.signatureRadius + c2.signatureRadius;
            output.sensorResolution = c1.sensorResolution + c2.sensorResolution;

            return output;
        }
        public static CoreStats operator -(CoreStats c1, CoreStats c2)
        {
            CoreStats output;
            output.mass = c1.mass - c2.mass;
            output.size = c1.size;
            output.signatureRadius = c1.signatureRadius - c2.signatureRadius;
            output.sensorResolution = c1.sensorResolution - c2.sensorResolution;

            return output;
        }
        public static CoreStats operator *(CoreStats c1, CoreStats c2)
        {
            CoreStats output;
            output.mass = c1.mass * c2.mass;
            output.size = c1.size;
            output.signatureRadius = c1.signatureRadius * c2.signatureRadius;
            output.sensorResolution = c1.sensorResolution * c2.sensorResolution;

            return output;
        }
        public static CoreStats operator /(CoreStats c1, CoreStats c2)
        {
            CoreStats output;
            output.mass = c1.mass / c2.mass;
            output.size = c1.size;
            output.signatureRadius = c1.signatureRadius / c2.signatureRadius;
            output.sensorResolution = c1.sensorResolution / c2.sensorResolution;

            return output;
        }
        #endregion
    }

    [Serializable]
    public struct BatteryStats
    {
        #region Members
        public float MaxCapacity;
        public float CurrentCapacity;
        public float RechargeRate;
        #endregion

        #region Constructors
        public BatteryStats(float m, float c, float r)
        {
            MaxCapacity = m;
            CurrentCapacity = c;
            RechargeRate = r;
        }
        #endregion

        #region Operators
        public static BatteryStats operator +(BatteryStats b1, BatteryStats b2)
        {
            return new BatteryStats(b1.MaxCapacity + b2.MaxCapacity, b1.CurrentCapacity + b2.CurrentCapacity, b1.RechargeRate + b2.RechargeRate);
        }
        public static BatteryStats operator -(BatteryStats b1, BatteryStats b2)
        {
            return new BatteryStats(b1.MaxCapacity - b2.MaxCapacity, b1.CurrentCapacity - b2.CurrentCapacity, b1.RechargeRate - b2.RechargeRate);
        }
        public static BatteryStats operator *(BatteryStats b1, BatteryStats b2)
        {
            return new BatteryStats(b1.MaxCapacity * b2.MaxCapacity, b1.CurrentCapacity * b2.CurrentCapacity, b1.RechargeRate * b2.RechargeRate);
        }
        public static BatteryStats operator /(BatteryStats b1, BatteryStats b2)
        {
            return new BatteryStats(b1.MaxCapacity / b2.MaxCapacity, b1.CurrentCapacity / b2.CurrentCapacity, b1.RechargeRate / b2.RechargeRate);
        }
        #endregion
    }

    [Serializable]
    public struct DefenseStats
    {
        #region Members
        public float shieldMaxHitpoints;
        public float shieldCurrentHitpoints;

        public float shieldRechargeRate;

        public float hullMaxHitpoints;
        public float hullCurrentHitpoints;

        public ResistanceProfile shieldResistance;
        public ResistanceProfile hullResistance;
        #endregion

        #region Constructors
        public DefenseStats(float sM, float sC, float sR, float hM, float hC, ResistanceProfile sRes, ResistanceProfile hRes)
        {
            shieldMaxHitpoints = sM;
            shieldCurrentHitpoints = sC;
            shieldRechargeRate = sR;
            hullMaxHitpoints = hM;
            hullCurrentHitpoints = hC;
            shieldResistance = sRes;
            hullResistance = hRes;
        }
        #endregion

        #region Operators
        public static DefenseStats operator +(DefenseStats d1, DefenseStats d2)
        {
            DefenseStats output;
            output.shieldMaxHitpoints = d1.shieldMaxHitpoints + d2.shieldMaxHitpoints;
            output.shieldCurrentHitpoints = d1.shieldCurrentHitpoints + d2.shieldCurrentHitpoints;
            output.shieldRechargeRate = d1.shieldRechargeRate + d2.shieldRechargeRate;
            output.hullMaxHitpoints = d1.hullMaxHitpoints + d2.hullMaxHitpoints;
            output.hullCurrentHitpoints = d1.hullCurrentHitpoints + d2.hullCurrentHitpoints;

            output.shieldResistance = d1.shieldResistance + d2.shieldResistance;
            output.hullResistance = d1.hullResistance + d2.hullResistance;

            return output;
        }
        public static DefenseStats operator -(DefenseStats d1, DefenseStats d2)
        {
            DefenseStats output;
            output.shieldMaxHitpoints = d1.shieldMaxHitpoints - d2.shieldMaxHitpoints;
            output.shieldCurrentHitpoints = d1.shieldCurrentHitpoints - d2.shieldCurrentHitpoints;
            output.shieldRechargeRate = d1.shieldRechargeRate - d2.shieldRechargeRate;
            output.hullMaxHitpoints = d1.hullMaxHitpoints - d2.hullMaxHitpoints;
            output.hullCurrentHitpoints = d1.hullCurrentHitpoints - d2.hullCurrentHitpoints;

            output.shieldResistance = d1.shieldResistance - d2.shieldResistance;
            output.hullResistance = d1.hullResistance - d2.hullResistance;

            return output;
        }
        public static DefenseStats operator *(DefenseStats d1, DefenseStats d2)
        {
            DefenseStats output;
            output.shieldMaxHitpoints = d1.shieldMaxHitpoints * d2.shieldMaxHitpoints;
            output.shieldCurrentHitpoints = d1.shieldCurrentHitpoints * d2.shieldCurrentHitpoints;
            output.shieldRechargeRate = d1.shieldRechargeRate * d2.shieldRechargeRate;
            output.hullMaxHitpoints = d1.hullMaxHitpoints * d2.hullMaxHitpoints;
            output.hullCurrentHitpoints = d1.hullCurrentHitpoints * d2.hullCurrentHitpoints;

            output.shieldResistance = d1.shieldResistance * d2.shieldResistance;
            output.hullResistance = d1.hullResistance * d2.hullResistance;

            return output;
        }
        public static DefenseStats operator /(DefenseStats d1, DefenseStats d2)
        {
            DefenseStats output;
            output.shieldMaxHitpoints = d1.shieldMaxHitpoints / d2.shieldMaxHitpoints;
            output.shieldCurrentHitpoints = d1.shieldCurrentHitpoints / d2.shieldCurrentHitpoints;
            output.shieldRechargeRate = d1.shieldRechargeRate / d2.shieldRechargeRate;
            output.hullMaxHitpoints = d1.hullMaxHitpoints / d2.hullMaxHitpoints;
            output.hullCurrentHitpoints = d1.hullCurrentHitpoints / d2.hullCurrentHitpoints;

            output.shieldResistance = d1.shieldResistance / d2.shieldResistance;
            output.hullResistance = d1.hullResistance / d2.hullResistance;

            return output;
        }
        #endregion
    }

    [Serializable]
    public struct OffenseStats
    {
        #region Members
        public AttackBonus LaserBonus;
        public AttackBonus RailgunBonus;
        public AttackBonus MissileBonus;
        public AttackBonus RepairerBonus;
        #endregion

        #region Constructors
        public OffenseStats(AttackBonus l, AttackBonus ra, AttackBonus m, AttackBonus re)
        {
            LaserBonus = l;
            RailgunBonus = ra;
            MissileBonus = m;
            RepairerBonus = re;
        }
        #endregion

        #region Operators
        public static OffenseStats operator +(OffenseStats a1, OffenseStats a2)
        {
            OffenseStats output = new OffenseStats();
            output.LaserBonus = a1.LaserBonus + a2.LaserBonus;
            output.RailgunBonus = a1.RailgunBonus + a2.RailgunBonus;
            output.MissileBonus = a1.MissileBonus + a2.MissileBonus;
            output.RepairerBonus = a1.RepairerBonus + a2.RepairerBonus;

            return output;
        }
        public static OffenseStats operator -(OffenseStats a1, OffenseStats a2)
        {
            OffenseStats output = new OffenseStats();
            output.LaserBonus = a1.LaserBonus - a2.LaserBonus;
            output.RailgunBonus = a1.RailgunBonus - a2.RailgunBonus;
            output.MissileBonus = a1.MissileBonus - a2.MissileBonus;
            output.RepairerBonus = a1.RepairerBonus - a2.RepairerBonus;

            return output;
        }
        public static OffenseStats operator *(OffenseStats a1, OffenseStats a2)
        {
            OffenseStats output = new OffenseStats();
            output.LaserBonus = a1.LaserBonus * a2.LaserBonus;
            output.RailgunBonus = a1.RailgunBonus * a2.RailgunBonus;
            output.MissileBonus = a1.MissileBonus * a2.MissileBonus;
            output.RepairerBonus = a1.RepairerBonus * a2.RepairerBonus;

            return output;
        }
        public static OffenseStats operator /(OffenseStats a1, OffenseStats a2)
        {
            OffenseStats output = new OffenseStats();
            output.LaserBonus = a1.LaserBonus / a2.LaserBonus;
            output.RailgunBonus = a1.RailgunBonus / a2.RailgunBonus;
            output.MissileBonus = a1.MissileBonus / a2.MissileBonus;
            output.RepairerBonus = a1.RepairerBonus / a2.RepairerBonus;

            return output;
        }
        #endregion
    }

    [Serializable]
    public struct ResistanceProfile
    {
        #region Members
        public float EM;
        public float Kinetic;
        public float Thermal;
        #endregion

        #region Constructors
        public ResistanceProfile(float em, float kin, float therm)
        {
            EM = em;
            Kinetic = kin;
            Thermal = therm;
        }
        #endregion

        #region Operators
        public static ResistanceProfile operator +(ResistanceProfile r1, ResistanceProfile r2)
        {
            ResistanceProfile output;
            output.EM = r1.EM + r2.EM;
            output.Kinetic = r1.Kinetic + r2.Kinetic;
            output.Thermal = r1.Thermal + r2.Thermal;

            return output;
        }
        public static ResistanceProfile operator -(ResistanceProfile r1, ResistanceProfile r2)
        {
            ResistanceProfile output;
            output.EM = r1.EM - r2.EM;
            output.Kinetic = r1.Kinetic - r2.Kinetic;
            output.Thermal = r1.Thermal - r2.Thermal;

            return output;
        }
        public static ResistanceProfile operator *(ResistanceProfile r1, ResistanceProfile r2)
        {
            ResistanceProfile output;
            output.EM = r1.EM * r2.EM;
            output.Kinetic = r1.Kinetic * r2.Kinetic;
            output.Thermal = r1.Thermal * r2.Thermal;

            return output;
        }
        public static ResistanceProfile operator /(ResistanceProfile r1, ResistanceProfile r2)
        {
            ResistanceProfile output;
            output.EM = r1.EM / r2.EM;
            output.Kinetic = r1.Kinetic / r2.Kinetic;
            output.Thermal = r1.Thermal / r2.Thermal;

            return output;
        }
        public static bool operator ==(ResistanceProfile r1, ResistanceProfile r2)
        {
            return r1.EM == r2.EM && r1.Kinetic == r2.Kinetic && r1.Thermal == r2.Thermal;
        }
        public static bool operator !=(ResistanceProfile r1, ResistanceProfile r2)
        {
            return r1.EM != r2.EM || r1.Kinetic != r2.Kinetic || r1.Thermal != r2.Thermal;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.EM == ((ResistanceProfile)obj).EM && this.Kinetic == ((ResistanceProfile)obj).Kinetic && this.Thermal == ((ResistanceProfile)obj).Thermal);
        }
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
        public string ToString(string format)
        {
            return EM.ToString(format) + "  |  " + Kinetic.ToString(format) + "  |  " + Thermal.ToString(format);
        }
        #endregion
    }

    [Serializable]
    public struct AttackBonus
    {
        #region Members
        public float Damage;
        public float Range;
        #endregion

        #region Constructors
        public AttackBonus(float d, float r)
        {
            Damage = d;
            Range = r;
        }
        #endregion

        #region Operators
        public static AttackBonus operator +(AttackBonus a1, AttackBonus a2)
        {
            return new AttackBonus(a1.Damage + a2.Damage, a1.Range + a2.Range);
        }
        public static AttackBonus operator -(AttackBonus a1, AttackBonus a2)
        {
            return new AttackBonus(a1.Damage - a2.Damage, a1.Range - a2.Range);
        }
        public static AttackBonus operator *(AttackBonus a1, AttackBonus a2)
        {
            return new AttackBonus(a1.Damage * a2.Damage, a1.Range * a2.Range);
        }
        public static AttackBonus operator /(AttackBonus a1, AttackBonus a2)
        {
            return new AttackBonus(a1.Damage / a2.Damage, a1.Range / a2.Range);
        }
        #endregion
    }

    [Serializable]
    public struct MobilityStats
    {
        #region Members
        public Vector3 Thrust;
        public float Torque;
        public float InertialDampenerMultiplier;

        public float JumpRange;
        public float JumpSpoolTime;
        public const float JumpReq = 100.0f;
        public float JumpChargeRate;
        #endregion

        #region Constructors
        public MobilityStats(Vector3 th, float tq, float i, float r, float s, float c)
        {
            Thrust = th;
            Torque = tq;
            InertialDampenerMultiplier = i;
            JumpRange = r;
            JumpSpoolTime = s;
            JumpChargeRate = c;
        }
        #endregion

        #region Operators
        public static MobilityStats operator +(MobilityStats m1, MobilityStats m2)
        {
            MobilityStats output = new MobilityStats();

            output.Thrust = m1.Thrust + m2.Thrust;
            output.Torque = m1.Torque + m2.Torque;
            output.InertialDampenerMultiplier = m1.InertialDampenerMultiplier + m2.InertialDampenerMultiplier;
            output.JumpRange = m1.JumpRange + m2.JumpRange;
            output.JumpSpoolTime = m1.JumpSpoolTime + m2.JumpSpoolTime;
            output.JumpChargeRate = m1.JumpChargeRate + m2.JumpChargeRate;

            return output;
        }
        public static MobilityStats operator -(MobilityStats m1, MobilityStats m2)
        {
            MobilityStats output = new MobilityStats();

            output.Thrust = m1.Thrust - m2.Thrust;
            output.Torque = m1.Torque - m2.Torque;
            output.InertialDampenerMultiplier = m1.InertialDampenerMultiplier - m2.InertialDampenerMultiplier;
            output.JumpRange = m1.JumpRange - m2.JumpRange;
            output.JumpSpoolTime = m1.JumpSpoolTime - m2.JumpSpoolTime;
            output.JumpChargeRate = m1.JumpChargeRate - m2.JumpChargeRate;

            return output;
        }
        public static MobilityStats operator *(MobilityStats m1, MobilityStats m2)
        {
            MobilityStats output = new MobilityStats();

            output.Thrust = Vector3.Scale(m1.Thrust, m2.Thrust);
            output.Torque = m1.Torque * m2.Torque;
            output.InertialDampenerMultiplier = m1.InertialDampenerMultiplier * m2.InertialDampenerMultiplier;
            output.JumpRange = m1.JumpRange * m2.JumpRange;
            output.JumpSpoolTime = m1.JumpSpoolTime * m2.JumpSpoolTime;
            output.JumpChargeRate = m1.JumpChargeRate * m2.JumpChargeRate;

            return output;
        }
        #endregion
    }

    #region Modifiers
    [Serializable]
    public enum ModifierType
    {
        Flat,
        Percentage
    }

    [Serializable]
    public struct CoreModifier
    {
        public ModifierType type;
        public CoreStats modifier;
    }

    [Serializable]
    public struct BatteryModifier
    {
        public ModifierType type;
        public BatteryStats modifier;
    }

    [Serializable]
    public struct DefenseModifier
    {
        public ModifierType type;
        public DefenseStats modifier;
    }

    [Serializable]
    public struct OffenseModifier
    {
        public ModifierType type;
        public OffenseStats modifier;
    }

    [Serializable]
    public struct MobilityModifier
    {
        public ModifierType type;
        public MobilityStats modifier;
    }
    #endregion
}