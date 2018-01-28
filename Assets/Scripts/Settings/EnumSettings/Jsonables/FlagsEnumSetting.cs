using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagsEnumSetting : JsonableEnumWithValues<FlagsEnumSetting, bool>
{
    public new bool this [int index]
    {
        get { return enumedValues[index]; }
        set { enumedValues[index] = value; }
    }

    public new bool this [string selectedEnum]
    {
        get { return enumedValues[enumSetting[selectedEnum]]; }
        set { enumedValues[enumSetting[selectedEnum]] = value; }
    }

    public bool AnyFlag
    {
        get
        {
            bool anyFlag = false;
            for (int i = 0; i < enumedValues.Length; i++)
                anyFlag |= enumedValues[i];
            return anyFlag;
        }
    }

    public static FlagsEnumSetting Create(EnumSetting enumSetting, bool[] enumedValues)
    {
        FlagsEnumSetting newFlagEnumSetting = CreateInstance<FlagsEnumSetting>();
        newFlagEnumSetting.enumSetting = enumSetting;
        newFlagEnumSetting.enumedValues = enumedValues;
        return newFlagEnumSetting;
    }

    public static FlagsEnumSetting CreateBlank(EnumSetting enumSetting, bool defaultValue = false)
    {
        bool[] pairings = new bool[enumSetting.Length];
        for (int i = 0; i < pairings.Length; i++)
            pairings[i] = defaultValue;
        return Create(enumSetting, pairings);
    }
}
