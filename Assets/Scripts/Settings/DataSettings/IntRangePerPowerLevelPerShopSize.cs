using System;
using UnityEngine;

[CreateAssetMenu]
public class IntRangePerPowerLevelPerShopSize : Saveable<IntRangePerPowerLevelPerShopSize>
{
    [SerializeField]
    protected EnumSetting m_ShopSizeEnum;
    [SerializeField]
    protected IntRangePerPowerLevel[] m_ShopSizeStratRanges;

    public IntRangePerPowerLevel this [string shopSize]
    {
        get
        {
            for(int i = 0; i < m_ShopSizeEnum.Length; i++)
            {
                if (m_ShopSizeEnum[i] == shopSize)
                    return m_ShopSizeStratRanges[i];
            }
            throw new ArgumentOutOfRangeException(nameof(shopSize), shopSize, "Unknown Shop Size.");
        }
    }

    public static IntRangePerPowerLevelPerShopSize Create (string name, EnumSetting shopSizeEnum)
    {
        IntRangePerPowerLevelPerShopSize newPerSizeAvailability = CreateInstance<IntRangePerPowerLevelPerShopSize>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerSizeAvailability.name = name;
        newPerSizeAvailability.m_ShopSizeEnum = shopSizeEnum;
        newPerSizeAvailability.m_ShopSizeStratRanges = new IntRangePerPowerLevel[shopSizeEnum.Length];

        SaveableHolder.AddSaveable(newPerSizeAvailability);

        return newPerSizeAvailability;
    }
    
    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_ShopSizeEnum.name + jsonSplitter[0];
        for(int i = 0; i < m_ShopSizeStratRanges.Length; i++)
        {
            jsonString += m_ShopSizeStratRanges[i].name + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_ShopSizeEnum = EnumSetting.Load(splitJsonString[1]);
        m_ShopSizeStratRanges = new IntRangePerPowerLevel[splitJsonString.Length - 2];
        for(int i = 0; i < m_ShopSizeStratRanges.Length; i++)
        {
            m_ShopSizeStratRanges[i] = IntRangePerPowerLevel.Load(splitJsonString[i + 2]);
        }
    }
}
