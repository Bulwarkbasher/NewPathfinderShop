using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedEnumSetting : Jsonable<SelectedEnumSetting>
{
    public EnumSetting enumSetting;
    public int index;

    public static SelectedEnumSetting Create (EnumSetting enumSetting, int index)
    {
        SelectedEnumSetting newSelectedEnumSetting = CreateInstance<SelectedEnumSetting>();
        newSelectedEnumSetting.enumSetting = enumSetting;
        newSelectedEnumSetting.index = index;
        return newSelectedEnumSetting;
    }

    public static SelectedEnumSetting CreateBlank (EnumSetting enumSetting)
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

    public static implicit operator string (SelectedEnumSetting selectedEnumSetting)
    {
        return selectedEnumSetting.enumSetting[selectedEnumSetting.index];
    }
}
