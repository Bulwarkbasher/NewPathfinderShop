using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RarityPerCharacterClassPerSpellContainer : Saveable<RarityPerCharacterClassPerSpellContainer>
{
    [SerializeField]
    protected EnumSetting m_SpellContainerEnum;
    [SerializeField]
    protected RarityPerCharacterClass[] m_CharacterClassRarities;

    public RarityPerCharacterClass this [string container]
    {
        get
        {
            for (int i = 0; i < m_SpellContainerEnum.Length; i++)
            {
                if (m_SpellContainerEnum[i] == container)
                {
                    return m_CharacterClassRarities[i];
                }
            }
            throw new ArgumentOutOfRangeException(nameof(container), container, null);
        }
    }

    public static RarityPerCharacterClassPerSpellContainer Create (string name, EnumSetting spellContainerEnum, RarityPerCharacterClass[] characterClassRarities)
    {
        RarityPerCharacterClassPerSpellContainer newPerContainerPerCreatorRarity = CreateInstance<RarityPerCharacterClassPerSpellContainer>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerContainerPerCreatorRarity.name = name;
        newPerContainerPerCreatorRarity.m_SpellContainerEnum = spellContainerEnum;
        newPerContainerPerCreatorRarity.m_CharacterClassRarities = characterClassRarities;

        SaveableHolder.AddSaveable(newPerContainerPerCreatorRarity);

        return newPerContainerPerCreatorRarity;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_SpellContainerEnum.name + jsonSplitter[0];
        for(int i = 0; i < m_CharacterClassRarities.Length; i++)
        {
            jsonString += m_CharacterClassRarities[i].name + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_SpellContainerEnum = EnumSetting.Load(splitJsonString[1]);

        m_CharacterClassRarities = new RarityPerCharacterClass[splitJsonString.Length - 2];
        for(int i = 0; i < m_CharacterClassRarities.Length; i++)
        {
            m_CharacterClassRarities[i] = RarityPerCharacterClass.Load(splitJsonString[i + 2]);
        }
    }
}
