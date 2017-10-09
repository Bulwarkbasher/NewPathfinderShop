using UnityEngine;
using System.Collections;

// TODO: change description to use name instead
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

    public string description;
    public int cost;
    public Rarity rarity;
    public int ulimateEquipmentPage;

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
        return JsonUtility.ToJson(item);
    }

    public static Item CreateFromJsonString (string jsonString)
    {
        Item item = CreateInstance<Item>();
        JsonUtility.FromJsonOverwrite(jsonString, item);
        return item;
    }
}
