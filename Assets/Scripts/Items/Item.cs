using UnityEngine;
using System.Collections;

// TODO: update which objects inherit from Item, should be more
public class Item : ScriptableObject
{
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

    public int cost;
    public Rarity rarity;
    public int ulimateEquipmentPage;

    static readonly string[] k_JsonSplitter = { "###ItemSplitter###" };

    public static Item PickItem (Item[] items)
    {
        float weightSum = 0f;

        for (int i = 0; i < items.Length; i++)
        {
            weightSum += Campaign.RarityWeighting.RarityToWeight(items[i].rarity);
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < items.Length; i++)
        {
            weightCounter -= Campaign.RarityWeighting.RarityToWeight(items[i].rarity);

            if (weightCounter <= 0f)
            {
                return items[i];
            }
        }

        return null;
    }

    public static string GetJsonString (Item item)
    {
        string jsonString = "";

        jsonString += item.name + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString (item.cost) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString ((int)item.rarity) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString (item.ulimateEquipmentPage) + k_JsonSplitter[0];

        return jsonString;
    }

    public static Item CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, System.StringSplitOptions.RemoveEmptyEntries);

        Item item = CreateInstance<Item>();

        item.name = splitJsonString[0];
        item.cost = Wrapper<int>.CreateFromJsonString (splitJsonString[1]);
        item.rarity = (Rarity)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);
        item.ulimateEquipmentPage = Wrapper<int>.CreateFromJsonString (splitJsonString[3]);

        return item;
    }
}
