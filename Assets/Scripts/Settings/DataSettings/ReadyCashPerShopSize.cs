using System;
using UnityEngine;

[CreateAssetMenu]
public class ReadyCashPerShopSize : SaveableWithEnumValues<ReadyCashPerShopSize, float>
{
    public static ReadyCashPerShopSize Create(string name, EnumSetting shopSizeEnum)
    {
        ReadyCashPerShopSize newPerSizeRestockFrequencyModifiers = CreateInstance<ReadyCashPerShopSize>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerSizeRestockFrequencyModifiers.name = name;
        newPerSizeRestockFrequencyModifiers.m_EnumSetting = shopSizeEnum;
        newPerSizeRestockFrequencyModifiers.m_EnumedValues = new float[shopSizeEnum.Length];

        SaveableHolder.AddSaveable(newPerSizeRestockFrequencyModifiers);

        return newPerSizeRestockFrequencyModifiers;
    }
}
