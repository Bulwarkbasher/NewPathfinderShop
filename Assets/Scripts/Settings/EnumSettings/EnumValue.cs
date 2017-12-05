using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumValue : JsonableWithEnumValues<EnumValue, bool>
{
    public static EnumValue Create (EnumSetting enumSetting, int index)
    {
        EnumValue newEnumValue = CreateInstance<EnumValue>();
        newEnumValue.enumSetting = enumSetting;
        newEnumValue.enumedValues = new bool[enumSetting.Length];
        newEnumValue.enumedValues[index] = true;
        return newEnumValue;
    }

    public static EnumValue CreateBlank (EnumSetting enumSetting)
    {
        return Create(enumSetting, 0);
    }

    public static implicit operator string (EnumValue selectedEnumSetting)
    {
        for (int i = 0; i < selectedEnumSetting.enumedValues.Length; i++)
            if (selectedEnumSetting.enumedValues[i])
                return selectedEnumSetting.enumSetting[i];
        throw new IndexOutOfRangeException();
    }

    public void Set (int index)
    {
        for (int i = 0; i < enumedValues.Length; i++)
            enumedValues[i] = false;
        enumedValues[index] = true;
    }

    public void Set(string selectedEnum)
    {
        for (int i = 0; i < enumedValues.Length; i++)
            enumedValues[i] = false;
        enumedValues[enumSetting[selectedEnum]] = true;
    }
}
