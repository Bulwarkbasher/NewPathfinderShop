using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecificWeapon : SpecificItem<SpecificWeapon>
{
    public Weapon weapon;
    public WeaponQuality enhancementBonus;
    public WeaponQuality specialMaterial;
    public WeaponQuality[] specialAbilities;

    private string m_Description;
    
    public static SpecificWeapon CreateRandom (PowerLevel powerLevel, int budget, WeaponCollection availableWeapons, WeaponQualityCollection availableQualities)
    {
        int originalBudget = budget;
        SpecificWeapon newSpecificWeapon = CreateInstance<SpecificWeapon>();

        newSpecificWeapon.powerLevel = powerLevel;
        newSpecificWeapon.weapon = availableWeapons.PickWeapon(ref budget);

        if (newSpecificWeapon.weapon == null)
            return null;

        bool weaponHasMaterial = false;
        bool weaponHasBonus = Campaign.UsesAutomaticBonusProgressionRules;

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
        m_Description = "";

        if(enhancementBonus)
            m_Description += Quality.GetBonusEquivalentName(enhancementBonus.bonusEquivalent) + " ";
        
        for (int i = 0; i < specialAbilities.Length; i++)
        {
            m_Description += specialAbilities[i].name + " ";
        }

        if (specialMaterial)
            m_Description += specialMaterial.name + " ";

        m_Description += weapon.name;

        return m_Description;
    }
    
    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += Weapon.GetJsonString (weapon) + jsonSplitter[0];
        jsonString += WeaponQuality.GetJsonString (enhancementBonus) + jsonSplitter[0];
        jsonString += WeaponQuality.GetJsonString(specialMaterial) + jsonSplitter[0];

        for (int i = 0; i < specialAbilities.Length; i++)
        {
            jsonString += WeaponQuality.GetJsonString(specialAbilities[i]) + jsonSplitter[0];
        }

        return jsonString;
    }
    
    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        weapon = Weapon.CreateFromJsonString (splitJsonString[0]);
        enhancementBonus = WeaponQuality.CreateFromJsonString (splitJsonString[1]);
        specialMaterial = WeaponQuality.CreateFromJsonString (splitJsonString[2]);

        specialAbilities = new WeaponQuality[splitJsonString.Length - 3];
        for (int i = 0; i < specialAbilities.Length; i++)
        {
            specialAbilities[i] = WeaponQuality.CreateFromJsonString (splitJsonString[i + 3]);
        }

        CalculateCost ();
    }
}
