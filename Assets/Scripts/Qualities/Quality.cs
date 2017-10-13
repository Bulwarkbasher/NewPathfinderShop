using System;
using UnityEngine;
using System.Collections;

public class Quality : Item<Quality>
{
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


    public enum QualityType
    {
        SpecialMaterial,
        EnhancementBonus,
        SpecialAbility,
    }


    public static readonly string[] BonusEquivalentNames = { "N/A", "+1", "+2", "+3", "+4", "+5", "+6", "+7", "+8", "+9", "+10" };
    public static Quality blankQuality;


    public QualityType qualityType;
    public BonusEquivalent bonusEquivalent;

    public static Quality CreateQuality (int cost, bool isStaticBonus, BonusEquivalent bonusEquivalent, int ultimateEquipmentPage)
    {
        Quality newQuality = CreateInstance<Quality>();
        newQuality.cost = cost;
        newQuality.bonusEquivalent = bonusEquivalent;
        newQuality.ulimateEquipmentPage = ultimateEquipmentPage;
        return newQuality;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)rarity) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(ulimateEquipmentPage) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)qualityType) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)bonusEquivalent) + jsonSplitter[0];
        
        return jsonString;
    }


    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        cost = Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
        rarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        ulimateEquipmentPage = Wrapper<int>.CreateFromJsonString(splitJsonString[3]);
        qualityType = (QualityType)Wrapper<int>.CreateFromJsonString (splitJsonString[4]);
        bonusEquivalent = (BonusEquivalent)Wrapper<int>.CreateFromJsonString(splitJsonString[5]);
    }
}
