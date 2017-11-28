using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponQualityConstraintsMatrix : Saveable<WeaponQualityConstraintsMatrix>
{
    public WeaponCollection weaponCollection;
    public WeaponQualityCollection weaponQualityCollection;
    public bool[,] matrix = new bool[0, 0]; // [Rows,Columns]

    public static WeaponQualityConstraintsMatrix Create (string name, WeaponCollection weaponCollection, WeaponQualityCollection weaponQualityCollection)
    {
        WeaponQualityConstraintsMatrix newMatrix = CreateInstance<WeaponQualityConstraintsMatrix>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Spell Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Spell Collection name invalid, name cannot start with Default");

        newMatrix.name = name;
        newMatrix.weaponCollection = weaponCollection;
        newMatrix.weaponQualityCollection = weaponQualityCollection;
        newMatrix.matrix = new bool[weaponCollection.items.Length, weaponQualityCollection.items.Length];

        SaveableHolder.AddSaveable(newMatrix);

        return newMatrix;
    }

    public bool CanWeaponUseQuality (Weapon weapon, WeaponQuality weaponQuality)
    {
        int weaponIndex = -1;
        for(int i = 0; i < weaponCollection.items.Length; i ++)
        {
            if (weaponCollection.items[i] == weapon)
                weaponIndex = i;
        }

        if (weaponIndex == -1)
            return false;

        int qualityIndex = -1;
        for (int i = 0; i < weaponQualityCollection.items.Length; i++)
        {
            if (weaponQualityCollection.items[i] == weaponQuality)
                qualityIndex = i;
        }

        if (qualityIndex == -1)
            return false;

        return matrix[weaponIndex, qualityIndex];
    }

    public bool AddRandomWeaponQuality (ref float budget, SpecificWeapon specificWeapon)
    {
        List<WeaponQuality> qualities = weaponQualityCollection.items.ToList();

        int weaponIndex = -1;
        for (int i = 0; i < weaponCollection.items.Length; i++)
        {
            if (weaponCollection.items[i] == specificWeapon.weapon)
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
            Quality.QualityType qualityType = qualities[i].qualityType;
            bool isBonus = qualityType == Quality.QualityType.EnhancementBonus;
            bool isMaterial = qualityType == Quality.QualityType.SpecialMaterial;
            bool isSpecial = qualityType == Quality.QualityType.SpecialMaterial;

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
        if (chosenQuality.qualityType == Quality.QualityType.EnhancementBonus)
            specificWeapon.enhancementBonus = chosenQuality;
        if (chosenQuality.qualityType == Quality.QualityType.SpecialMaterial)
            specificWeapon.specialMaterial = chosenQuality;
        if (chosenQuality.qualityType == Quality.QualityType.SpecialAbility)
            specificWeapon.AddSpecialAbility(chosenQuality);

        return true;
    }

    public void AssignRandomWeapon (ref float budget, SpecificWeapon specificWeapon)
    {
        specificWeapon.weapon = weaponCollection.PickWeapon(ref budget);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += weaponCollection.name + jsonSplitter[0];
        jsonString += weaponQualityCollection.name + jsonSplitter[0];

        for(int i = 0; i < weaponCollection.items.Length; i++)
        {
            for(int j = 0; j < weaponQualityCollection.items.Length; j++)
            {
                jsonString += Wrapper<bool>.GetJsonString(matrix[i, j]) + jsonSplitter[0];
            }
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        weaponCollection = WeaponCollection.Load(splitJsonString[1]);
        weaponQualityCollection = WeaponQualityCollection.Load(splitJsonString[2]);

        int jsonIndex = 3;

        for (int i = 0; i < weaponCollection.items.Length; i++)
        {
            for (int j = 0; j < weaponQualityCollection.items.Length; j++)
            {
                matrix[i, j] = Wrapper<bool>.CreateFromJsonString(splitJsonString[jsonIndex]);
                jsonIndex++;
            }
        }
    }
}
