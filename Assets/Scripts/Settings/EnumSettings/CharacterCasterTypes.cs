using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterCasterTypes : Saveable<CharacterCasterTypes>
{
    public enum CasterType
    {
        NoSpells, Level4Spells, Level6Spells, Level9Spells,
    }


    public EnumSetting characterClasses;
    public CasterType[] casterTypes = new CasterType[0];

    public CasterType this [string characterClass]
    {
        get
        {
            for(int i = 0; i < characterClasses.settings.Length; i++)
            {
                if(characterClasses.settings[i] == characterClass)
                {
                    return casterTypes[i];
                }
            }
            return CasterType.NoSpells;
        }
    }

    public CasterType this [int index]
    {
        get { return casterTypes[index]; }
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += characterClasses.name + jsonSplitter[0];

        for (int i = 0; i < casterTypes.Length; i++)
        {
            jsonString += Wrapper<int>.GetJsonString((int)casterTypes[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        int jsonIndex = 0;
        characterClasses = EnumSetting.Load(splitJsonString[jsonIndex]);
        jsonIndex++;

        casterTypes = new CasterType[splitJsonString.Length - 1];
        for (int i = 0; i < casterTypes.Length; i++, jsonIndex++)
        {
            casterTypes[i] = (CasterType)Wrapper<int>.CreateFromJsonString(splitJsonString[jsonIndex]);
        }
    }

    public static int MinCasterLevel (CasterType casterType, int spellLevel)
    {
        if (casterType == CasterType.Level4Spells)
            return spellLevel * 3 + 1;
        if (casterType == CasterType.Level6Spells)
            return spellLevel * 3 - 2;
        if (casterType == CasterType.Level9Spells)
            return spellLevel * 2 - 1;
        return -1;
    }
}
