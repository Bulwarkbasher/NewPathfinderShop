using UnityEngine;
using System.Linq;
using System.Collections.Generic;

// TODO NEXT: use ItemCollection
[CreateAssetMenu]
public class WeaponQualityCollection : Saveable<WeaponQualityCollection>
{
    public WeaponQuality[] qualities = new WeaponQuality[0];

    public static WeaponQualityCollection Create (string name)
    {
        WeaponQualityCollection weaponQualityCollection = CreateInstance<WeaponQualityCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Weapon Quality Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Weapon Quality Collection name invalid, name cannot start with Default");

        weaponQualityCollection.name = name;
        weaponQualityCollection.qualities = new WeaponQuality[0];

        SaveableHolder.AddSaveable(weaponQualityCollection);

        return weaponQualityCollection;
    }

    public WeaponQuality PickWeaponQuality (Weapon weaponToAddTo, int budget, WeaponQuality[] bonusEquivalentQualities, bool allowMaterials, bool allowBonuses, bool allowAbilities)
    {
        if (qualities.Length == 0)
            return null;

        WeaponQuality[] availableQualities = qualities.Where(x => x.CostToIncrease(bonusEquivalentQualities) <= budget).ToArray();

        for(int i = 0; i < bonusEquivalentQualities.Length; i++)
        {
            availableQualities = availableQualities.Where(x => x != bonusEquivalentQualities[i]).ToArray();
        }
        
        if (!allowMaterials)
            availableQualities = availableQualities.Where(x => x.qualityType != Quality.QualityType.SpecialMaterial).ToArray();
        
        if (!allowBonuses)
            availableQualities = availableQualities.Where(x => x.qualityType != Quality.QualityType.EnhancementBonus).ToArray();

        if (!allowAbilities)
            availableQualities = availableQualities.Where(x => x.qualityType != Quality.QualityType.SpecialAbility).ToArray();

        availableQualities = availableQualities.Where (weaponToAddTo.CanWeaponUseQuality).ToArray ();

        return WeaponQuality.PickItem(availableQualities);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];

        for (int i = 0; i < qualities.Length; i++)
        {
            jsonString += WeaponQuality.GetJsonString(qualities[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];

        qualities = new WeaponQuality[splitJsonString.Length - 1];
        for (int i = 0; i < qualities.Length; i++)
        {
            qualities[i] = WeaponQuality.CreateFromJsonString (splitJsonString[i + 1]);
        }
    }
}
