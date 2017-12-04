using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonableSelectedEnumSetting : Jsonable<JsonableSelectedEnumSetting>
{
    public EnumSetting enumSetting;
    public int index;

    public static JsonableSelectedEnumSetting Create (EnumSetting enumSetting, int index)
    {
        JsonableSelectedEnumSetting newSelectedEnumSetting = CreateInstance<JsonableSelectedEnumSetting>();
        newSelectedEnumSetting.enumSetting = enumSetting;
        newSelectedEnumSetting.index = index;
        return newSelectedEnumSetting;
    }

    public static JsonableSelectedEnumSetting CreateBlank (EnumSetting enumSetting)
    {
        return Create(enumSetting, 0);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += enumSetting.name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(index) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        enumSetting = EnumSetting.Load(splitJsonString[0]);
        index = Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
    }

    public static implicit operator string (JsonableSelectedEnumSetting selectedEnumSetting)
    {
        return selectedEnumSetting.enumSetting[selectedEnumSetting.index];
    }
}
