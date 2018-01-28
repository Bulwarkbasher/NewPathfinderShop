using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class RarityPerCharacterClass : SaveableWithEnumJsonables<RarityPerCharacterClass, EnumValue>
{
    public static RarityPerCharacterClass Create(string name, EnumSetting characterClasses, EnumValue[] containerRarities)
    {
        RarityPerCharacterClass newRarityPerCharacterClass = CreateInstance<RarityPerCharacterClass>();
        newRarityPerCharacterClass.m_EnumSetting = characterClasses;
        newRarityPerCharacterClass.m_EnumedJsonables = new EnumValue[characterClasses.Length];
        return newRarityPerCharacterClass;
    }

    public static RarityPerCharacterClass CreateBlank(EnumSetting characterClasses, EnumSetting rarities)
    {
        EnumValue[] classRarities = new EnumValue[characterClasses.Length];
        for (int i = 0; i < classRarities.Length; i++)
        {
            classRarities[i] = EnumValue.CreateBlank(rarities);
        }
        return Create("NAME", characterClasses, classRarities);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_EnumSetting.name + jsonSplitter[0];
        for (int i = 0; i < m_EnumedJsonables.Length; i++)
        {
            jsonString += EnumValue.GetJsonString(m_EnumedJsonables[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_EnumSetting = EnumSetting.Load(splitJsonString[1]);

        m_EnumedJsonables = new EnumValue[splitJsonString.Length - 2];
        for (int i = 0; i < m_EnumedJsonables.Length; i++)
        {
            m_EnumedJsonables[i] = EnumValue.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }

    public string PickClass()
    {
        float weightSum = 0f;

        for (int i = 0; i < m_EnumedJsonables.Length; i++)
        {
            weightSum += Campaign.WeightingPerRarity[m_EnumedJsonables[i]];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < m_EnumedJsonables.Length; i++)
        {
            weightCounter -= Campaign.WeightingPerRarity[m_EnumedJsonables[i]];

            if (weightCounter <= 0f)
            {
                return m_EnumSetting[i];
            }
        }

        return null;
    }

    public string PickSpellCastingClass (SaveableSelectedEnumPerEnum characterCasterTypes)
    {
        float weightSum = 0f;

        for (int i = 0; i < m_EnumedJsonables.Length; i++)
        {
            if (characterCasterTypes[i] == "NoSpells")
                continue;

            weightSum += Campaign.WeightingPerRarity[m_EnumedJsonables[i]];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < m_EnumedJsonables.Length; i++)
        {
            if (characterCasterTypes[i] == "NoSpells")
                continue;

            weightCounter -= Campaign.WeightingPerRarity[m_EnumedJsonables[i]];

            if (weightCounter <= 0f)
            {
                return m_EnumSetting[i];
            }
        }

        return null;
    }
}
