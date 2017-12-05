using System;
using UnityEngine;

public abstract class Quality<TQuality> : Item<TQuality>
    where TQuality : Quality<TQuality>
{
    public EnumValue qualityType;
    public int bonusEquivalent;

    public static TQuality Create(string name, int cost, EnumValue rarity, EnumValue book, int page,
        EnumValue qualityType, int bonusEquivalent)
    {
        TQuality newWeaponQuality = CreateInstance<TQuality>();
        newWeaponQuality.name = name;
        newWeaponQuality.rarity = rarity;
        newWeaponQuality.book = book;
        newWeaponQuality.page = page;
        newWeaponQuality.qualityType = qualityType;
        newWeaponQuality.bonusEquivalent = bonusEquivalent;
        return newWeaponQuality;
    }

    public static TQuality CreateBlank(EnumSetting rarities, EnumSetting books, EnumSetting qualityTypes)
    {
        return Create("NAME", 0, EnumValue.CreateBlank(rarities), EnumValue.CreateBlank(books), 999,
            EnumValue.CreateBlank(qualityTypes), 0);
    }

    public static int BonusToCost (int bonus)
    {
        return CreateInstance<TQuality>().BonusToCostInternal(bonus);
    }

    protected abstract int BonusToCostInternal(int bonus);

    public float GetCost()
    {
        if (bonusEquivalent != 0)
        {
            return BonusToCostInternal(bonusEquivalent);
        }

        return cost;
    }

    public float CostToIncrease(TQuality[] oldQualities)
    {
        if (bonusEquivalent == 0)
            return cost;

        int oldBonus = 0;

        for (int i = 0; i < oldQualities.Length; i++)
        {
            oldBonus += oldQualities[i].bonusEquivalent;
        }

        return BonusToCostInternal(oldBonus + bonusEquivalent) - BonusToCostInternal(oldBonus);
    }

    public string BonusEquivalentAsString ()
    {
        if (bonusEquivalent == 0)
            return "";

        return bonusEquivalent.ToString();
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += EnumValue.GetJsonString(rarity) + jsonSplitter[0];
        jsonString += EnumValue.GetJsonString(book) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];
        jsonString += EnumValue.GetJsonString(qualityType) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(bonusEquivalent) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[1]);
        rarity = EnumValue.CreateFromJsonString(splitJsonString[2]);
        book = EnumValue.CreateFromJsonString(splitJsonString[3]);
        page = Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
        qualityType = EnumValue.CreateFromJsonString(splitJsonString[5]);
        bonusEquivalent = Wrapper<int>.CreateFromJsonString(splitJsonString[6]);
    }
}


public class Quality
{
    /*[Flags]
    public enum BonusEquivalent
    {
        NA,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
    }

    [Flags]
    public enum QualityType
    {
        SpecialMaterial,
        EnhancementBonus,
        SpecialAbility,
    }


    public static string GetBonusEquivalentName(BonusEquivalent bonusEquivalent)
    {
        switch (bonusEquivalent)
        {
            case BonusEquivalent.NA:
                return "N/A";
            case BonusEquivalent.One:
                return "+1";
            case BonusEquivalent.Two:
                return "+2";
            case BonusEquivalent.Three:
                return "+3";
            case BonusEquivalent.Four:
                return "+4";
            case BonusEquivalent.Five:
                return "+5";
            case BonusEquivalent.Six:
                return "+6";
            case BonusEquivalent.Seven:
                return "+7";
            case BonusEquivalent.Eight:
                return "+8";
            case BonusEquivalent.Nine:
                return "+9";
            case BonusEquivalent.Ten:
                return "+10";
            default:
                throw new ArgumentOutOfRangeException("bonusEquivalent", bonusEquivalent, null);
        }
    }*/

}
