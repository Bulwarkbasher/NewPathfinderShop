using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArmourQualityConstraintsMatrix : Saveable<ArmourQualityConstraintsMatrix>
{
    public ArmourCollection armourCollection;
    public ArmourQualityCollection armourQualityCollection;
    public bool[,] matrix = new bool[0, 0]; // [Rows,Columns]

    public static ArmourQualityConstraintsMatrix Create(string name, ArmourCollection armourCollection, ArmourQualityCollection armourQualityCollection)
    {
        ArmourQualityConstraintsMatrix newMatrix = CreateInstance<ArmourQualityConstraintsMatrix>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Spell Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Spell Collection name invalid, name cannot start with Default");

        newMatrix.name = name;
        newMatrix.armourCollection = armourCollection;
        newMatrix.armourQualityCollection = armourQualityCollection;
        newMatrix.matrix = new bool[armourCollection.items.Length, armourQualityCollection.items.Length];

        SaveableHolder.AddSaveable(newMatrix);

        return newMatrix;
    }

    public bool CanArmourUseQuality(Armour armour, ArmourQuality armourQuality)
    {
        int armourIndex = -1;
        for (int i = 0; i < armourCollection.items.Length; i++)
        {
            if (armourCollection.items[i] == armour)
                armourIndex = i;
        }

        if (armourIndex == -1)
            return false;

        int qualityIndex = -1;
        for (int i = 0; i < armourQualityCollection.items.Length; i++)
        {
            if (armourQualityCollection.items[i] == armourQuality)
                qualityIndex = i;
        }

        if (qualityIndex == -1)
            return false;

        return matrix[armourIndex, qualityIndex];
    }

    public bool AddRandomArmourQuality(ref float budget, SpecificArmour specificArmour)
    {
        List<ArmourQuality> qualities = armourQualityCollection.items.ToList();

        int armourIndex = -1;
        for (int i = 0; i < armourCollection.items.Length; i++)
        {
            if (armourCollection.items[i] == specificArmour.armour)
                armourIndex = i;
        }

        bool hasMaterial = specificArmour.specialMaterial != null;
        bool needsBonus = !Campaign.UsesAutomaticBonusProgressionRules && specificArmour.enhancementBonus == null;
        List<ArmourQuality> currentArmourQualities = new List<ArmourQuality>(specificArmour.specialAbilities);
        if (!needsBonus)
            currentArmourQualities.Add(specificArmour.enhancementBonus);
        ArmourQuality[] currentQualities = currentArmourQualities.ToArray();

        for (int i = 0; i < qualities.Count; i++)
        {
            Quality.QualityType qualityType = qualities[i].qualityType;
            bool isBonus = qualityType == Quality.QualityType.EnhancementBonus;
            bool isMaterial = qualityType == Quality.QualityType.SpecialMaterial;
            bool isSpecial = qualityType == Quality.QualityType.SpecialMaterial;

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
        if (chosenQuality.qualityType == Quality.QualityType.EnhancementBonus)
            specificArmour.enhancementBonus = chosenQuality;
        if (chosenQuality.qualityType == Quality.QualityType.SpecialMaterial)
            specificArmour.specialMaterial = chosenQuality;
        if (chosenQuality.qualityType == Quality.QualityType.SpecialAbility)
            specificArmour.AddSpecialAbility(chosenQuality);

        return true;
    }

    public void AssignRandomArmour(ref float budget, SpecificArmour specificArmour)
    {
        specificArmour.armour = armourCollection.PickArmour(ref budget);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += armourCollection.name + jsonSplitter[0];
        jsonString += armourQualityCollection.name + jsonSplitter[0];

        for (int i = 0; i < armourCollection.items.Length; i++)
        {
            for (int j = 0; j < armourQualityCollection.items.Length; j++)
            {
                jsonString += Wrapper<bool>.GetJsonString(matrix[i, j]) + jsonSplitter[0];
            }
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        armourCollection = ArmourCollection.Load(splitJsonString[1]);
        armourQualityCollection = ArmourQualityCollection.Load(splitJsonString[2]);

        int jsonIndex = 3;

        for (int i = 0; i < armourCollection.items.Length; i++)
        {
            for (int j = 0; j < armourQualityCollection.items.Length; j++)
            {
                matrix[i, j] = Wrapper<bool>.CreateFromJsonString(splitJsonString[jsonIndex]);
                jsonIndex++;
            }
        }
    }
}
