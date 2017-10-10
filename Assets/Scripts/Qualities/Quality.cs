using System;
using UnityEngine;
using System.Collections;

public class Quality : Item
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

    static readonly string[] k_JsonSplitter = { "###QualitySplitter###", };

    public static Quality CreateQuality (int cost, bool isStaticBonus, BonusEquivalent bonusEquivalent, int ultimateEquipmentPage)
    {
        Quality newQuality = CreateInstance<Quality>();
        newQuality.cost = cost;
        newQuality.bonusEquivalent = bonusEquivalent;
        newQuality.ulimateEquipmentPage = ultimateEquipmentPage;
        return newQuality;
    }

    public static string GetJsonString(Quality quality)
    {
        string jsonString = Item.GetJsonString (quality);

        jsonString += Wrapper<int>.GetJsonString((int)quality.qualityType) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)quality.bonusEquivalent) + k_JsonSplitter[0];
        
        return jsonString;
    }


    public new static Quality CreateFromJsonString(string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);
        Item qualityBase = Item.CreateFromJsonString(splitJsonString[0]);
        Quality quality = CreateInstance<Quality>();

        quality.name = qualityBase.name;
        quality.cost = qualityBase.cost;
        quality.rarity = qualityBase.rarity;
        quality.ulimateEquipmentPage = qualityBase.ulimateEquipmentPage;
        quality.qualityType = (QualityType)Wrapper<int>.CreateFromJsonString (splitJsonString[1]);
        quality.bonusEquivalent = (BonusEquivalent)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);

        return quality;
    }
}
