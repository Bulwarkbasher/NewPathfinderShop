using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificArmour : SpecificItem<SpecificArmour>
{
    public Armour armour;
    public ArmourQuality enhancementBonus;
    public ArmourQuality specialMaterial;
    public ArmourQuality[] specialAbilities = new ArmourQuality[0];

    public static SpecificArmour CreateRandom(SpecificItem.PowerLevel powerLevel, ArmourQualityConstraintsMatrix matrix, FloatRange budgetRange)
    {
        SpecificArmour newSpecificArmour = CreateInstance<SpecificArmour>();

        newSpecificArmour.powerLevel = powerLevel;
        float budget = budgetRange.Random();
        matrix.AssignRandomArmour(ref budget, newSpecificArmour);

        while (budget > 0)
        {
            if (!matrix.AddRandomArmourQuality(ref budget, newSpecificArmour))
                break;
        }

        if (newSpecificArmour.specialMaterial == null)
            newSpecificArmour.specialMaterial = ArmourQuality.CreateBlank(matrix.armourCollection.books);
        if (newSpecificArmour.enhancementBonus == null)
            newSpecificArmour.enhancementBonus = ArmourQuality.CreateBlank(matrix.armourCollection.books);

        newSpecificArmour.CalculateCost();
        newSpecificArmour.CalculateName();

        return newSpecificArmour;
    }

    public void SwapArmour(Armour newArmour)
    {
        armour = newArmour;
        CalculateCost();
        CalculateName();
    }

    public void SwapEnhancement(ArmourQuality newEnhancement)
    {
        enhancementBonus = newEnhancement;
        CalculateCost();
        CalculateName();
    }

    public void SwapSpecialMaterial(ArmourQuality newMaterial)
    {
        specialMaterial = newMaterial;
        CalculateCost();
        CalculateName();
    }

    public void SwapSpecialAbility(int oldAbilityIndex, ArmourQuality newSpecialAbility)
    {
        specialAbilities[oldAbilityIndex] = newSpecialAbility;
        CalculateCost();
        CalculateName();
    }

    public void AddSpecialAbility(ArmourQuality newSpecialAbility)
    {
        ArmourQuality[] newSpecialAbilities = new ArmourQuality[specialAbilities.Length + 1];
        for (int i = 0; i < specialAbilities.Length; i++)
        {
            newSpecialAbilities[i] = specialAbilities[i];
        }
        newSpecialAbilities[specialAbilities.Length] = newSpecialAbility;
        specialAbilities = newSpecialAbilities;
        CalculateCost();
        CalculateName();
    }

    public void RemoveSpecialAbility(int removeAt)
    {
        ArmourQuality[] newSpecialAbilities = new ArmourQuality[specialAbilities.Length - 1];
        int oldIndex = 0;
        int newIndex = 0;
        for (; oldIndex < specialAbilities.Length; oldIndex++, newIndex++)
        {
            if (oldIndex == removeAt)
                oldIndex++;
            newSpecialAbilities[newIndex] = specialAbilities[oldIndex];
        }
        specialAbilities = newSpecialAbilities;
        CalculateCost();
        CalculateName();
    }

    private void CalculateCost()
    {
        cost = 0;
        cost += armour.cost;
        cost += specialMaterial.cost;

        int bonus = (int)enhancementBonus.bonusEquivalent;

        for (int i = 0; i < specialAbilities.Length; i++)
        {
            if (specialAbilities[i].bonusEquivalent == Quality.BonusEquivalent.NA)
                cost += specialAbilities[i].cost;
            else
                bonus += (int)specialAbilities[i].bonusEquivalent;
        }
        cost += ArmourQuality.BonusToCost((Quality.BonusEquivalent)bonus);
    }

    void CalculateName()
    {
        name = "";

        if (enhancementBonus.name != "NAME")
            name += Quality.GetBonusEquivalentName(enhancementBonus.bonusEquivalent) + " ";

        for (int i = 0; i < specialAbilities.Length; i++)
        {
            name += specialAbilities[i].name + " ";
        }

        if (specialMaterial.name != "NAME")
            name += specialMaterial.name + " ";

        name += armour.name;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)powerLevel) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += Armour.GetJsonString(armour) + jsonSplitter[0];
        jsonString += ArmourQuality.GetJsonString(enhancementBonus) + jsonSplitter[0];
        jsonString += ArmourQuality.GetJsonString(specialMaterial) + jsonSplitter[0];

        for (int i = 0; i < specialAbilities.Length; i++)
        {
            jsonString += ArmourQuality.GetJsonString(specialAbilities[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        powerLevel = (SpecificItem.PowerLevel)Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        notes = CreateStringFromSafeJson(splitJsonString[3]);
        armour = Armour.CreateFromJsonString(splitJsonString[4]);
        enhancementBonus = ArmourQuality.CreateFromJsonString(splitJsonString[5]);
        specialMaterial = ArmourQuality.CreateFromJsonString(splitJsonString[6]);

        specialAbilities = new ArmourQuality[splitJsonString.Length - 7];
        for (int i = 0; i < specialAbilities.Length; i++)
        {
            specialAbilities[i] = ArmourQuality.CreateFromJsonString(splitJsonString[i + 7]);
        }
    }
}
