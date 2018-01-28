using UnityEngine;
using System;
using Random = UnityEngine.Random;

public abstract class Item<TItem> : Jsonable<TItem>
    where TItem : Item<TItem>
{
    public float cost;
    public EnumValue rarity;
    public EnumValue book;
    public int page;

    public static TItem Create(string name, int cost, EnumValue rarity, EnumValue book,
        int page)
    {
        TItem newItem = CreateInstance<TItem>();
        newItem.name = name;
        newItem.cost = cost;
        newItem.rarity = rarity;
        newItem.book = book;
        newItem.page = page;
        return newItem;
    }

    public static TItem CreateBlank(EnumSetting rarities, EnumSetting books)
    {
        return Create("NAME", 0, EnumValue.CreateBlank(rarities), EnumValue.CreateBlank(books), 0);
    }

    public static TItem PickItem (TItem[] items)
    {
        float weightSum = 0f;

        for (int i = 0; i < items.Length; i++)
        {
            weightSum += Campaign.WeightingPerRarity[items[i].rarity];
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < items.Length; i++)
        {
            weightCounter -= Campaign.WeightingPerRarity[items[i].rarity];

            if (weightCounter <= 0f)
            {
                return items[i];
            }
        }

        return null;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += EnumValue.GetJsonString(rarity) + jsonSplitter[0];
        jsonString += EnumValue.GetJsonString(book) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[1]);
        rarity = EnumValue.CreateFromJsonString(splitJsonString[2]);
        book = EnumValue.CreateFromJsonString(splitJsonString[3]);
        page = Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
    }
}


public class Item
{
    /*[Flags]
    public enum Rarity
    {
        Mundane,
        VeryCommon,
        Common,
        Uncommon,
        Unusual,
        Occasional,
        Rare,
        VeryRare,
        Exceptional,
        Mythical,
    }

    public static int RarityTypeCount
    {
        get { return Enum.GetNames(typeof(Rarity)).Length; }
    }*/
}
