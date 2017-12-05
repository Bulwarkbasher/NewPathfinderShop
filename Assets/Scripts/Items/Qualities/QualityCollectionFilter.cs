using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QualityCollectionFilter<TQualityCollectionFilter, TQualityCollection, TQuality> : ItemCollectionFilter<TQualityCollectionFilter, TQualityCollection, TQuality>
    where TQualityCollectionFilter : QualityCollectionFilter<TQualityCollectionFilter, TQualityCollection, TQuality>
    where TQualityCollection : QualityCollection<TQualityCollectionFilter, TQualityCollection, TQuality>
    where TQuality : Quality<TQuality>
{
    public FlagsEnumSetting allowedQualityTypes;
    public IntRange bonusEquivalencyRange;

    public static TQualityCollectionFilter Create (string nameSearch, string nameStartingLetter,
        FloatRange costRange, FlagsEnumSetting allowedRarities, FlagsEnumSetting allowedBooks,
        FlagsEnumSetting allowedQualityTypes, IntRange bonusEquivalencyRange)
    {
        TQualityCollectionFilter newItemCollectionFilter = CreateInstance<TQualityCollectionFilter>();
        newItemCollectionFilter.nameSearch = nameSearch;
        newItemCollectionFilter.nameStartsWith = nameStartingLetter;
        newItemCollectionFilter.costRange = costRange;
        newItemCollectionFilter.allowedRarities = allowedRarities;
        newItemCollectionFilter.allowedBooks = allowedBooks;
        newItemCollectionFilter.allowedQualityTypes = allowedQualityTypes;
        newItemCollectionFilter.bonusEquivalencyRange = bonusEquivalencyRange;
        return newItemCollectionFilter;
    }

    public static TQualityCollectionFilter CreateBlank (EnumSetting rarities, EnumSetting books, EnumSetting qualityTypes)
    {
        FloatRange costRange = FloatRange.Create();
        costRange.SetRange(float.NegativeInfinity, float.PositiveInfinity);
        FlagsEnumSetting allowedRarities = FlagsEnumSetting.CreateBlank(rarities, true);
        FlagsEnumSetting allowedBooks = FlagsEnumSetting.CreateBlank(books, true);
        FlagsEnumSetting allowedQualityTypes = FlagsEnumSetting.CreateBlank(qualityTypes, true);
        IntRange bonusEquivalencyRange = IntRange.Create();
        bonusEquivalencyRange.SetRange(int.MinValue, int.MaxValue);
        return Create("", "", costRange, allowedRarities, allowedBooks, allowedQualityTypes, bonusEquivalencyRange);
    }

    protected override bool DoesItemPassFilter(TQuality quality)
    {
        if (!string.IsNullOrEmpty(nameSearch) && !quality.name.Contains(nameSearch))
            return false;

        if (!string.IsNullOrEmpty(nameStartsWith) && !quality.name.StartsWith(nameStartsWith))
            return false;

        if (quality.cost > costRange.max || quality.cost < costRange.min)
            return false;

        if (allowedRarities[quality.rarity])
            return false;

        if (allowedQualityTypes[quality.qualityType])
            return false;

        if (quality.bonusEquivalent > bonusEquivalencyRange.max || quality.bonusEquivalent < bonusEquivalencyRange.min)
            return false;

        return allowedBooks[quality.book];
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += GetSafeJsonFromString(nameSearch) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(nameStartsWith) + jsonSplitter[0];
        jsonString += FloatRange.GetJsonString(costRange) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedRarities) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedBooks) + jsonString[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedQualityTypes) + jsonSplitter[0];
        jsonString += IntRange.GetJsonString(bonusEquivalencyRange) + jsonSplitter[0];

        return jsonString;
    }


    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        nameSearch = CreateStringFromSafeJson(splitJsonString[0]);
        nameStartsWith = CreateStringFromSafeJson(splitJsonString[1]);
        costRange = FloatRange.CreateFromJsonString(splitJsonString[2]);
        allowedRarities = FlagsEnumSetting.CreateFromJsonString(splitJsonString[3]);
        allowedBooks = FlagsEnumSetting.CreateFromJsonString(splitJsonString[4]);
        allowedQualityTypes = FlagsEnumSetting.CreateFromJsonString(splitJsonString[5]);
        bonusEquivalencyRange = IntRange.CreateFromJsonString(splitJsonString[6]);
    }
}
