using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BGCore
{
    public static class MyEnumExtensions
    {
        public static string ToDescriptionString(this DataTypes.ResourceType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    public static class DataTypes
    {
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

        public enum HardpointSize
        {
            small,
            medium,
            large,
            xlarge
        }
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