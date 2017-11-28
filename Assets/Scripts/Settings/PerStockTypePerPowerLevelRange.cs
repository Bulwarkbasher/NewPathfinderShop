using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PerStockTypePerPowerLevelRange : Saveable<PerStockTypePerPowerLevelRange>
{
    [SerializeField]
    protected PerPowerLevelRange m_ArmourPerPowerLevelRange;
    [SerializeField]
    protected PerPowerLevelRange m_PotionPerPowerLevelRange;
    [SerializeField]
    protected PerPowerLevelRange m_RingPerPowerLevelRange;
    [SerializeField]
    protected PerPowerLevelRange m_RodPerPowerLevelRange;
    [SerializeField]
    protected PerPowerLevelRange m_ScrollPerPowerLevelRange;
    [SerializeField]
    protected PerPowerLevelRange m_StaffPerPowerLevelRange;
    [SerializeField]
    protected PerPowerLevelRange m_WandPerPowerLevelRange;
    [SerializeField]
    protected PerPowerLevelRange m_WeaponPerPowerLevelRange;
    [SerializeField]
    protected PerPowerLevelRange m_WondrousPerPowerLevelRange;

    public PerPowerLevelRange this [Shop.StockType type]
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

    public static PerStockTypePerPowerLevelRange Create(string name, PerPowerLevelRange armour, PerPowerLevelRange potion, PerPowerLevelRange ring, PerPowerLevelRange rod, PerPowerLevelRange scroll, PerPowerLevelRange staff, PerPowerLevelRange wand, PerPowerLevelRange weapon, PerPowerLevelRange wondrous)
    {
        PerStockTypePerPowerLevelRange newPerStockTypePerPowerLevelRange = CreateInstance<PerStockTypePerPowerLevelRange>();

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
        m_ArmourPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[1]);
        m_PotionPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[2]);
        m_RingPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[3]);
        m_RodPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[4]);
        m_ScrollPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[5]);
        m_StaffPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[6]);
        m_WandPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[7]);
        m_WeaponPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[8]);
        m_WondrousPerPowerLevelRange = PerPowerLevelRange.Load(splitJsonString[9]);
    }
}
