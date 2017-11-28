using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnumSettingIndex
{
    public EnumSetting enumSetting;
    public int index;

    public string Current
    {
        get { return enumSetting[index]; }
    }

    public EnumSettingIndex()
    {

    }

    public EnumSettingIndex(EnumSetting enumSetting, int index)
    {
        this.enumSetting = enumSetting;
        this.index = index;
    }

    protected static string[] GetJsonSplitter()
    {
        string[] jsonSplitter = { "###EnumSettingIndexSplitter###" };
        return jsonSplitter;
    }

    public static EnumSettingIndex CreateFromJsonString(string jsonString)
    {
        EnumSettingIndex enumSettingIndex = new EnumSettingIndex();

        string[] splitJsonString = jsonString.Split(GetJsonSplitter(), StringSplitOptions.RemoveEmptyEntries);

        enumSettingIndex.enumSetting = EnumSetting.Load(splitJsonString[0]);
        enumSettingIndex.index = Wrapper<int>.CreateFromJsonString(splitJsonString[1]);

        return enumSettingIndex;
    }

    public static string GetJsonString(EnumSettingIndex enumSettingIndex)
    {
        string[] jsonSplitter = GetJsonSplitter();

        string jsonString = "";

        jsonString += enumSettingIndex.enumSetting.name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(enumSettingIndex.index) + jsonSplitter[0];

        return jsonString;
    }
}
