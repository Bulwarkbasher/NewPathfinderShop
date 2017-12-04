using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonableBoolPerEnumSetting : Jsonable<JsonableBoolPerEnumSetting>
{
    public EnumSetting enumSetting;
    public bool[] pairings = new bool[0];

    public bool this [int index]
    {
        get
        {
            return pairings[index];
        }
        set
        {
            pairings[index] = value;
        }
    }

    public bool this [string setting]
    {
        get
        {
            for (int i = 0; i < enumSetting.settings.Length; i++)
            {
                if (enumSetting.settings[i] == setting)
                    return this[i];
            }
            return false;
        }
        set
        {
            for (int i = 0; i < enumSetting.settings.Length; i++)
            {
                if (enumSetting.settings[i] == setting)
                {
                    pairings[i] = value;
                    return;
                }
            }
            throw new IndexOutOfRangeException();
        }
    }

    public static JsonableBoolPerEnumSetting Create(EnumSetting enumSetting, bool[] pairings)
    {
        JsonableBoolPerEnumSetting newEnumSettingIntPairing = CreateInstance<JsonableBoolPerEnumSetting>();
        newEnumSettingIntPairing.enumSetting = enumSetting;
        newEnumSettingIntPairing.pairings = pairings;
        return newEnumSettingIntPairing;
    }

    public static JsonableBoolPerEnumSetting CreateBlank(EnumSetting enumSetting, bool defaultPairing = false)
    {
        bool[] pairings = new bool[enumSetting.Length];
        for (int i = 0; i < pairings.Length; i++)
            pairings[i] = defaultPairing;
        return Create(enumSetting, pairings);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += enumSetting.name + jsonSplitter[0];

        for (int i = 0; i < pairings.Length; i++)
        {
            jsonString += Wrapper<bool>.GetJsonString(pairings[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        enumSetting = EnumSetting.Load(splitJsonString[0]);

        pairings = new bool[splitJsonString.Length - 1];
        for (int i = 0; i < pairings.Length; i++)
        {
            pairings[i] = Wrapper<bool>.CreateFromJsonString(splitJsonString[i + 1]);
        }
    }
}
