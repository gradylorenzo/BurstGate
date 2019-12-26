using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BGCore.Data
{
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
    public enum Size
    {
        small = 1,
        medium = 2,
        large = 3,
        xlare = 4
    }
}