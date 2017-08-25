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
        return JsonUtility.ToJson(quality);
    }


    public new static Quality CreateFromJsonString(string jsonString)
    {
        Quality quality = CreateInstance<Quality>();
        JsonUtility.FromJsonOverwrite(jsonString, quality);
        return quality;
    }
}
