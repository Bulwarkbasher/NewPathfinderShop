using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecificWeapon : SpecificItem
{
    public Weapon weapon;
    public WeaponQuality enhancementBonus;
    public WeaponQuality specialMaterial;
    public WeaponQuality[] specialAbilities;
    public int cost;


    private string description;


    private static readonly string[] k_JsonSplitter = 
    {
        "###SpecWeapSplitter###",
    };

    
    public static SpecificWeapon CreateRandom (PowerLevel powerLevel, int budget, WeaponCollection availableWeapons, WeaponQualitiesCollection availableQualities)
    {
        int originalBudget = budget;
        SpecificWeapon newSpecificWeapon = CreateInstance<SpecificWeapon>();

        newSpecificWeapon.powerLevel = powerLevel;
        newSpecificWeapon.weapon = availableWeapons.PickWeapon(ref budget);

        if (newSpecificWeapon.weapon == null)
            return null;

        bool weaponHasMaterial = false;
        bool weaponHasBonus = CampaignCollection.Current.UsesAutomaticBonusProgressionRules;

        List<WeaponQuality> weaponSpecialAbilities = new List<WeaponQuality>();

        while (budget > 0)
        {
            List<WeaponQuality> bonusEquivalentQualities = new List<WeaponQuality>(weaponSpecialAbilities);
            if (newSpecificWeapon.enhancementBonus)
                bonusEquivalentQualities.Add(newSpecificWeapon.enhancementBonus);

            WeaponQuality newQuality = availableQualities.PickWeaponQuality(newSpecificWeapon.weapon, budget, bonusEquivalentQualities.ToArray(), !weaponHasMaterial, !weaponHasBonus, weaponHasBonus);

            if (newQuality == null)
                break;

            if (newQuality.qualityType == Quality.QualityType.SpecialMaterial)
            {
                weaponHasMaterial = true;
                newSpecificWeapon.specialMaterial = newQuality;
            }
            else if (newQuality.qualityType == Quality.QualityType.EnhancementBonus)
            {
                weaponHasBonus = true;
                newSpecificWeapon.enhancementBonus = newQuality;
            }
            else
            {
                weaponSpecialAbilities.Add(newQuality);
            }            

            int costToAdd = newQuality.CostToIncrease(bonusEquivalentQualities.ToArray());

            budget -= costToAdd;
        }

        newSpecificWeapon.specialAbilities = weaponSpecialAbilities.ToArray();

        if (newSpecificWeapon.specialMaterial == null)
        {
            WeaponQuality blankSpecialMaterial = CreateInstance<WeaponQuality>();
            newSpecificWeapon.specialMaterial = blankSpecialMaterial;
        }
        if (newSpecificWeapon.enhancementBonus == null)
        {
            WeaponQuality blankSpecialMaterial = CreateInstance<WeaponQuality>();
            newSpecificWeapon.specialMaterial = blankSpecialMaterial;
        }

        newSpecificWeapon.cost = originalBudget - budget;

        return newSpecificWeapon;
    }


    public void SwapWeapon (Weapon newWeapon)
    {
        weapon = newWeapon;
        CalculateCost();
    }


    public void SwapEnhancement (WeaponQuality newEnhancement)
    {
        enhancementBonus = newEnhancement;
        CalculateCost();
    }


    public void SwapSpecialMaterial (WeaponQuality newMaterial)
    {
        specialMaterial = newMaterial;
        CalculateCost();
    }


    public void SwapSpecialAbility (int oldAbilityIndex, WeaponQuality newSpecialAbility)
    {
        specialAbilities[oldAbilityIndex] = newSpecialAbility;
        CalculateCost();
    }


    public void AddSpecialAbility (WeaponQuality newSpecialAbility)
    {
        WeaponQuality[] newSpecialAbilities = new WeaponQuality[specialAbilities.Length + 1];
        for(int i = 0; i < specialAbilities.Length; i++)
        {
            newSpecialAbilities[i] = specialAbilities[i];
        }
        newSpecialAbilities[specialAbilities.Length] = newSpecialAbility;
        specialAbilities = newSpecialAbilities;
        CalculateCost();
    }


    public void RemoveSpecialAbility (int removeAt)
    {
        WeaponQuality[] newSpecialAbilities = new WeaponQuality[specialAbilities.Length - 1];
        int oldIndex = 0;
        int newIndex = 0;
        for(; oldIndex < specialAbilities.Length; oldIndex++, newIndex++)
        {
            if(oldIndex == removeAt)
                oldIndex++;
            newSpecialAbilities[newIndex] = specialAbilities[oldIndex];
        }
        specialAbilities = newSpecialAbilities;
        CalculateCost();
    }


    private void CalculateCost ()
    {
        cost = 0;
        cost += weapon.cost;
        cost += specialMaterial.cost;

        int bonus = (int)enhancementBonus.bonusEquivalent;

        for(int i = 0; i < specialAbilities.Length; i++)
        {
            if (specialAbilities[i].bonusEquivalent == Quality.BonusEquivalent.NA)
                cost += specialAbilities[i].cost;
            else
                bonus += (int)specialAbilities[i].bonusEquivalent;
        }
        cost += WeaponQuality.BonusToCost((Quality.BonusEquivalent)bonus);
    }


    public override string ToString()
    {
        description = "";

        if(enhancementBonus)
            description += enhancementBonus.description + " ";
        
        for (int i = 0; i < specialAbilities.Length; i++)
        {
            description += specialAbilities[i].description + " ";
        }

        if (specialMaterial)
            description += specialMaterial.description + " ";

        description += weapon.description;

        return description;
    }


    public static string GetJsonString (SpecificWeapon specificWeapon)
    {
        string weaponString = Weapon.GetJsonString(specificWeapon.weapon);
        string enhancementBonusString = WeaponQuality.GetJsonString(specificWeapon.enhancementBonus);
        string specialMaterialString = WeaponQuality.GetJsonString(specificWeapon.specialMaterial);
        
        string[] specialAbilityStrings = new string[specificWeapon.specialAbilities.Length];
        for(int i = 0; i < specialAbilityStrings.Length; i++)
        {
            specialAbilityStrings[i] = WeaponQuality.GetJsonString(specificWeapon.specialAbilities[i]);
        }

        string specificWeaponString = weaponString + k_JsonSplitter[0] + enhancementBonusString + k_JsonSplitter[0] + specialMaterialString + k_JsonSplitter[0];

        for(int i = 0; i < specialAbilityStrings.Length; i++)
        {
            specificWeaponString += k_JsonSplitter[0] + specialAbilityStrings[i];
        }

        return specificWeaponString;
    }


    public static SpecificWeapon CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, System.StringSplitOptions.RemoveEmptyEntries);

        string weaponString = splitJsonString[0];
        string enhancementBonusString = splitJsonString[1];
        string specialMaterialString = splitJsonString[2];

        string[] specialAbilityStrings = new string[splitJsonString.Length - 3];
        for(int i = 0; i < specialAbilityStrings.Length; i++)
        {
            specialAbilityStrings[i] = splitJsonString[i + 3];
        }

        SpecificWeapon specificWeapon = CreateInstance<SpecificWeapon>();
        specificWeapon.weapon = Weapon.CreateFromJsonString(weaponString);
        specificWeapon.enhancementBonus = WeaponQuality.CreateFromJsonString(enhancementBonusString);
        specificWeapon.specialMaterial = WeaponQuality.CreateFromJsonString(specialMaterialString);

        specificWeapon.specialAbilities = new WeaponQuality[specialAbilityStrings.Length];
        for(int i = 0; i < specificWeapon.specialAbilities.Length; i++)
        {
            specificWeapon.specialAbilities[i] = WeaponQuality.CreateFromJsonString(specialAbilityStrings[i]);
        }

        specificWeapon.CalculateCost();

        return specificWeapon;
    }
}
