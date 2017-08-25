using UnityEngine;
using System.Collections;

public class WeaponQuality : Quality
{
    // Weapons this is applied to must have at least one of these
    public Weapon.WeaponType anyOfWeaponType;
    public Weapon.Handedness anyOfHandedness;
    public Weapon.DamageType anyOfDamageType;
    public Weapon.Special anyOfSpecial;
    public Weapon.AttackConstraints anyOfAttackConstraints;
    public Weapon.MaterialConstraints anyOfMaterialConstraints;
    public Weapon.SpecialConstraints anyOfSpecialConstraints;

    //Weapons this is applied to must have all of these
    public Weapon.WeaponType allOfWeaponType;
    public Weapon.Handedness allOfHandedness;
    public Weapon.DamageType allOfDamageType;
    public Weapon.Special allOfSpecial;
    public Weapon.AttackConstraints allOfAttackConstraints;
    public Weapon.MaterialConstraints allOfMaterialConstraints;
    public Weapon.SpecialConstraints allOfSpecialConstraints;


    public static int BonusToCost (BonusEquivalent bonus)
    {
        return (int)bonus * (int)bonus * 2000;
    }


    public int GetCost ()
    {
        if (cost == -1)
        {
            return BonusToCost(bonusEquivalent);
        }

        return cost;
    }


    public int CostToIncrease (WeaponQuality[] oldQualities)
    {
        if (bonusEquivalent == BonusEquivalent.NA)
            return cost;

        BonusEquivalent oldBonus = BonusEquivalent.NA;

        for (int i = 0; i < oldQualities.Length; i++)
        {
            oldBonus += (int)oldQualities[i].bonusEquivalent;
        }

        int oldCost = BonusToCost(oldBonus);
        BonusEquivalent newBonus = (BonusEquivalent)((int)oldBonus + (int)bonusEquivalent);
        int newCost = BonusToCost(newBonus);

        return newCost - oldCost;
    }


    public static string GetJsonString(WeaponQuality weaponQuality)
    {
        return JsonUtility.ToJson(weaponQuality);
    }


    public new static WeaponQuality CreateFromJsonString(string jsonString)
    {
        WeaponQuality weaponQuality = CreateInstance<WeaponQuality>();
        JsonUtility.FromJsonOverwrite(jsonString, weaponQuality);
        return weaponQuality;
    }
}
