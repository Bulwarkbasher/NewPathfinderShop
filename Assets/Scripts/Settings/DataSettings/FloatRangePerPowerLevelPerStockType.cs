using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatRangePerPowerLevelPerStockType : Saveable<FloatRangePerPowerLevelPerStockType>
{
    [SerializeField]
    protected FloatRangePerPowerLevel m_ArmourPerPowerLevelRange;
    [SerializeField]
    protected FloatRangePerPowerLevel m_PotionPerPowerLevelRange;
    [SerializeField]
    protected FloatRangePerPowerLevel m_RingPerPowerLevelRange;
    [SerializeField]
    protected FloatRangePerPowerLevel m_RodPerPowerLevelRange;
    [SerializeField]
    protected FloatRangePerPowerLevel m_ScrollPerPowerLevelRange;
    [SerializeField]
    protected FloatRangePerPowerLevel m_StaffPerPowerLevelRange;
    [SerializeField]
    protected FloatRangePerPowerLevel m_WandPerPowerLevelRange;
    [SerializeField]
    protected FloatRangePerPowerLevel m_WeaponPerPowerLevelRange;
    [SerializeField]
    protected FloatRangePerPowerLevel m_WondrousPerPowerLevelRange;

    public FloatRangePerPowerLevel this [Shop.StockType type]
    {
        get
        {
            switch (type)
            {
                case Shop.StockType.Armour:
                    return m_ArmourPerPowerLevelRange;
                case Shop.StockType.Potion:
                    return m_PotionPerPowerLevelRange;
                case Shop.StockType.Ring:
                    return m_RingPerPowerLevelRange;
                case Shop.StockType.Rod:
                    return m_RodPerPowerLevelRange;
                case Shop.StockType.Scroll:
                    return m_ScrollPerPowerLevelRange;
                case Shop.StockType.Staff:
                    return m_StaffPerPowerLevelRange;
                case Shop.StockType.Wand:
                    return m_WandPerPowerLevelRange;
                case Shop.StockType.Weapon:
                    return m_WeaponPerPowerLevelRange;
                case Shop.StockType.Wondrous:
                    return m_WondrousPerPowerLevelRange;
                default:
                    throw new ArgumentOutOfRangeException("type", type, "Unknown Shop Stock Type.");
            }
        }
    }

    public static FloatRangePerPowerLevelPerStockType Create(string name, FloatRangePerPowerLevel armour, FloatRangePerPowerLevel potion, FloatRangePerPowerLevel ring, FloatRangePerPowerLevel rod, FloatRangePerPowerLevel scroll, FloatRangePerPowerLevel staff, FloatRangePerPowerLevel wand, FloatRangePerPowerLevel weapon, FloatRangePerPowerLevel wondrous)
    {
        FloatRangePerPowerLevelPerStockType newPerStockTypePerPowerLevelRange = CreateInstance<FloatRangePerPowerLevelPerStockType>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerStockTypePerPowerLevelRange.name = name;
        newPerStockTypePerPowerLevelRange.m_ArmourPerPowerLevelRange = armour;
        newPerStockTypePerPowerLevelRange.m_PotionPerPowerLevelRange = potion;
        newPerStockTypePerPowerLevelRange.m_RingPerPowerLevelRange = ring;
        newPerStockTypePerPowerLevelRange.m_RodPerPowerLevelRange = rod;
        newPerStockTypePerPowerLevelRange.m_ScrollPerPowerLevelRange = scroll;
        newPerStockTypePerPowerLevelRange.m_StaffPerPowerLevelRange = staff;
        newPerStockTypePerPowerLevelRange.m_WandPerPowerLevelRange = wand;
        newPerStockTypePerPowerLevelRange.m_WeaponPerPowerLevelRange = weapon;
        newPerStockTypePerPowerLevelRange.m_WondrousPerPowerLevelRange = wondrous;

        SaveableHolder.AddSaveable(newPerStockTypePerPowerLevelRange);

        return newPerStockTypePerPowerLevelRange;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_ArmourPerPowerLevelRange.name + jsonSplitter[0];
        jsonString += m_PotionPerPowerLevelRange.name + jsonSplitter[0];
        jsonString += m_RingPerPowerLevelRange.name + jsonSplitter[0];
        jsonString += m_RodPerPowerLevelRange.name + jsonSplitter[0];
        jsonString += m_ScrollPerPowerLevelRange.name + jsonSplitter[0];
        jsonString += m_StaffPerPowerLevelRange.name + jsonSplitter[0];
        jsonString += m_WandPerPowerLevelRange.name + jsonSplitter[0];
        jsonString += m_WeaponPerPowerLevelRange.name + jsonSplitter[0];
        jsonString += m_WondrousPerPowerLevelRange.name + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_ArmourPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[1]);
        m_PotionPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[2]);
        m_RingPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[3]);
        m_RodPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[4]);
        m_ScrollPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[5]);
        m_StaffPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[6]);
        m_WandPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[7]);
        m_WeaponPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[8]);
        m_WondrousPerPowerLevelRange = FloatRangePerPowerLevel.Load(splitJsonString[9]);
    }
}
