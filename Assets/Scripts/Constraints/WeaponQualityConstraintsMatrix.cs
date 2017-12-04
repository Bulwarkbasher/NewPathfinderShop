using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponQualityConstraintsMatrix : QualityConstraintsMatrix<WeaponQualityConstraintsMatrix, WeaponCollectionFilter, WeaponCollection, Weapon, WeaponQualityCollectionFilter, WeaponQualityCollection, WeaponQuality>
{
    public bool AddRandomWeaponQuality (ref float budget, SpecificWeapon specificWeapon)
    {
        List<WeaponQuality> qualities = qualityCollection.items.ToList();

        int weaponIndex = -1;
        for (int i = 0; i < itemCollection.Length; i++)
        {
            if (itemCollection[i] == specificWeapon.weapon)
                weaponIndex = i;
        }

        bool hasMaterial = specificWeapon.specialMaterial != null;
        bool needsBonus = !Campaign.UsesAutomaticBonusProgressionRules && specificWeapon.enhancementBonus == null;
        List<WeaponQuality> currentWeaponQualities = new List<WeaponQuality>(specificWeapon.specialAbilities);
        if(!needsBonus)
            currentWeaponQualities.Add(specificWeapon.enhancementBonus);
        WeaponQuality[] currentQualities = currentWeaponQualities.ToArray();

        for (int i = 0; i < qualities.Count; i++)
        {
            string qualityType = qualities[i].qualityType;
            bool isBonus = qualityType == "EnhancementBonus";
            bool isMaterial = qualityType == "SpecialMaterial";
            bool isSpecial = qualityType == "SpecialMaterial";

            if (!matrix[weaponIndex,i] || 
                (isBonus && !needsBonus) || 
                (isSpecial && needsBonus) || 
                (isMaterial && hasMaterial) || 
                qualities[i].CostToIncrease(currentQualities) > budget)
            {
                qualities.RemoveAt(i);
                i--;
            }
        }

        if (qualities.Count == 0)
            return false;

        WeaponQuality chosenQuality = WeaponQuality.PickItem(qualities.ToArray());
        if (chosenQuality.qualityType == "EnhancementBonus")
            specificWeapon.enhancementBonus = chosenQuality;
        if (chosenQuality.qualityType == "SpecialMaterial")
            specificWeapon.specialMaterial = chosenQuality;
        if (chosenQuality.qualityType == "SpecialAbility")
            specificWeapon.AddSpecialAbility(chosenQuality);

        return true;
    }

    public void AssignRandomWeapon (ref float budget, SpecificWeapon specificWeapon)
    {
        specificWeapon.weapon = itemCollection.PickWeapon(ref budget);
    }
}
