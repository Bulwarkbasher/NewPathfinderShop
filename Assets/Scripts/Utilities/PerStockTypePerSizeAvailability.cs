using System;
using UnityEngine;

[CreateAssetMenu]
public class PerStockTypePerSizeAvailability : Saveable<PerStockTypePerSizeAvailability>
{
    [SerializeField]
    protected PerSizeAvailability m_ArmourPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_PotionPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_RingPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_RodPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_ScrollPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_StaffPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_WandPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_WeaponPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_WondrousPerSizeAvailability;

    public PerSizeAvailability this [Shop.StockType type]
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

    public static PerStockTypePerSizeAvailability Create(string name, PerSizeAvailability armour, PerSizeAvailability potion, PerSizeAvailability ring, PerSizeAvailability rod, PerSizeAvailability scroll, PerSizeAvailability staff, PerSizeAvailability wand, PerSizeAvailability weapon, PerSizeAvailability wondrous)
    {
        PerStockTypePerSizeAvailability newPerStockTypePerSizeAvailability = CreateInstance<PerStockTypePerSizeAvailability> ();

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

        Save(newPerStockTypePerSizeAvailability);

        return newPerStockTypePerSizeAvailability;
    }

    protected override string GetJsonString(string[] jsonSplitter)
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

        PerSizeAvailability.Save(m_ArmourPerSizeAvailability);
        PerSizeAvailability.Save(m_PotionPerSizeAvailability);
        PerSizeAvailability.Save(m_RingPerSizeAvailability);
        PerSizeAvailability.Save(m_RodPerSizeAvailability);
        PerSizeAvailability.Save(m_ScrollPerSizeAvailability);
        PerSizeAvailability.Save(m_StaffPerSizeAvailability);
        PerSizeAvailability.Save(m_WandPerSizeAvailability);
        PerSizeAvailability.Save(m_WeaponPerSizeAvailability);
        PerSizeAvailability.Save(m_WondrousPerSizeAvailability);

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_ArmourPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[1]);
        m_PotionPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[2]);
        m_RingPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[3]);
        m_RodPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[4]);
        m_ScrollPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[5]);
        m_StaffPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[6]);
        m_WandPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[7]);
        m_WeaponPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[8]);
        m_WondrousPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[9]);
    }
}
