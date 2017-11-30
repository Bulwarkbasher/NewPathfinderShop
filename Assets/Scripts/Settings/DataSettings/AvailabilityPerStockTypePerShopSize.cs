using System;
using UnityEngine;

[CreateAssetMenu]
public class AvailabilityPerStockTypePerShopSize : Saveable<AvailabilityPerStockTypePerShopSize>
{
    [SerializeField]
    protected EnumSetting m_ShopSizeEnum;
    [SerializeField]
    protected IntRangePerPowerLevelPerStockType[] m_ShopSizeAvailibilityPerPowerLevels;

    public IntRangePerPowerLevelPerStockType this[string shopSize]
    {
        get
        {
            for (int i = 0; i < m_ShopSizeEnum.Length; i++)
            {
                if (m_ShopSizeEnum[i] == shopSize)
                    return m_ShopSizeAvailibilityPerPowerLevels[i];
            }
            throw new ArgumentOutOfRangeException(nameof(shopSize), shopSize, "Unknown Shop Size.");
        }
    }

    public static AvailabilityPerStockTypePerShopSize Create(string name)
    {
        AvailabilityPerStockTypePerShopSize newAvailabilityPerStockTypePerShopSize = CreateInstance<AvailabilityPerStockTypePerShopSize> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newAvailabilityPerStockTypePerShopSize.name = name;
        newAvailabilityPerStockTypePerShopSize.m_ShopSizeEnum = Campaign.ShopSizes;
        newAvailabilityPerStockTypePerShopSize.m_ShopSizeAvailibilityPerPowerLevels = new IntRangePerPowerLevelPerStockType[Campaign.ShopSizes.Length];

        for(int i = 0; i < Campaign.ShopSizes.Length; i++)
        {
            newAvailabilityPerStockTypePerShopSize.m_ShopSizeAvailibilityPerPowerLevels[i] = Campaign.AvailabilityPerStockTypePerShopSize[Campaign.ShopSizes[i]];
        }

        SaveableHolder.AddSaveable(newAvailabilityPerStockTypePerShopSize);

        return newAvailabilityPerStockTypePerShopSize;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_ShopSizeEnum.name + jsonSplitter[0];
        for (int i = 0; i < m_ShopSizeAvailibilityPerPowerLevels.Length; i++)
        {
            jsonString += m_ShopSizeAvailibilityPerPowerLevels[i].name + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_ShopSizeEnum = EnumSetting.Load(splitJsonString[1]);
        m_ShopSizeAvailibilityPerPowerLevels = new IntRangePerPowerLevelPerStockType[splitJsonString.Length - 2];
        for (int i = 0; i < m_ShopSizeAvailibilityPerPowerLevels.Length; i++)
        {
            m_ShopSizeAvailibilityPerPowerLevels[i] = IntRangePerPowerLevelPerStockType.Load(splitJsonString[i + 2]);
        }
    }
}
