using System;
using UnityEngine;

public abstract class Quality<TChild> : Item<TChild>
    where TChild : Quality<TChild>
{
    public Quality.QualityType qualityType;
    public Quality.BonusEquivalent bonusEquivalent;
}


public class Quality
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
    }

}
