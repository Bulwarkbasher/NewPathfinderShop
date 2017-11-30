using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntRangePerPowerLevelPerStockType : Saveable<IntRangePerPowerLevelPerStockType>
{
    [SerializeField]
    protected IntRangePerPowerLevel m_ArmourAvailabilityPerPowerLevel;
    [SerializeField]
    protected IntRangePerPowerLevel m_PotionAvailabilityPerPowerLevel;
    [SerializeField]
    protected IntRangePerPowerLevel m_RingAvailabilityPerPowerLevel;
    [SerializeField]
    protected IntRangePerPowerLevel m_RodAvailabilityPerPowerLevel;
    [SerializeField]
    protected IntRangePerPowerLevel m_ScrollAvailabilityPerPowerLevel;
    [SerializeField]
    protected IntRangePerPowerLevel m_StaffAvailabilityPerPowerLevel;
    [SerializeField]
    protected IntRangePerPowerLevel m_WandAvailabilityPerPowerLevel;
    [SerializeField]
    protected IntRangePerPowerLevel m_WeaponAvailabilityPerPowerLevel;
    [SerializeField]
    protected IntRangePerPowerLevel m_WondrousAvailabilityPerPowerLevel;

    public IntRangePerPowerLevel this [Shop.StockType stockType]
    {
        get
        {
            switch (stockType)
            {
                case Shop.StockType.Armour:
                    return m_ArmourAvailabilityPerPowerLevel;
                case Shop.StockType.Potion:
                    return m_PotionAvailabilityPerPowerLevel;
                case Shop.StockType.Ring:
                    return m_RingAvailabilityPerPowerLevel;
                case Shop.StockType.Rod:
                    return m_RodAvailabilityPerPowerLevel;
                case Shop.StockType.Scroll:
                    return m_ScrollAvailabilityPerPowerLevel;
                case Shop.StockType.Staff:
                    return m_StaffAvailabilityPerPowerLevel;
                case Shop.StockType.Wand:
                    return m_WandAvailabilityPerPowerLevel;
                case Shop.StockType.Weapon:
                    return m_WeaponAvailabilityPerPowerLevel;
                case Shop.StockType.Wondrous:
                    return m_WondrousAvailabilityPerPowerLevel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stockType), stockType, "Unknown Shop Stock Type.");
            }
        }
    }

    public static IntRangePerPowerLevelPerStockType Create (string name, string shopSize)
    {
        IntRangePerPowerLevelPerStockType newAvailabilityPerPowerLevelPerStockType = CreateInstance<IntRangePerPowerLevelPerStockType>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newAvailabilityPerPowerLevelPerStockType.name = name;
        newAvailabilityPerPowerLevelPerStockType.m_ArmourAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Armour];
        newAvailabilityPerPowerLevelPerStockType.m_PotionAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Potion];
        newAvailabilityPerPowerLevelPerStockType.m_RingAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Ring];
        newAvailabilityPerPowerLevelPerStockType.m_RodAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Rod];
        newAvailabilityPerPowerLevelPerStockType.m_ScrollAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Scroll];
        newAvailabilityPerPowerLevelPerStockType.m_StaffAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Staff];
        newAvailabilityPerPowerLevelPerStockType.m_WandAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Wand];
        newAvailabilityPerPowerLevelPerStockType.m_WeaponAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Weapon];
        newAvailabilityPerPowerLevelPerStockType.m_WondrousAvailabilityPerPowerLevel = Campaign.AvailabilityPerStockTypePerShopSize[shopSize][Shop.StockType.Wondrous];

        SaveableHolder.AddSaveable(newAvailabilityPerPowerLevelPerStockType);

        return newAvailabilityPerPowerLevelPerStockType;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_ArmourAvailabilityPerPowerLevel.name + jsonSplitter[0];
        jsonString += m_PotionAvailabilityPerPowerLevel.name + jsonSplitter[0];
        jsonString += m_RingAvailabilityPerPowerLevel.name + jsonSplitter[0];
        jsonString += m_RodAvailabilityPerPowerLevel.name + jsonSplitter[0];
        jsonString += m_ScrollAvailabilityPerPowerLevel.name + jsonSplitter[0];
        jsonString += m_StaffAvailabilityPerPowerLevel.name + jsonSplitter[0];
        jsonString += m_WandAvailabilityPerPowerLevel.name + jsonSplitter[0];
        jsonString += m_WeaponAvailabilityPerPowerLevel.name + jsonSplitter[0];
        jsonString += m_WondrousAvailabilityPerPowerLevel.name + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_ArmourAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[1]);
        m_PotionAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[2]);
        m_RingAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[3]);
        m_RodAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[4]);
        m_ScrollAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[5]);
        m_StaffAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[6]);
        m_WandAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[7]);
        m_WeaponAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[8]);
        m_WondrousAvailabilityPerPowerLevel = IntRangePerPowerLevel.Load(splitJsonString[9]);
    }
}
