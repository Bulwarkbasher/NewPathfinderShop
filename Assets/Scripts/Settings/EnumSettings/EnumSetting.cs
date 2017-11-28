using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnumSetting : Saveable<EnumSetting>
{
    public string[] settings = new string[0];

    public string DefaultSetting
    {
        get { return settings[0]; }
    }

    public string this [int index]
    {
        get
        {
            index = Mathf.Clamp(index, 0, settings.Length);
            return settings[index];
        }
    }

    public void AddSetting (string newSetting)
    {
        for(int i = 0; i < settings.Length; i++)
        {
            if (settings[i] == newSetting)
                return;
        }

        string[] newSettings = new string[settings.Length + 1];
        for(int i = 0; i < settings.Length; i++)
        {
            newSettings[i] = settings[i];
        }
        newSettings[settings.Length] = newSetting;
        settings = newSettings;
    }

    public bool RemoveSettingAt(int removeAtIndex)
    {
        if (removeAtIndex >= settings.Length || removeAtIndex < 0)
            return false;

        string[] newSettings = new string[settings.Length - 1];
        for (int i = 0; i < newSettings.Length; i++)
        {
            int oldItemIndex = i < removeAtIndex ? i : i + 1;
            newSettings[i] = settings[oldItemIndex];
        }
        settings = newSettings;
        return true;
    }

    public bool RemoveSetting(string setting)
    {
        for (int i = 0; i < settings.Length; i++)
        {
            if (settings[i] == setting)
            {
                return RemoveSettingAt(i);
            }
        }

        return false;
    }

    public bool NameIsUnique(string setting)
    {
        for (int i = 0; i < settings.Length; i++)
        {
            if (settings[i] == setting)
                return false;
        }
        return true;
    }

    public GUIContent[] SettingsAsGUIContentArray ()
    {
        GUIContent[] contents = new GUIContent[settings.Length];
        for(int i = 0; i < contents.Length; i++)
        {
            contents[i] = new GUIContent(settings[i]);
        }
        return contents;
    }

    public GUIContent NameAsGUIContent ()
    {
        return new GUIContent(name);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        
        for(int i = 0; i < settings.Length; i++)
        {
            jsonString += settings[i] + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];

        settings = new string[splitJsonString.Length - 1];
        for(int i = 0; i < settings.Length; i++)
        {
            settings[i] = splitJsonString[i + 1];
        }
    }
}