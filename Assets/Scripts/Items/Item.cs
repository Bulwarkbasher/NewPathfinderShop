using UnityEngine;

public abstract class Item<TChild> : Jsonable<TChild>
    where TChild : Item<TChild>
{
    public float cost;
    public Item.Rarity rarity;
    public EnumSettingIndex book;
    public int page;

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
