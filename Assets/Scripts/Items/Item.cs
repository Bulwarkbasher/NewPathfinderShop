using UnityEngine;
using System.Collections;

// TODO: update which objects inherit from Item, should be more
public abstract class Item<TChild> : Jsonable<TChild>
    where TChild : Item<TChild>
{
    public int cost;
    public Item.Rarity rarity;
    public int ulimateEquipmentPage;

    public static TChild PickItem (TChild[] items)
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
}


public class Item
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
}
