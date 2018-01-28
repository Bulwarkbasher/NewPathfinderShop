using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCollectionFilter<TItemCollectionFilter, TItemCollection, TItem> : Jsonable<TItemCollectionFilter>
    where TItemCollectionFilter : ItemCollectionFilter<TItemCollectionFilter, TItemCollection, TItem>
    where TItemCollection : ItemCollection<TItemCollectionFilter, TItemCollection, TItem>
    where TItem : Item<TItem>
{
    public bool showFilteredOnly;
    public string nameSearch;
    public string nameStartsWith;
    public FloatRange costRange;
    public FlagsEnumSetting allowedRarities;
    public FlagsEnumSetting allowedBooks;

    public static TItemCollectionFilter Create(string nameSearch, string nameStartingLetter, FloatRange costRange, FlagsEnumSetting allowedRarities, FlagsEnumSetting allowedBooks)
    {
        TItemCollectionFilter newItemCollectionFilter = CreateInstance<TItemCollectionFilter>();
        newItemCollectionFilter.nameSearch = nameSearch;
        newItemCollectionFilter.nameStartsWith = nameStartingLetter;
        newItemCollectionFilter.costRange = costRange;
        newItemCollectionFilter.allowedRarities = allowedRarities;
        newItemCollectionFilter.allowedBooks = allowedBooks;
        return newItemCollectionFilter;
    }

    public static TItemCollectionFilter CreateBlank(EnumSetting rarities, EnumSetting books)
    {
        FloatRange costRange = FloatRange.Create();
        costRange.SetRange(float.NegativeInfinity, float.PositiveInfinity);
        FlagsEnumSetting allowedRarities = FlagsEnumSetting.CreateBlank(rarities, true);
        FlagsEnumSetting allowedBooks = FlagsEnumSetting.CreateBlank(books, true);
        return Create("", "", costRange, allowedRarities, allowedBooks);
    }

    public void ApplyFilter(TItemCollection itemCollection)
    {
        itemCollection.doesItemPassFilter = new bool[itemCollection.Length];

        for(int i = 0; i < itemCollection.Length; i++)
        {
            itemCollection.doesItemPassFilter[i] = DoesItemPassFilter(itemCollection[i]);
        }
    }

    protected virtual bool DoesItemPassFilter(TItem item)
    {
        if (!string.IsNullOrEmpty(nameSearch) && !item.name.Contains(nameSearch))
            return false;

        if (!string.IsNullOrEmpty(nameStartsWith) && !item.name.StartsWith(nameStartsWith))
            return false;

        if (item.cost > costRange.max || item.cost < costRange.min)
            return false;

        if (!allowedRarities[item.rarity])
            return false;

        return allowedBooks[item.book];
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += GetSafeJsonFromString(nameSearch) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(nameStartsWith) + jsonSplitter[0];
        jsonString += FloatRange.GetJsonString(costRange) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedRarities) + jsonSplitter[0];
        jsonString += FlagsEnumSetting.GetJsonString(allowedBooks) + jsonString[0];

        return jsonString;
    }


    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        nameSearch = CreateStringFromSafeJson(splitJsonString[0]);
        nameStartsWith = CreateStringFromSafeJson(splitJsonString[1]);
        costRange = FloatRange.CreateFromJsonString(splitJsonString[2]);
        allowedRarities = FlagsEnumSetting.CreateFromJsonString(splitJsonString[3]);
        allowedBooks = FlagsEnumSetting.CreateFromJsonString(splitJsonString[4]);
    }
}