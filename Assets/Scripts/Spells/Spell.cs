using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class Spell : Jsonable<Spell>
{
    public JsonableSelectedEnumPerEnum containerAllowances;      // containers, allowance
    public JsonableSelectedEnumPerEnum containerRarities;        // containers, rarities
    public IntValuedEnum creatorLevels;              // int (level), characterClasses
    public EnumValue book;
    public int page;
    public float materialCost;

    public static Spell Create(string name, JsonableSelectedEnumPerEnum containerAllowances, JsonableSelectedEnumPerEnum containerRarities,
        IntValuedEnum creatorLevels, EnumValue book, int page, int materialCost)
    {
        Spell newSpell = CreateInstance<Spell> ();
        newSpell.name = name;
        newSpell.containerAllowances = containerAllowances;
        newSpell.containerRarities = containerRarities;
        newSpell.creatorLevels = creatorLevels;
        newSpell.book = book;
        newSpell.page = page;
        newSpell.materialCost = materialCost;
        return newSpell;
    }

    public static Spell CreateBlank (EnumSetting spellContainers, EnumSetting allowances, EnumSetting rarities, EnumSetting characterClasses,
        SaveableSelectedEnumPerEnum casterTypesPerCharacterClass, EnumSetting books)
    {
        JsonableSelectedEnumPerEnum containerAllowances = JsonableSelectedEnumPerEnum.CreateBlank(spellContainers, allowances);
        JsonableSelectedEnumPerEnum containerRarities = JsonableSelectedEnumPerEnum.CreateBlank(spellContainers, rarities);
        IntValuedEnum creatorLevels = IntValuedEnum.CreateBlank(characterClasses);
        EnumValue book = EnumValue.CreateBlank(books);

        return Create ("NAME", containerAllowances, containerRarities, creatorLevels, book, 239, 0);
    }

    public static int MinCasterLevel(string casterType, int spellLevel)
    {
        if (casterType == "Level 4 Spells")
            return spellLevel * 3 + 1;
        if (casterType == "Level 6 Spells")
            return spellLevel * 3 - 2;
        if (casterType == "Level 9 Spells")
            return spellLevel * 2 - 1;
        return -1;
    }

    public float GetPotionCost(string creator, int creatorLevel)
    {
        string allowance = containerAllowances["Potion"];
        if (allowance == "Cannot Be")
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == "Default" && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return 25f + materialCost;

        return 50f * creationLevel * creatorLevel + materialCost;
    }

    public float GetScrollCost(string creator, int creatorLevel)
    {
        string allowance = containerAllowances["Scroll"];
        if (allowance == "Cannot Be")
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == "Default" && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return 12.5f + materialCost;

        return 25f * creationLevel * creatorLevel + materialCost;
    }
    
    public float GetWandCost(string creator, int creatorLevel, int charges)
    {
        string allowance = containerAllowances["Wand"];
        if (allowance == "Cannot Be")
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == "Default" && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return (7.5f + materialCost) * charges;

        return (15 + materialCost) * charges * creationLevel * creatorLevel;
    }

    public static Spell PickSpell (List<Spell> spells, string container)
    {
        float weightSum = 0f;

        for (int i = 0; i < spells.Count; i++)
        {
            EnumValue rarity = spells[i].containerRarities[container];
            weightSum += Campaign.WeightingPerRarity[rarity];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < spells.Count; i++)
        {
            EnumValue rarity = spells[i].containerRarities[container];
            weightCounter -= Campaign.WeightingPerRarity[rarity];

            if (weightCounter <= 0f)
            {
                return spells[i];
            }
        }

        return null;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += JsonableSelectedEnumPerEnum.GetJsonString(containerAllowances) + jsonSplitter[0];
        jsonString += JsonableSelectedEnumPerEnum.GetJsonString(containerRarities) + jsonSplitter[0];
        jsonString += IntValuedEnum.GetJsonString(creatorLevels) + jsonSplitter[0];
        jsonString += EnumValue.GetJsonString(book) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(materialCost) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        containerAllowances = JsonableSelectedEnumPerEnum.CreateFromJsonString(splitJsonString[1]);
        containerRarities = JsonableSelectedEnumPerEnum.CreateFromJsonString(splitJsonString[2]);
        creatorLevels = IntValuedEnum.CreateFromJsonString(splitJsonString[4]);
        book = EnumValue.CreateFromJsonString(splitJsonString[5]);
        page = Wrapper<int>.CreateFromJsonString (splitJsonString[6]);
        materialCost = Wrapper<float>.CreateFromJsonString (splitJsonString[7]);
    }
}