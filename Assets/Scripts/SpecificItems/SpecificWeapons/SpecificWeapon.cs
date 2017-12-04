using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecificWeapon : SpecificItem<SpecificWeapon>
{
    public Weapon weapon;
    public WeaponQuality enhancementBonus;
    public WeaponQuality specialMaterial;
    public WeaponQuality[] specialAbilities = new WeaponQuality[0];

    public static SpecificWeapon CreateRandom (JsonableSelectedEnumSetting powerLevel, WeaponQualityConstraintsMatrix matrix, FloatRange budgetRange)
    {
        SpecificWeapon newSpecificWeapon = CreateInstance<SpecificWeapon>();
        
        newSpecificWeapon.powerLevel = powerLevel;
        float budget = budgetRange.Random();
        matrix.AssignRandomWeapon(ref budget, newSpecificWeapon);

        while(budget > 0)
        {
            if (!matrix.AddRandomWeaponQuality(ref budget, newSpecificWeapon))
                break;
        }

        if (newSpecificWeapon.specialMaterial == null)
            newSpecificWeapon.specialMaterial = WeaponQuality.CreateBlank(matrix.itemCollection.rarities, matrix.itemCollection.books);
        if (newSpecificWeapon.enhancementBonus == null)
            newSpecificWeapon.enhancementBonus = WeaponQuality.CreateBlank(matrix.itemCollection.rarities, matrix.itemCollection.books);

        newSpecificWeapon.CalculateCost();
        newSpecificWeapon.CalculateName();

        return newSpecificWeapon;
    }
        
    public void SwapWeapon (Weapon newWeapon)
    {
        weapon = newWeapon;
        CalculateCost();
        CalculateName();
    }
    
    public void SwapEnhancement (WeaponQuality newEnhancement)
    {
        enhancementBonus = newEnhancement;
        CalculateCost();
        CalculateName();
    }
    
    public void SwapSpecialMaterial (WeaponQuality newMaterial)
    {
        specialMaterial = newMaterial;
        CalculateCost();
        CalculateName();
    }
    
    public void SwapSpecialAbility (int oldAbilityIndex, WeaponQuality newSpecialAbility)
    {
        specialAbilities[oldAbilityIndex] = newSpecialAbility;
        CalculateCost();
        CalculateName();
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
        CalculateName();
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
        CalculateName();
    }
    
    private void CalculateCost ()
    {
        cost = 0;
        cost += weapon.cost;
        cost += specialMaterial.cost;

        int bonus = (int)enhancementBonus.bonusEquivalent;

        for(int i = 0; i < specialAbilities.Length; i++)
        {
            if (specialAbilities[i].bonusEquivalent == 0)
                cost += specialAbilities[i].cost;
            else
                bonus += (int)specialAbilities[i].bonusEquivalent;
        }
        cost += WeaponQuality.BonusToCost(bonus);
    }

    void CalculateName ()
    {
        name = "";

        if(enhancementBonus.name != "NAME")
            name += enhancementBonus.BonusEquivalentAsString() + " ";

        for (int i = 0; i < specialAbilities.Length; i++)
        {
            name += specialAbilities[i].name + " ";
        }

        if (specialMaterial.name != "NAME")
            name += specialMaterial.name + " ";

        name += weapon.name;
    }
    
    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += JsonableSelectedEnumSetting.GetJsonString(powerLevel) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
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
        name = splitJsonString[0];
        powerLevel = JsonableSelectedEnumSetting.CreateFromJsonString(splitJsonString[1]);
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        notes = CreateStringFromSafeJson(splitJsonString[3]);
        weapon = Weapon.CreateFromJsonString (splitJsonString[4]);
        enhancementBonus = WeaponQuality.CreateFromJsonString (splitJsonString[5]);
        specialMaterial = WeaponQuality.CreateFromJsonString (splitJsonString[6]);

        specialAbilities = new WeaponQuality[splitJsonString.Length - 7];
        for (int i = 0; i < specialAbilities.Length; i++)
        {
            specialAbilities[i] = WeaponQuality.CreateFromJsonString (splitJsonString[i + 8]);
        }
    }
}
