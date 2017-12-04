using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveableSelectedEnumPerEnum : Saveable<SaveableSelectedEnumPerEnum>
{
    public EnumSetting enumSetting;
    public JsonableSelectedEnumSetting[] selectedEnumSettings;

    public JsonableSelectedEnumSetting this[string selectedEnum]
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

    public JsonableSelectedEnumSetting this [int index]
    {
        get
        {
            return selectedEnumSettings[index];
        }
    }

    public static SaveableSelectedEnumPerEnum Create(string name, EnumSetting enumSetting, JsonableSelectedEnumSetting[] selectedEnumSettings)
    {
        SaveableSelectedEnumPerEnum newSelectedEnumPerEnum = CreateInstance<SaveableSelectedEnumPerEnum>();
        newSelectedEnumPerEnum.enumSetting = enumSetting;
        newSelectedEnumPerEnum.selectedEnumSettings = new JsonableSelectedEnumSetting[enumSetting.Length];
        return newSelectedEnumPerEnum;
    }

    public static SaveableSelectedEnumPerEnum CreateBlank(EnumSetting enumSetting, EnumSetting enumSettings)
    {
        JsonableSelectedEnumSetting[] selectedEnumSettings = new JsonableSelectedEnumSetting[enumSetting.Length];
        for (int i = 0; i < selectedEnumSettings.Length; i++)
        {
            selectedEnumSettings[i] = JsonableSelectedEnumSetting.CreateBlank(enumSettings);
        }
        return Create("NAME", enumSetting, selectedEnumSettings);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += enumSetting.name + jsonSplitter[0];

        for (int i = 0; i < selectedEnumSettings.Length; i++)
        {
            jsonString += JsonableSelectedEnumSetting.GetJsonString(selectedEnumSettings[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        enumSetting = EnumSetting.Load(splitJsonString[0]);

        selectedEnumSettings = new JsonableSelectedEnumSetting[splitJsonString.Length - 1];
        for (int i = 0; i < selectedEnumSettings.Length; i++)
        {
            selectedEnumSettings[i] = JsonableSelectedEnumSetting.CreateFromJsonString(splitJsonString[i + 1]);
        }
    }
}
