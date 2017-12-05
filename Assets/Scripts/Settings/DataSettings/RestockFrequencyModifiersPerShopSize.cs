using System;
using UnityEngine;

[CreateAssetMenu]
public class RestockFrequencyModifiersPerShopSize : SaveableWithEnumValues<RestockFrequencyModifiersPerShopSize, float>
{
    public static RestockFrequencyModifiersPerShopSize Create (string name, EnumSetting shopSizeEnum)
    {
        RestockFrequencyModifiersPerShopSize newPerSizeRestockFrequencyModifiers = CreateInstance<RestockFrequencyModifiersPerShopSize> ();

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
