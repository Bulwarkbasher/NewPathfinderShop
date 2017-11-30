using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourQuality : Quality<ArmourQuality>
{
    public static ArmourQuality Create(string name, int cost, Item.Rarity rarity, SelectedEnumSetting book, int page,
        Quality.QualityType qualityType, Quality.BonusEquivalent bonusEquivalent)
    {
        ArmourQuality newArmourQuality = CreateInstance<ArmourQuality>();
        newArmourQuality.name = name;
        newArmourQuality.rarity = rarity;
        newArmourQuality.book = book;
        newArmourQuality.page = page;
        newArmourQuality.qualityType = qualityType;
        newArmourQuality.bonusEquivalent = bonusEquivalent;
        return newArmourQuality;
    }

    public static ArmourQuality CreateBlank(EnumSetting books)
    {
        return Create("NAME", 0, Item.Rarity.Mundane, SelectedEnumSetting.CreateBlank(books), 999,
            Quality.QualityType.SpecialMaterial, Quality.BonusEquivalent.NA);
    }

    public static int BonusToCost(Quality.BonusEquivalent bonus)
    {
        return (int)bonus * (int)bonus * 1000;
    }

    public float GetCost()
    {
        if (bonusEquivalent != Quality.BonusEquivalent.NA)
        {
            return BonusToCost(bonusEquivalent);
        }

        return cost;
    }

    public float CostToIncrease(ArmourQuality[] oldQualities)
    {
        if (bonusEquivalent == Quality.BonusEquivalent.NA)
            return cost;

        Quality.BonusEquivalent oldBonus = Quality.BonusEquivalent.NA;

        for (int i = 0; i < oldQualities.Length; i++)
        {
            oldBonus += (int)oldQualities[i].bonusEquivalent;
        }

        int oldCost = BonusToCost(oldBonus);
        Quality.BonusEquivalent newBonus = (Quality.BonusEquivalent)((int)oldBonus + (int)bonusEquivalent);
        int newCost = BonusToCost(newBonus);

        return newCost - oldCost;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)rarity) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)qualityType) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)bonusEquivalent) + jsonString[0];

        return jsonString;
    }


    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[1]);
        rarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        page = Wrapper<int>.CreateFromJsonString(splitJsonString[3]);
        qualityType = (Quality.QualityType)Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
        bonusEquivalent = (Quality.BonusEquivalent)Wrapper<int>.CreateFromJsonString(splitJsonString[5]);
    }
}
