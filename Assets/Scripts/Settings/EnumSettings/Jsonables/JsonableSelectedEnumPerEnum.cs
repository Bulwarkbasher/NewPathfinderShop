using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonableSelectedEnumPerEnum : JsonableEnumWithJsonables<JsonableSelectedEnumPerEnum, EnumValue>
{
    public static JsonableSelectedEnumPerEnum Create (EnumSetting enumSetting, EnumValue[] selectedEnumSettings)
    {
        JsonableSelectedEnumPerEnum selectedEnumPerEnum = CreateInstance<JsonableSelectedEnumPerEnum>();
        selectedEnumPerEnum.enumSetting = enumSetting;
        selectedEnumPerEnum.enumedJsonables = new EnumValue[enumSetting.Length];
        return selectedEnumPerEnum;
    }

    public static JsonableSelectedEnumPerEnum CreateBlank (EnumSetting enumSetting, EnumSetting enumSettings)
    {
        EnumValue[] selectedEnumSettings = new EnumValue[enumSetting.Length];
        for(int i = 0; i < selectedEnumSettings.Length; i++)
        {
            selectedEnumSettings[i] = EnumValue.CreateBlank(enumSettings);
        }
        return Create(enumSetting, selectedEnumSettings);
    }
}
