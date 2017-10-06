using System;
using UnityEngine;

[CreateAssetMenu]
public class PerStockTypePerSizeAvailability : ScriptableObject, ISaveable
{
    public string GetFolderPath() { return Application.persistentDataPath + "/PerStockTypePerSizeAvailability"; }
    protected readonly static string[] k_JsonSplitter = { "###PerStockTypePerSizeAvailabilitySplitter###", };

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

        if (newPerStockTypePerSizeAvailability.CheckName(name) == SaveableExtensions.NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (newPerStockTypePerSizeAvailability.CheckName(name) == SaveableExtensions.NameCheckResult.IsDefault)
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

        newPerStockTypePerSizeAvailability.Save ();

        return newPerStockTypePerSizeAvailability;
    }

    public void Save ()
    {
        string jsonString = "";

        jsonString += name + k_JsonSplitter[0];
        jsonString += m_ArmourPerSizeAvailability.name + k_JsonSplitter[0];
        jsonString += m_PotionPerSizeAvailability.name + k_JsonSplitter[0];
        jsonString += m_RingPerSizeAvailability.name + k_JsonSplitter[0];
        jsonString += m_RodPerSizeAvailability.name + k_JsonSplitter[0];
        jsonString += m_ScrollPerSizeAvailability.name + k_JsonSplitter[0];
        jsonString += m_StaffPerSizeAvailability.name + k_JsonSplitter[0];
        jsonString += m_WandPerSizeAvailability.name + k_JsonSplitter[0];
        jsonString += m_WeaponPerSizeAvailability.name + k_JsonSplitter[0];
        jsonString += m_WondrousPerSizeAvailability.name + k_JsonSplitter[0];

        this.WriteJsonStringToFile (name, jsonString);

        m_ArmourPerSizeAvailability.Save ();
        m_PotionPerSizeAvailability.Save ();
        m_RingPerSizeAvailability.Save ();
        m_RodPerSizeAvailability.Save ();
        m_ScrollPerSizeAvailability.Save ();
        m_StaffPerSizeAvailability.Save ();
        m_WandPerSizeAvailability.Save ();
        m_WeaponPerSizeAvailability.Save ();
        m_WondrousPerSizeAvailability.Save();
    }

    public static PerStockTypePerSizeAvailability Load (string name)
    {
        PerStockTypePerSizeAvailability perStockTypePerSizeAvailability = CreateInstance<PerStockTypePerSizeAvailability> ();

        string[] splitJsonString = perStockTypePerSizeAvailability.GetSplitJsonStringsFromFile (name, k_JsonSplitter);

        perStockTypePerSizeAvailability.name = splitJsonString[0];
        perStockTypePerSizeAvailability.m_ArmourPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[1]);
        perStockTypePerSizeAvailability.m_PotionPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[2]);
        perStockTypePerSizeAvailability.m_RingPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[3]);
        perStockTypePerSizeAvailability.m_RodPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[4]);
        perStockTypePerSizeAvailability.m_ScrollPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[5]);
        perStockTypePerSizeAvailability.m_StaffPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[6]);
        perStockTypePerSizeAvailability.m_WandPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[7]);
        perStockTypePerSizeAvailability.m_WeaponPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[8]);
        perStockTypePerSizeAvailability.m_WondrousPerSizeAvailability = PerSizeAvailability.Load(splitJsonString[9]);

        return perStockTypePerSizeAvailability;
    }
}
