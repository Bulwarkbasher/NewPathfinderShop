using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class Spell : Jsonable<Spell>
{
    /*[Flags]
    public enum Allowance
    {
        Default,
        CanBe,
        CannotBe,
    }

    [Flags]
    public enum Container
    {
        Potion,
        Scroll,
        Wand,
    }*/


    public JsonableSelectedEnumPerEnum containerAllowances;      // containers, allowance
    public JsonableSelectedEnumPerEnum containerRarities;        // containers, rarities
    public JsonableIntPerEnumSetting creatorLevels;              // int (level), characterClasses
    public JsonableSelectedEnumSetting book;
    public int page;
    public int materialCost;

    public static Spell Create(string name, JsonableSelectedEnumPerEnum containerAllowances, JsonableSelectedEnumPerEnum containerRarities,
        JsonableIntPerEnumSetting creatorLevels, JsonableSelectedEnumSetting book, int page, int materialCost)
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
        JsonableIntPerEnumSetting creatorLevels = JsonableIntPerEnumSetting.CreateBlank(characterClasses);
        JsonableSelectedEnumSetting book = JsonableSelectedEnumSetting.CreateBlank(books);

        return Create ("NAME", containerAllowances, containerRarities, creatorLevels, book, 239, 0);
    }

    public static int MinCasterLevel(string casterType, int spellLevel)
    {
        if (casterType == "Level4Spells")
            return spellLevel * 3 + 1;
        if (casterType == "Level6Spells")
            return spellLevel * 3 - 2;
        if (casterType == "Level9Spells")
            return spellLevel * 2 - 1;
        return -1;
    }

    public float GetPotionCost(string creator, int creatorLevel)
    {
        string allowance = containerAllowances["Potion"];
        if (allowance == "CannotBe")
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
        if (allowance == "CannotBe")
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
        if (allowance == "CannotBe")
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
            JsonableSelectedEnumSetting rarity = spells[i].containerRarities[container];
            weightSum += Campaign.WeightingPerRarity[rarity];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < spells.Count; i++)
        {
            JsonableSelectedEnumSetting rarity = spells[i].containerRarities[container];
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
        jsonString += JsonableIntPerEnumSetting.GetJsonString(creatorLevels) + jsonSplitter[0];
        jsonString += JsonableSelectedEnumSetting.GetJsonString(book) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(materialCost) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        containerAllowances = JsonableSelectedEnumPerEnum.CreateFromJsonString(splitJsonString[1]);
        containerRarities = JsonableSelectedEnumPerEnum.CreateFromJsonString(splitJsonString[2]);
        creatorLevels = JsonableIntPerEnumSetting.CreateFromJsonString(splitJsonString[4]);
        book = JsonableSelectedEnumSetting.CreateFromJsonString(splitJsonString[5]);
        page = Wrapper<int>.CreateFromJsonString (splitJsonString[6]);
        materialCost = Wrapper<int>.CreateFromJsonString (splitJsonString[7]);
    }
}