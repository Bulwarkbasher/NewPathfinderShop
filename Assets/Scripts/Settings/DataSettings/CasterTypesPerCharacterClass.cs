using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CasterTypesPerCharacterClass : Saveable<CasterTypesPerCharacterClass>
{
    public enum CasterType
    {
        NoSpells, Level4Spells, Level6Spells, Level9Spells,
    }

    [SerializeField]
    protected EnumSetting m_CharacterClassesEnum;
    [SerializeField]
    protected CasterType[] m_CasterTypes = new CasterType[0];

    public CasterType this [string characterClass]
    {
        get
        {
            for(int i = 0; i < m_CharacterClassesEnum.Length; i++)
            {
                if(m_CharacterClassesEnum[i] == characterClass)
                {
                    return m_CasterTypes[i];
                }
            }
            return CasterType.NoSpells;
        }
    }

    public CasterType this [int index]
    {
        get { return m_CasterTypes[index]; }
    }

    public EnumSetting CharacterClassesEnum
    {
        get { return m_CharacterClassesEnum; }
    }

    public static CasterTypesPerCharacterClass Create (string name, EnumSetting characterClasses)
    {
        CasterTypesPerCharacterClass newCharacterCasterTypes = CreateInstance<CasterTypesPerCharacterClass>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newCharacterCasterTypes.name = name;
        newCharacterCasterTypes.m_CharacterClassesEnum = characterClasses;
        newCharacterCasterTypes.m_CasterTypes = new CasterType[characterClasses.Length];

        SaveableHolder.AddSaveable(newCharacterCasterTypes);

        return newCharacterCasterTypes;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += m_CharacterClassesEnum.name + jsonSplitter[0];

        for (int i = 0; i < m_CasterTypes.Length; i++)
        {
            jsonString += Wrapper<int>.GetJsonString((int)m_CasterTypes[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        int jsonIndex = 0;
        m_CharacterClassesEnum = EnumSetting.Load(splitJsonString[jsonIndex]);
        jsonIndex++;

        m_CasterTypes = new CasterType[splitJsonString.Length - 1];
        for (int i = 0; i < m_CasterTypes.Length; i++, jsonIndex++)
        {
            m_CasterTypes[i] = (CasterType)Wrapper<int>.CreateFromJsonString(splitJsonString[jsonIndex]);
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
