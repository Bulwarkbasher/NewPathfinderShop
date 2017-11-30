using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumSettingIntPairing : Jsonable<EnumSettingIntPairing>
{
    public EnumSetting enumSetting;
    public int[] pairings = new int[0];

    public int this[int index]
    {
        get
        {
            index = Mathf.Clamp(index, 0, pairings.Length);
            return pairings[index];
        }
    }

    public int this[string setting]
    {
        get
        {
            for (int i = 0; i < enumSetting.settings.Length; i++)
            {
                if (enumSetting.settings[i] == setting)
                    return this[i];
            }
            return -1;
        }
    }

    public static EnumSettingIntPairing Create (EnumSetting enumSetting, int[] pairings)
    {
        EnumSettingIntPairing newEnumSettingIntPairing = CreateInstance<EnumSettingIntPairing>();
        newEnumSettingIntPairing.enumSetting = enumSetting;
        newEnumSettingIntPairing.pairings = pairings;
        return newEnumSettingIntPairing;
    }

    public static EnumSettingIntPairing CreateBlank (EnumSetting enumSetting)
    {
        return Create(enumSetting, new int[enumSetting.Length]);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += enumSetting.name + jsonSplitter[0];
        
        for(int i = 0; i < pairings.Length; i++)
        {
            jsonString += Wrapper<int>.GetJsonString(pairings[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        enumSetting = EnumSetting.Load(splitJsonString[0]);

        pairings = new int[splitJsonString.Length - 1];
        for(int i = 0; i < pairings.Length; i++)
        {
            pairings[i] = Wrapper<int>.CreateFromJsonString(splitJsonString[i + 1]);
        }
    }
}