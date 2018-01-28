using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCollectionFilter : Jsonable<SpellCollectionFilter>
{
    public bool showFilteredOnly;
    public string nameSearch;
    public string nameStartsWith;
    public FlagsEnumSetting allowedPotionAllowances;
    public FlagsEnumSetting allowedScrollAllowances;
    public FlagsEnumSetting allowedWandAllowances;
    public FlagsEnumSetting allowedPotionRarities;
    public FlagsEnumSetting allowedScrollRarities;
    public FlagsEnumSetting allowedWandRarities;
    public FlagsEnumSetting allowedCreators;
    public IntRange levelRange;
    public FloatRange materialCostRange;
    public FlagsEnumSetting allowedBooks;

    public static SpellCollectionFilter Create(string nameSearch, string nameStartingLetter,
        FlagsEnumSetting allowedPotionAllowances, FlagsEnumSetting allowedScrollAllowances,
        FlagsEnumSetting allowedWandAllowances, FlagsEnumSetting allowedPotionRarities,
        FlagsEnumSetting allowedScrollRarities, FlagsEnumSetting allowedWandRarities,
        FlagsEnumSetting allowedCreators, IntRange levelRange, FloatRange materialCostRange,
        FlagsEnumSetting allowedBooks)
    {
        SpellCollectionFilter newItemCollectionFilter = CreateInstance<SpellCollectionFilter>();
        newItemCollectionFilter.nameSearch = nameSearch;
        newItemCollectionFilter.nameStartsWith = nameStartingLetter;
        newItemCollectionFilter.allowedPotionAllowances = allowedPotionAllowances;
        newItemCollectionFilter.allowedScrollAllowances = allowedScrollAllowances;
        newItemCollectionFilter.allowedWandAllowances = allowedWandAllowances;
        newItemCollectionFilter.allowedPotionRarities = allowedPotionRarities;
        newItemCollectionFilter.allowedScrollRarities = allowedScrollRarities;
        newItemCollectionFilter.allowedWandRarities = allowedWandRarities;
        newItemCollectionFilter.allowedCreators = allowedCreators;
        newItemCollectionFilter.levelRange = levelRange;
        newItemCollectionFilter.materialCostRange = materialCostRange;
        newItemCollectionFilter.allowedBooks = allowedBooks;
        return newItemCollectionFilter;
    }

    public static SpellCollectionFilter CreateBlank(EnumSetting allowances, EnumSetting rarities,
        EnumSetting creatorClasses, EnumSetting books)
    {
        FlagsEnumSetting allowedPotionAllowances = FlagsEnumSetting.CreateBlank(allowances, true);
        FlagsEnumSetting allowedScrollAllowances = FlagsEnumSetting.CreateBlank(allowances, true);
        FlagsEnumSetting allowedWandAllowances = FlagsEnumSetting.CreateBlank(allowances, true);
        FlagsEnumSetting allowedPotionRarities = FlagsEnumSetting.CreateBlank(rarities, true);
        FlagsEnumSetting allowedScrollRarities = FlagsEnumSetting.CreateBlank(rarities, true);
        FlagsEnumSetting allowedWandRarities = FlagsEnumSetting.CreateBlank(rarities, true);
        FlagsEnumSetting allowedCreators = FlagsEnumSetting.CreateBlank(creatorClasses, true);
        IntRange levelRange = IntRange.Create();
        levelRange.SetRange(0, 9);
        FloatRange materialCostRange = FloatRange.Create();
        materialCostRange.SetRange(0f, float.PositiveInfinity);
        FlagsEnumSetting allowedBooks = FlagsEnumSetting.CreateBlank(books, true);
        return Create("", "", allowedPotionAllowances, allowedScrollAllowances, allowedWandAllowances,
            allowedPotionRarities, allowedScrollRarities, allowedWandRarities, allowedCreators,
            levelRange, materialCostRange, allowedBooks);
    }

    public void ApplyFilter (SpellCollection spellCollection)
    {
        spellCollection.doesSpellPassFilter = new bool[spellCollection.spells.Length];

        for(int i = 0; i < spellCollection.spells.Length; i++)
        {
            spellCollection.doesSpellPassFilter[i] = DoesSpellPassFilter(spellCollection.spells[i]);
        }
    }

    protected virtual bool DoesSpellPassFilter (Spell spell)
    {
        if (!string.IsNullOrEmpty(nameSearch) && !spell.name.Contains(nameSearch))
            return false;

        if (!string.IsNullOrEmpty(nameStartsWith) && !spell.name.StartsWith(nameStartsWith))
            return false;

        if (!allowedPotionAllowances[spell.containerAllowances["Potion"]])
            return false;

        if (!allowedScrollAllowances[spell.containerAllowances["Scroll"]])
            return false;

        if (!allowedWandAllowances[spell.containerAllowances["Wand"]])
            return false;

        if (!allowedPotionRarities[spell.containerRarities["Potion"]])
            return false;

        if (!allowedScrollRarities[spell.containerRarities["Scroll"]])
            return false;

        if (!allowedWandRarities[spell.containerRarities["Wand"]])
            return false;

        bool canBeCastByClassAtRightLevel = false;
        for(int i = 0; i < allowedCreators.Length; i++)
        {
            if(allowedCreators[i])
            {
                int creatorLevel = spell.creatorLevels[allowedCreators.enumSetting[i]];
                if (creatorLevel >= levelRange.min && creatorLevel <= levelRange.max)
                {
                    canBeCastByClassAtRightLevel = true;
                    break;
                }
            }
        }

        if (!canBeCastByClassAtRightLevel)
            return false;

        if (spell.materialCost > materialCostRange.max || spell.materialCost < materialCostRange.min)
            return false;

        return allowedBooks[spell.book];
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += GetSafeJsonFromString(nameSearch) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(nameStartsWith) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedPotionAllowances) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedScrollAllowances) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedWandAllowances) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedPotionRarities) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedScrollRarities) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedWandRarities) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedCreators) + jsonSplitter[0];
        jsonString += IntRange.GetJsonString(levelRange) + jsonSplitter[0];
        jsonString += FloatRange.GetJsonString(materialCostRange) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedBooks) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        nameSearch = CreateStringFromSafeJson(splitJsonString[0]);
        nameStartsWith = CreateStringFromSafeJson(splitJsonString[1]);
        allowedPotionAllowances = FlagsEnumSetting.CreateFromJsonString(splitJsonString[2]);
        allowedScrollAllowances = FlagsEnumSetting.CreateFromJsonString(splitJsonString[3]);
        allowedWandAllowances = FlagsEnumSetting.CreateFromJsonString(splitJsonString[4]);
        allowedPotionRarities = FlagsEnumSetting.CreateFromJsonString(splitJsonString[5]);
        allowedScrollRarities = FlagsEnumSetting.CreateFromJsonString(splitJsonString[6]);
        allowedWandRarities = FlagsEnumSetting.CreateFromJsonString(splitJsonString[7]);
        allowedCreators = FlagsEnumSetting.CreateFromJsonString(splitJsonString[8]);
        levelRange = IntRange.CreateFromJsonString(splitJsonString[9]);
        materialCostRange = FloatRange.CreateFromJsonString(splitJsonString[10]);
        allowedBooks = FlagsEnumSetting.CreateFromJsonString(splitJsonString[11]);
    }
}
