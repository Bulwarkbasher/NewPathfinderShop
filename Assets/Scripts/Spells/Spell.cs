using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class Spell : Jsonable<Spell>
{
    public enum Allowance
    {
        Default,
        CanBe,
        CannotBe,
    }


    public enum Container
    {
        Potion,
        Scroll,
        Wand,
    }


    public SpellContainerAllowances allowances;
    public SpellContainerRarities rarities;
    public EnumSettingIntPairing creatorLevels;
    public SelectedEnumSetting book;
    public int page;
    public int materialCost;

    public static Spell Create(string name, SpellContainerAllowances allowances, SpellContainerRarities rarities, CasterTypesPerCharacterClass creatorCasterTypes,
        EnumSettingIntPairing creatorLevels, SelectedEnumSetting book, int page, int materialCost)
    {
        Spell newSpell = CreateInstance<Spell> ();
        newSpell.name = name;
        newSpell.allowances = allowances;
        newSpell.rarities = rarities;
        newSpell.creatorLevels = creatorLevels;
        newSpell.book = book;
        newSpell.page = page;
        newSpell.materialCost = materialCost;
        return newSpell;
    }

    public static Spell CreateBlank (EnumSetting characterClasses, CasterTypesPerCharacterClass casterTypesPerCharacterClass, EnumSetting books)
    {
        return Create ("NAME", SpellContainerAllowances.CreateBlank(), SpellContainerRarities.CreateBlank(), casterTypesPerCharacterClass, 
            EnumSettingIntPairing.CreateBlank(characterClasses), SelectedEnumSetting.CreateBlank(books), 239, 0);
    }

    public float GetPotionCost(string creator, int creatorLevel)
    {
        Allowance allowance = allowances[Container.Potion];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return 25f + materialCost;

        return 50f * creationLevel * creatorLevel + materialCost;
    }

    public float GetScrollCost(string creator, int creatorLevel)
    {
        Allowance allowance = allowances[Container.Scroll];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return 12.5f + materialCost;

        return 25f * creationLevel * creatorLevel + materialCost;
    }
    
    public float GetWandCost(string creator, int creatorLevel, int charges)
    {
        Allowance allowance = allowances[Container.Wand];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return (7.5f + materialCost) * charges;

        return (15 + materialCost) * charges * creationLevel * creatorLevel;
    }

    public static Spell PickSpell (List<Spell> spells, Container container)
    {
        float weightSum = 0f;

        for (int i = 0; i < spells.Count; i++)
        {
            Item.Rarity rarity = spells[i].rarities[container];
            weightSum += Campaign.WeightingPerRarity[rarity];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < spells.Count; i++)
        {
            Item.Rarity rarity = spells[i].rarities[container];
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
        jsonString += SpellContainerAllowances.GetJsonString(allowances) + jsonSplitter[0];
        jsonString += SpellContainerRarities.GetJsonString(rarities) + jsonSplitter[0];
        jsonString += EnumSettingIntPairing.GetJsonString(creatorLevels) + jsonSplitter[0];
        jsonString += SelectedEnumSetting.GetJsonString(book) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(materialCost) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        allowances = SpellContainerAllowances.CreateFromJsonString(splitJsonString[1]);
        rarities = SpellContainerRarities.CreateFromJsonString(splitJsonString[2]);
        creatorLevels = EnumSettingIntPairing.CreateFromJsonString(splitJsonString[4]);
        book = SelectedEnumSetting.CreateFromJsonString(splitJsonString[5]);
        page = Wrapper<int>.CreateFromJsonString (splitJsonString[6]);
        materialCost = Wrapper<int>.CreateFromJsonString (splitJsonString[7]);
    }
}