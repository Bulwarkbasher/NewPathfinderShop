using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveableSelectedEnumPerEnum : Saveable<SaveableSelectedEnumPerEnum>
{
    public EnumSetting enumSetting;
    public EnumValue[] selectedEnumSettings;

    public EnumValue this[string selectedEnum]
    {
        get
        {
            for (int i = 0; i < enumSetting.Length; i++)
            {
                if (enumSetting[i] == selectedEnum)
                {
                    return selectedEnumSettings[i];
                }
            }
            throw new ArgumentOutOfRangeException(nameof(selectedEnum), selectedEnum, null);
        }
    }

    public EnumValue this [int index]
    {
        get
        {
            return selectedEnumSettings[index];
        }
    }

    public static SaveableSelectedEnumPerEnum Create(string name, EnumSetting enumSetting, EnumValue[] selectedEnumSettings)
    {
        SaveableSelectedEnumPerEnum newSelectedEnumPerEnum = CreateInstance<SaveableSelectedEnumPerEnum>();
        newSelectedEnumPerEnum.enumSetting = enumSetting;
        newSelectedEnumPerEnum.selectedEnumSettings = new EnumValue[enumSetting.Length];
        return newSelectedEnumPerEnum;
    }

    public static SaveableSelectedEnumPerEnum CreateBlank(EnumSetting enumSetting, EnumSetting enumSettings)
    {
        EnumValue[] selectedEnumSettings = new EnumValue[enumSetting.Length];
        for (int i = 0; i < selectedEnumSettings.Length; i++)
        {
            selectedEnumSettings[i] = EnumValue.CreateBlank(enumSettings);
        }
        return Create("NAME", enumSetting, selectedEnumSettings);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += enumSetting.name + jsonSplitter[0];

        for (int i = 0; i < selectedEnumSettings.Length; i++)
        {
            jsonString += EnumValue.GetJsonString(selectedEnumSettings[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        enumSetting = EnumSetting.Load(splitJsonString[0]);

        selectedEnumSettings = new EnumValue[splitJsonString.Length - 1];
        for (int i = 0; i < selectedEnumSettings.Length; i++)
        {
            selectedEnumSettings[i] = EnumValue.CreateFromJsonString(splitJsonString[i + 1]);
        }
    }
}
