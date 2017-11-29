using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SelectedEnumSetting
{
    public EnumSetting enumSetting;
    public int index;

    public SelectedEnumSetting()
    {

    }

    public SelectedEnumSetting(EnumSetting enumSetting, int index)
    {
        this.enumSetting = enumSetting;
        this.index = index;
    }

    public static implicit operator string (SelectedEnumSetting selectedEnumSetting)
    {
        return selectedEnumSetting.enumSetting[selectedEnumSetting.index];
    }

    protected static string[] GetJsonSplitter()
    {
        string[] jsonSplitter = { "###EnumSettingIndexSplitter###" };
        return jsonSplitter;
    }

    public static SelectedEnumSetting CreateFromJsonString(string jsonString)
    {
        SelectedEnumSetting enumSettingIndex = new SelectedEnumSetting();

        string[] splitJsonString = jsonString.Split(GetJsonSplitter(), StringSplitOptions.RemoveEmptyEntries);

        enumSettingIndex.enumSetting = EnumSetting.Load(splitJsonString[0]);
        enumSettingIndex.index = Wrapper<int>.CreateFromJsonString(splitJsonString[1]);

        return enumSettingIndex;
    }

    public static string GetJsonString(SelectedEnumSetting enumSettingIndex)
    {
        string[] jsonSplitter = GetJsonSplitter();

        string jsonString = "";

        jsonString += enumSettingIndex.enumSetting.name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(enumSettingIndex.index) + jsonSplitter[0];

        return jsonString;
    }
}
