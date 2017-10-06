using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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

        Save(weaponQualityCollection);

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

        List<WeaponQuality> constrainedWeaponQualityList = new List<WeaponQuality>();
        for(int i = 0; i < availableQualities.Length; i++)
        {
            WeaponQuality weaponQuality = availableQualities[i];

            if(FlagUtilities.ValueHasAnyFlag((int)weaponQuality.anyOfWeaponType, (int)weaponToAddTo.weaponType) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.anyOfHandedness, (int)weaponToAddTo.handedness) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.anyOfDamageType, (int)weaponToAddTo.damageType) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.anyOfSpecial, (int)weaponToAddTo.special) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.anyOfAttackConstraints, (int)weaponToAddTo.attackConstraints) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.anyOfMaterialConstraints, (int)weaponToAddTo.materialConstraints) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.anyOfSpecialConstraints, (int)weaponToAddTo.specialConstraints) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.allOfWeaponType, (int)weaponToAddTo.weaponType) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.allOfHandedness, (int)weaponToAddTo.handedness) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.allOfDamageType, (int)weaponToAddTo.damageType) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.allOfSpecial, (int)weaponToAddTo.special) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.allOfAttackConstraints, (int)weaponToAddTo.attackConstraints) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.allOfMaterialConstraints, (int)weaponToAddTo.materialConstraints) &&
            FlagUtilities.ValueHasAnyFlag((int)weaponQuality.allOfSpecialConstraints, (int)weaponToAddTo.specialConstraints))
            {
                constrainedWeaponQualityList.Add(weaponQuality);
            }            
        }
        availableQualities = constrainedWeaponQualityList.ToArray();

        return Item.PickItem(availableQualities) as WeaponQuality;
    }

    protected override string GetJsonString(string[] jsonSplitter)
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
