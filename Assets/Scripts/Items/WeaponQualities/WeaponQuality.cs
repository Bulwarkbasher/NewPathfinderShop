using UnityEngine;
using System.Collections;

public class WeaponQuality : Quality<WeaponQuality>
{
    public static WeaponQuality Create (string name, int cost, Item.Rarity rarity, SelectedEnumSetting book, int page,
        Quality.QualityType qualityType, Quality.BonusEquivalent bonusEquivalent)
    {
        WeaponQuality newWeaponQuality = CreateInstance<WeaponQuality> ();
        newWeaponQuality.name = name;
        newWeaponQuality.rarity = rarity;
        newWeaponQuality.book = book;
        newWeaponQuality.page = page;
        newWeaponQuality.qualityType = qualityType;
        newWeaponQuality.bonusEquivalent = bonusEquivalent;
        return newWeaponQuality;
    }

    public static WeaponQuality CreateBlank (EnumSetting books)
    {
        return Create ("NAME", 0, Item.Rarity.Mundane, new SelectedEnumSetting(books, 0), 999,
            Quality.QualityType.SpecialMaterial, Quality.BonusEquivalent.NA);
    }

    public static int BonusToCost (Quality.BonusEquivalent bonus)
    {
        return (int)bonus * (int)bonus * 2000;
    }
    
    public float GetCost ()
    {
        if (bonusEquivalent != Quality.BonusEquivalent.NA)
        {
            return BonusToCost(bonusEquivalent);
        }

        return cost;
    }

    public float CostToIncrease (WeaponQuality[] oldQualities)
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
        cost = Wrapper<float>.CreateFromJsonString (splitJsonString[1]);
        rarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);
        page = Wrapper<int>.CreateFromJsonString (splitJsonString[3]);
        qualityType = (Quality.QualityType)Wrapper<int>.CreateFromJsonString (splitJsonString[4]);
        bonusEquivalent = (Quality.BonusEquivalent)Wrapper<int>.CreateFromJsonString (splitJsonString[5]);
    }
}
