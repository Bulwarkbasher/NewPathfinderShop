using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntValuedEnum : JsonableWithEnumValues<IntValuedEnum, int>
{
    public static IntValuedEnum Create (EnumSetting enumSetting, int[] enumedValues)
    {
        IntValuedEnum newIntValuedEnum = CreateInstance<IntValuedEnum>();
        newIntValuedEnum.enumSetting = enumSetting;
        newIntValuedEnum.enumedValues = enumedValues;
        return newIntValuedEnum;
    }

    public static IntValuedEnum CreateBlank (EnumSetting enumSetting)
    {
        return Create(enumSetting, new int[enumSetting.Length]);
    }
}