using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArmourQualityConstraintsMatrix : QualityConstraintsMatrix<ArmourQualityConstraintsMatrix, ArmourCollectionFilter, ArmourCollection, Armour, ArmourQualityCollectionFilter, ArmourQualityCollection, ArmourQuality>
{
    public bool AddRandomArmourQuality(ref float budget, SpecificArmour specificArmour)
    {
        int armourIndex = -1;
        for (int i = 0; i < itemCollection.Length; i++)
        {
            if (itemCollection[i] == specificArmour.armour)
                armourIndex = i;
        }

        bool hasMaterial = specificArmour.specialMaterial != null;
        bool needsBonus = !Campaign.UsesAutomaticBonusProgressionRules && specificArmour.enhancementBonus == null;
        List<ArmourQuality> currentArmourQualities = new List<ArmourQuality>(specificArmour.specialAbilities);
        if (!needsBonus)
            currentArmourQualities.Add(specificArmour.enhancementBonus);
        ArmourQuality[] currentQualities = currentArmourQualities.ToArray();

        List<ArmourQuality> qualities = qualityCollection.items.ToList();

        for (int i = 0; i < qualities.Count; i++)
        {
            string qualityType = qualities[i].qualityType;
            bool isBonus = qualityType == "EnhancementBonus";
            bool isMaterial = qualityType == "SpecialMaterial";
            bool isSpecial = qualityType == "SpecialMaterial";

            if (!matrix[armourIndex, i] ||
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

        ArmourQuality chosenQuality = ArmourQuality.PickItem(qualities.ToArray());

        budget -= chosenQuality.CostToIncrease(currentQualities);

        if (chosenQuality.qualityType == "EnhancementBonus")
            specificArmour.enhancementBonus = chosenQuality;
        if (chosenQuality.qualityType == "SpecialMaterial")
            specificArmour.specialMaterial = chosenQuality;
        if (chosenQuality.qualityType == "SpecialMaterial")
            specificArmour.AddSpecialAbility(chosenQuality);

        return true;
    }

    public void AssignRandomArmour(ref float budget, SpecificArmour specificArmour)
    {
        specificArmour.armour = itemCollection.PickArmour(ref budget);
    }
}
