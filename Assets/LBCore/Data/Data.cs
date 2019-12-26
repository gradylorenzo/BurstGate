using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BGCore.Data
{
    [Serializable]
    public struct StatSheet
    {
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
            public float LockingSpeed (float otherSigRadius, float thisSensorResolution)
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
            public static CoreStats operator + (CoreStats c1, CoreStats c2)
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
            //Every ship has a battery, a method to
            //store power between being generated and
            //being consumed.
            public float MaxCapacity;
            public float CurrentCapacity;

            //Every ship has a method to generate that power.
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
            public static BatteryStats operator + (BatteryStats b1, BatteryStats b2)
            {
                return new BatteryStats(b1.MaxCapacity + b2.MaxCapacity, b1.CurrentCapacity + b2.CurrentCapacity, b1.RechargeRate + b2.RechargeRate);
            }
            public static BatteryStats operator - (BatteryStats b1, BatteryStats b2)
            {
                return new BatteryStats(b1.MaxCapacity - b2.MaxCapacity, b1.CurrentCapacity - b2.CurrentCapacity, b1.RechargeRate - b2.RechargeRate);
            }
            public static BatteryStats operator * (BatteryStats b1, BatteryStats b2)
            {
                return new BatteryStats(b1.MaxCapacity * b2.MaxCapacity, b1.CurrentCapacity * b2.CurrentCapacity, b1.RechargeRate * b2.RechargeRate);
            }
            public static BatteryStats operator / (BatteryStats b1, BatteryStats b2)
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
        public struct AttackStats
        {
            #region Members
            public AttackBonus LaserBonus;
            public AttackBonus RailgunBonus;
            public AttackBonus MissileBonus;
            public AttackBonus RepairerBonus;
            #endregion

            #region Constructors
            public AttackStats(AttackBonus l, AttackBonus ra, AttackBonus m, AttackBonus re)
            {
                LaserBonus = l;
                RailgunBonus = ra;
                MissileBonus = m;
                RepairerBonus = re;
            }
            #endregion

            #region Operators
            public static AttackStats operator + (AttackStats a1, AttackStats a2)
            {
                AttackStats output = new AttackStats();
                output.LaserBonus = a1.LaserBonus + a2.LaserBonus;
                output.RailgunBonus = a1.RailgunBonus + a2.RailgunBonus;
                output.MissileBonus = a1.MissileBonus + a2.MissileBonus;
                output.RepairerBonus = a1.RepairerBonus + a2.RepairerBonus;

                return output;
            }
            public static AttackStats operator -(AttackStats a1, AttackStats a2)
            {
                AttackStats output = new AttackStats();
                output.LaserBonus = a1.LaserBonus - a2.LaserBonus;
                output.RailgunBonus = a1.RailgunBonus - a2.RailgunBonus;
                output.MissileBonus = a1.MissileBonus - a2.MissileBonus;
                output.RepairerBonus = a1.RepairerBonus - a2.RepairerBonus;

                return output;
            }
            public static AttackStats operator *(AttackStats a1, AttackStats a2)
            {
                AttackStats output = new AttackStats();
                output.LaserBonus = a1.LaserBonus * a2.LaserBonus;
                output.RailgunBonus = a1.RailgunBonus * a2.RailgunBonus;
                output.MissileBonus = a1.MissileBonus * a2.MissileBonus;
                output.RepairerBonus = a1.RepairerBonus * a2.RepairerBonus;

                return output;
            }
            public static AttackStats operator /(AttackStats a1, AttackStats a2)
            {
                AttackStats output = new AttackStats();
                output.LaserBonus = a1.LaserBonus / a2.LaserBonus;
                output.RailgunBonus = a1.RailgunBonus / a2.RailgunBonus;
                output.MissileBonus = a1.MissileBonus / a2.MissileBonus;
                output.RepairerBonus = a1.RepairerBonus / a2.RepairerBonus;

                return output;
            }
            #endregion
        }
        
        public CoreStats Core;
        public BatteryStats Battery;
        public DefenseStats Defense;
        public AttackStats Attack;
        public MobilityStats Mobility;
    }

    public static class MyEnumExtensions
    {
        public static string ToDescriptionString(this ResourceType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    [System.Serializable]
    public enum ResourceType
    {
        //ORES
        [Description("Raw Iron")]
        raw_iron,
        [Description("Raw Carbon")]
        raw_carbon,
        [Description("Raw Magnesium")]
        raw_magnesium,
        [Description("Raw Copper")]
        raw_copper,
        [Description("Raw Silver")]
        raw_silver,
        [Description("Raw Gold")]
        raw_gold,
        [Description("Raw Uranium")]
        raw_uranium,

        //INGOTS
        [Description("Iron Ingot")]
        iron_ingot,
        [Description("Carbon Ingot")]
        carbon_ingot,
        [Description("Magnesium Ingot")]
        magnesium_ingot,
        [Description("Copper Ingot")]
        copper_ingot,
        [Description("Silver Ingot")]
        silver_ingot,
        [Description("Gold Ingot")]
        gold_ingot,
        [Description("Uranium Ingot")]
        uranium_ingot,

        //MINOR COMPONENTS
        [Description("Cooling Hose")]
        cooling_hose,
        [Description("Magnetic Coil")]
        magnetic_coil,

        //MAJOR COMPONENTS
        [Description("Reactor Unit")]
        reactor_unit
    }

    [System.Serializable]
    public struct Resource
    {
        public ResourceType resourceType;
        public int quantity;
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
        public ResistanceProfile (float em, float kin, float therm)
        {
            EM = em;
            Kinetic = kin;
            Thermal = therm;
        }
        #endregion

        #region Operators
        public static ResistanceProfile operator + (ResistanceProfile r1, ResistanceProfile r2)
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
        public AttackBonus (float d, float r)
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

        public int JumpRange;
        public float JumpSpoolTime;
        public const float JumpReq = 100.0f;
        public float JumpChargeRate;
        #endregion

        #region Constructors
        public MobilityStats(Vector3 th, float tq, float i, int r, float s, float c)
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

    [Serializable]
    public enum Size
    {
        small = 1,
        medium = 2,
        large = 3,
        xlare = 4
    }

    [Serializable]
    public struct DoubleVector2
    {
        #region Members
        public double x;
        public double y;
        private double _magnitude { get; set; }
        #endregion

        #region Properties
        public double magnitude
        {
            get { return _magnitude; }
            set { _magnitude = value; }
        }
        #endregion

        #region Constructors
        public DoubleVector2(double X, double Y)
        {
            x = X;
            y = Y;
            _magnitude = Math.Sqrt(x * x + y * y);
        }
        #endregion

        #region Static Properties
        public static DoubleVector2 zero
        {
            get { return new DoubleVector2(0, 0); }
        }

        public static DoubleVector2 forward
        {
            get { return new DoubleVector2(0, 1); }
        }

        public static DoubleVector2 right
        {
            get { return new DoubleVector2(1, 0); }
        }
        #endregion

        #region Static Methods

        //Lerp
        public static DoubleVector2 Lerp(DoubleVector2 a, DoubleVector2 b, float c)
        {
            double x = (a.x + c * (b.x - a.x));
            double y = (a.y + c * (b.y - a.y));

            return new DoubleVector2(x, y);
        }

        //MoveTowards
        public static DoubleVector2 MoveTowards(DoubleVector2 a, DoubleVector2 b, double c)
        {
            DoubleVector2 newPos = b - a;
            double magnitude = newPos.magnitude;
            if (magnitude <= c || magnitude == 0f)
                return b;
            return a + newPos / magnitude * c;
        }

        //FromSingleV3
        public static DoubleVector2 FromVector2(Vector2 v)
        {
            return new DoubleVector2(v.x, v.y);
        }

        //Distance
        public static double Distance(DoubleVector2 a, DoubleVector2 b)
        {
            return Math.Sqrt(((b.x - a.x) * (b.x - a.x)) + ((b.y - a.y) * (b.y - a.y)));
        }

        //ToSingleV3
        public static Vector2 ToVector2(DoubleVector2 v)
        {
            float x = Convert.ToSingle(v.x);
            float y = Convert.ToSingle(v.y);

            return new Vector2(x, y);
        }
        #endregion

        #region Operators
        public static DoubleVector2 operator +(DoubleVector2 a, DoubleVector2 b)
        {
            DoubleVector2 v = new DoubleVector2(a.x + b.x, a.y + b.y);
            return v;
        }
        public static DoubleVector2 operator -(DoubleVector2 a, DoubleVector2 b)
        {
            DoubleVector2 v = new DoubleVector2(a.x - b.x, a.y - b.y);
            return v;
        }
        public static DoubleVector2 operator *(DoubleVector2 a, DoubleVector2 b)
        {
            DoubleVector2 v = new DoubleVector2(a.x * b.x, a.y * b.y);
            return v;
        }
        public static DoubleVector2 operator *(DoubleVector2 a, float b)
        {
            DoubleVector2 v = new DoubleVector2(a.x * b, a.y * b);
            return v;
        }

        public static DoubleVector2 operator *(DoubleVector2 a, double b)
        {
            DoubleVector2 v = new DoubleVector2(a.x * b, a.y * b);
            return v;
        }
        public static DoubleVector2 operator /(DoubleVector2 a, DoubleVector2 b)
        {
            DoubleVector2 v = new DoubleVector2(a.x / b.x, a.y / b.y);
            return v;
        }
        public static DoubleVector2 operator /(DoubleVector2 a, double b)
        {
            DoubleVector2 v = new DoubleVector2(a.x / b, a.y / b);
            return v;
        }
        public static bool operator ==(DoubleVector2 a, DoubleVector2 b)
        {
            if (a.x == b.x && a.y == b.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(DoubleVector2 a, DoubleVector2 b)
        {
            if (a.x != b.x || a.y != b.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.x == ((DoubleVector2)obj).x && this.y == ((DoubleVector2)obj).y);
        }
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public string ToString(string format)
        {
            string s = "x = " + x.ToString(format) + "\ny = " + y.ToString(format);
            return s;
        }
        #endregion
    }
}