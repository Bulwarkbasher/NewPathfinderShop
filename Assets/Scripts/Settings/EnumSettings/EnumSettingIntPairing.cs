using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnumSettingIntPairing
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

    public EnumSettingIntPairing()
    {

    }

    public EnumSettingIntPairing(EnumSetting enumSetting)
    {
        this.enumSetting = enumSetting;
        pairings = new int[enumSetting.settings.Length];
    }

    protected static string[] GetJsonSplitter()
    {
        string[] jsonSplitter = { "###EnumSettingIntPairingSplitter###" };
        return jsonSplitter;
    }

    public void AddPairing (string setting, int pairing)
    {
        for (int i = 0; i < enumSetting.settings.Length; i++)
        {
            if (enumSetting.settings[i] == setting)
                return;
        }

        string[] newSettings = new string[enumSetting.settings.Length + 1];
        int[] newPairings = new int[enumSetting.settings.Length + 1];
        for (int i = 0; i < enumSetting.settings.Length; i++)
        {
            newSettings[i] = enumSetting.settings[i];
            newPairings[i] = pairings[i];
        }
        newSettings[enumSetting.settings.Length] = setting;
        newPairings[enumSetting.settings.Length] = pairing;
        enumSetting.settings = newSettings;
        pairings = newPairings;
    }

    public bool RemovePairingAt(int removeAtIndex)
    {
        if (removeAtIndex >= enumSetting.settings.Length || removeAtIndex < 0)
            return false;

        string[] newSettings = new string[enumSetting.settings.Length - 1];
        int[] newPairings = new int[enumSetting.settings.Length - 1];
        for (int i = 0; i < newSettings.Length; i++)
        {
            int oldItemIndex = i < removeAtIndex ? i : i + 1;
            newSettings[i] = enumSetting.settings[oldItemIndex];
            newPairings[i] = pairings[oldItemIndex];
        }
        enumSetting.settings = newSettings;
        pairings = newPairings;
        return true;
    }

    public bool RemovePairing(string setting)
    {
        for (int i = 0; i < enumSetting.settings.Length; i++)
        {
            if (enumSetting.settings[i] == setting)
            {
                return RemovePairingAt(i);
            }
        }

        return false;
    }

    public static EnumSettingIntPairing CreateFromJsonString(string jsonString)
    {
        EnumSettingIntPairing enumSettingIndex = new EnumSettingIntPairing();

        string[] splitJsonString = jsonString.Split(GetJsonSplitter(), StringSplitOptions.RemoveEmptyEntries);

        enumSettingIndex.enumSetting = EnumSetting.Load(splitJsonString[0]);
        enumSettingIndex.pairings = new int[splitJsonString.Length - 1];
        for (int i = 0; i < enumSettingIndex.pairings.Length; i++)
        {
            enumSettingIndex.pairings[i] = Wrapper<int>.CreateFromJsonString(splitJsonString[i + 1]);
        }

        return enumSettingIndex;
    }

    public static string GetJsonString(EnumSettingIntPairing enumSettingIntPairing)
    {
        string[] jsonSplitter = GetJsonSplitter();

        string jsonString = "";

        jsonString += enumSettingIntPairing.enumSetting.name + jsonSplitter[0];

        for (int i = 0; i < enumSettingIntPairing.pairings.Length; i++)
        {
            jsonString += Wrapper<int>.GetJsonString(enumSettingIntPairing.pairings[i]) + jsonSplitter[0];
        }

        return jsonString;
    }
}