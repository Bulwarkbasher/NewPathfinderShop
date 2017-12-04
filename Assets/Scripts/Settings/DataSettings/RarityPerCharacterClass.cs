using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class RarityPerCharacterClass : Saveable<RarityPerCharacterClass>
{
    public EnumSetting characterClassEnum;
    public JsonableSelectedEnumSetting[] selectedRarityEnums;

    public JsonableSelectedEnumSetting this[string characterClass]
    {
        get
        {
            for (int i = 0; i < characterClassEnum.Length; i++)
            {
                if (characterClassEnum[i] == characterClass)
                {
                    return selectedRarityEnums[i];
                }
            }
            throw new ArgumentOutOfRangeException(nameof(characterClass), characterClass, null);
        }
    }

    public JsonableSelectedEnumSetting this[int index]
    {
        get
        {
            return selectedRarityEnums[index];
        }
    }

    public static JsonableSelectedEnumPerEnum Create(string name, EnumSetting containers, JsonableSelectedEnumSetting[] containerRarities)
    {
        JsonableSelectedEnumPerEnum newSpellContainerRarities = CreateInstance<JsonableSelectedEnumPerEnum>();
        newSpellContainerRarities.enumSetting = containers;
        newSpellContainerRarities.selectedEnumSettings = new JsonableSelectedEnumSetting[containers.Length];
        return newSpellContainerRarities;
    }

    public static JsonableSelectedEnumPerEnum CreateBlank(EnumSetting containers, EnumSetting rarities)
    {
        JsonableSelectedEnumSetting[] containerRarities = new JsonableSelectedEnumSetting[containers.Length];
        for (int i = 0; i < containerRarities.Length; i++)
        {
            containerRarities[i] = JsonableSelectedEnumSetting.CreateBlank(rarities);
        }
        return Create("NAME", containers, containerRarities);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += characterClassEnum.name + jsonSplitter[0];

        for (int i = 0; i < selectedRarityEnums.Length; i++)
        {
            jsonString += JsonableSelectedEnumSetting.GetJsonString(selectedRarityEnums[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        characterClassEnum = EnumSetting.Load(splitJsonString[0]);

        selectedRarityEnums = new JsonableSelectedEnumSetting[splitJsonString.Length - 1];
        for (int i = 0; i < selectedRarityEnums.Length; i++)
        {
            selectedRarityEnums[i] = JsonableSelectedEnumSetting.CreateFromJsonString(splitJsonString[i + 1]);
        }
    }

    public string PickClass()
    {
        float weightSum = 0f;

        for (int i = 0; i < selectedRarityEnums.Length; i++)
        {
            weightSum += Campaign.WeightingPerRarity[selectedRarityEnums[i]];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < selectedRarityEnums.Length; i++)
        {
            weightCounter -= Campaign.WeightingPerRarity[selectedRarityEnums[i]];

            if (weightCounter <= 0f)
            {
                return characterClassEnum[i];
            }
        }

        return null;
    }

    public string PickSpellCastingClass (SaveableSelectedEnumPerEnum characterCasterTypes)
    {
        float weightSum = 0f;

        for (int i = 0; i < selectedRarityEnums.Length; i++)
        {
            if (characterCasterTypes[i] == "NoSpells")
                continue;

            weightSum += Campaign.WeightingPerRarity[selectedRarityEnums[i]];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < selectedRarityEnums.Length; i++)
        {
            if (characterCasterTypes[i] == "NoSpells")
                continue;

            weightCounter -= Campaign.WeightingPerRarity[selectedRarityEnums[i]];

            if (weightCounter <= 0f)
            {
                return characterClassEnum[i];
            }
        }

        return null;
    }
}
