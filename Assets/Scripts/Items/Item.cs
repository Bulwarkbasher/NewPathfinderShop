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


    private const int k_RarityEnumValueCount = 10;
    private readonly Keyframe defaultCurveFirstKey = new Keyframe(0f, 1f, -1.5708f, -1.5708f);
    private readonly Keyframe defaultCurveLastKey = new Keyframe(1f, 0f, -0.6f, -0.6f);
    

    public float RarityToWeight (AnimationCurve weightingCurve)
    {
        float time = ((float)((int)rarity)) / k_RarityEnumValueCount;
        return weightingCurve.Evaluate(time);
    }

    // TODO: to draw curve at runtime use int values for rarity and draw a line renderer between each value
    public float RarityToWeight ()
    {
        AnimationCurve defaultWeightCurve = new AnimationCurve
        (
            defaultCurveFirstKey,
            defaultCurveLastKey
        );
        return RarityToWeight(defaultWeightCurve);
    }


    public static Item PickItem (Item[] items)
    {
        float weightSum = 0f;

        for (int i = 0; i < items.Length; i++)
        {
            weightSum += items[i].RarityToWeight();
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < items.Length; i++)
        {
            weightCounter -= items[i].RarityToWeight();

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
