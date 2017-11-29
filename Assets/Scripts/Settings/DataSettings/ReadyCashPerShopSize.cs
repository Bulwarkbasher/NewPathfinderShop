using System;
using UnityEngine;

[CreateAssetMenu]
public class ReadyCashPerShopSize : Saveable<ReadyCashPerShopSize>
{
    [SerializeField]
    protected EnumSetting m_ShopSizeEnum;
    [SerializeField]
    protected float[] m_ShopSizeReadyCash;

    public float this[string shopSize]
    {
        get
        {
            for (int i = 0; i < m_ShopSizeEnum.Length; i++)
            {
                if (m_ShopSizeEnum[i] == shopSize)
                    return m_ShopSizeReadyCash[i];
            }
            throw new ArgumentOutOfRangeException(nameof(shopSize), shopSize, "Unknown Shop Size.");
        }
    }

    public static ReadyCashPerShopSize Create(string name, EnumSetting shopSizeEnum)
    {
        ReadyCashPerShopSize newPerSizeRestockFrequencyModifiers = CreateInstance<ReadyCashPerShopSize>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerSizeRestockFrequencyModifiers.name = name;
        newPerSizeRestockFrequencyModifiers.m_ShopSizeEnum = shopSizeEnum;
        newPerSizeRestockFrequencyModifiers.m_ShopSizeReadyCash = new float[shopSizeEnum.Length];

        SaveableHolder.AddSaveable(newPerSizeRestockFrequencyModifiers);

        return newPerSizeRestockFrequencyModifiers;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_ShopSizeEnum.name + jsonSplitter[0];
        for (int i = 0; i < m_ShopSizeReadyCash.Length; i++)
        {
            jsonString += Wrapper<float>.GetJsonString(m_ShopSizeReadyCash[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_ShopSizeEnum = EnumSetting.Load(splitJsonString[1]);
        m_ShopSizeReadyCash = new float[splitJsonString.Length - 2];
        for (int i = 0; i < m_ShopSizeReadyCash.Length; i++)
        {
            m_ShopSizeReadyCash[i] = Wrapper<float>.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}
