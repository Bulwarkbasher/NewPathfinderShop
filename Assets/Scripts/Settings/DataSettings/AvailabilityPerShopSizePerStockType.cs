using System;
using UnityEngine;

[CreateAssetMenu]
public class AvailabilityPerShopSizePerStockType : Saveable<AvailabilityPerShopSizePerStockType>
{
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_ArmourPerSizeAvailability;
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_PotionPerSizeAvailability;
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_RingPerSizeAvailability;
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_RodPerSizeAvailability;
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_ScrollPerSizeAvailability;
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_StaffPerSizeAvailability;
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_WandPerSizeAvailability;
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_WeaponPerSizeAvailability;
    [SerializeField]
    protected IntRangePerPowerLevelPerShopSize m_WondrousPerSizeAvailability;

    public IntRangePerPowerLevelPerShopSize this [Shop.StockType type]
    {
        get
        {
            switch (type)
            {
                case Shop.StockType.Armour:
                    return m_ArmourPerSizeAvailability;
                case Shop.StockType.Potion:
                    return m_PotionPerSizeAvailability;
                case Shop.StockType.Ring:
                    return m_RingPerSizeAvailability;
                case Shop.StockType.Rod:
                    return m_RodPerSizeAvailability;
                case Shop.StockType.Scroll:
                    return m_ScrollPerSizeAvailability;
                case Shop.StockType.Staff:
                    return m_StaffPerSizeAvailability;
                case Shop.StockType.Wand:
                    return m_WandPerSizeAvailability;
                case Shop.StockType.Weapon:
                    return m_WeaponPerSizeAvailability;
                case Shop.StockType.Wondrous:
                    return m_WondrousPerSizeAvailability;
                default:
                    throw new ArgumentOutOfRangeException ("type", type, "Unknown Shop Stock Type.");
            }
        }
    }

    public static AvailabilityPerShopSizePerStockType Create(string name, IntRangePerPowerLevelPerShopSize armour, IntRangePerPowerLevelPerShopSize potion, IntRangePerPowerLevelPerShopSize ring, IntRangePerPowerLevelPerShopSize rod, IntRangePerPowerLevelPerShopSize scroll, IntRangePerPowerLevelPerShopSize staff, IntRangePerPowerLevelPerShopSize wand, IntRangePerPowerLevelPerShopSize weapon, IntRangePerPowerLevelPerShopSize wondrous)
    {
        AvailabilityPerShopSizePerStockType newPerStockTypePerSizeAvailability = CreateInstance<AvailabilityPerShopSizePerStockType> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerStockTypePerSizeAvailability.name = name;
        newPerStockTypePerSizeAvailability.m_ArmourPerSizeAvailability = armour;
        newPerStockTypePerSizeAvailability.m_PotionPerSizeAvailability = potion;
        newPerStockTypePerSizeAvailability.m_RingPerSizeAvailability = ring;
        newPerStockTypePerSizeAvailability.m_RodPerSizeAvailability = rod;
        newPerStockTypePerSizeAvailability.m_ScrollPerSizeAvailability = scroll;
        newPerStockTypePerSizeAvailability.m_StaffPerSizeAvailability = staff;
        newPerStockTypePerSizeAvailability.m_WandPerSizeAvailability = wand;
        newPerStockTypePerSizeAvailability.m_WeaponPerSizeAvailability = weapon;
        newPerStockTypePerSizeAvailability.m_WondrousPerSizeAvailability = wondrous;

        SaveableHolder.AddSaveable(newPerStockTypePerSizeAvailability);

        return newPerStockTypePerSizeAvailability;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_ArmourPerSizeAvailability.name + jsonSplitter[0];
        jsonString += m_PotionPerSizeAvailability.name + jsonSplitter[0];
        jsonString += m_RingPerSizeAvailability.name + jsonSplitter[0];
        jsonString += m_RodPerSizeAvailability.name + jsonSplitter[0];
        jsonString += m_ScrollPerSizeAvailability.name + jsonSplitter[0];
        jsonString += m_StaffPerSizeAvailability.name + jsonSplitter[0];
        jsonString += m_WandPerSizeAvailability.name + jsonSplitter[0];
        jsonString += m_WeaponPerSizeAvailability.name + jsonSplitter[0];
        jsonString += m_WondrousPerSizeAvailability.name + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_ArmourPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[1]);
        m_PotionPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[2]);
        m_RingPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[3]);
        m_RodPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[4]);
        m_ScrollPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[5]);
        m_StaffPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[6]);
        m_WandPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[7]);
        m_WeaponPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[8]);
        m_WondrousPerSizeAvailability = IntRangePerPowerLevelPerShopSize.Load(splitJsonString[9]);
    }
}
