using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class RarityPerCharacterClass : Saveable<RarityPerCharacterClass>
{
    [SerializeField]
    protected EnumSetting m_CharacterClasses;
    [SerializeField]
    protected Item.Rarity[] m_CharacterClassRarities;

    public Item.Rarity this [string characterClass]
    {
        get
        {
            for (int i = 0; i < m_CharacterClasses.settings.Length; i++)
            {
                if (m_CharacterClasses.settings[i] == characterClass)
                {
                    return m_CharacterClassRarities[i];
                }
            }
            throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null);
        }
    }

    public Item.Rarity this [int index]
    {
        get { return m_CharacterClassRarities[index]; }
    }    

    public static RarityPerCharacterClass Create (string name, EnumSetting characterClasses, Item.Rarity[] rarities)
    {
        RarityPerCharacterClass newPerCreatorRarity = CreateInstance<RarityPerCharacterClass>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerCreatorRarity.name = name;
        newPerCreatorRarity.m_CharacterClasses = characterClasses;
        newPerCreatorRarity.m_CharacterClassRarities = rarities;

        SaveableHolder.AddSaveable(newPerCreatorRarity);

        return newPerCreatorRarity;
    }


    public string PickClass()
    {
        float weightSum = 0f;

        for (int i = 0; i < m_CharacterClassRarities.Length; i++)
        {
            weightSum += Campaign.WeightingPerRarity[m_CharacterClassRarities[i]];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < m_CharacterClassRarities.Length; i++)
        {
            weightCounter -= Campaign.WeightingPerRarity[m_CharacterClassRarities[i]];

            if (weightCounter <= 0f)
            {
                return m_CharacterClasses.settings[i];
            }
        }

        return null;
    }

    public string PickSpellCastingClass (CasterTypesPerCharacterClass characterCasterTypes)
    {
        float weightSum = 0f;

        for (int i = 0; i < m_CharacterClassRarities.Length; i++)
        {
            if (characterCasterTypes[i] == CasterTypesPerCharacterClass.CasterType.NoSpells)
                continue;

            weightSum += Campaign.WeightingPerRarity[m_CharacterClassRarities[i]];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < m_CharacterClassRarities.Length; i++)
        {
            if (characterCasterTypes[i] == CasterTypesPerCharacterClass.CasterType.NoSpells)
                continue;

            weightCounter -= Campaign.WeightingPerRarity[m_CharacterClassRarities[i]];

            if (weightCounter <= 0f)
            {
                return m_CharacterClasses.settings[i];
            }
        }

        return null;
    }

    protected override string ConvertToJsonString (string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_CharacterClasses.name + jsonSplitter[0];
        
        for(int i = 0; i < m_CharacterClassRarities.Length; i++)
        {
            jsonString += Wrapper<int>.GetJsonString((int)m_CharacterClassRarities[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString (string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_CharacterClasses = EnumSetting.Load(splitJsonString[1]);

        m_CharacterClassRarities = new Item.Rarity[splitJsonString.Length - 2];
        for(int i = 0; i < m_CharacterClassRarities.Length; i++)
        {
            m_CharacterClassRarities[i] = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}
