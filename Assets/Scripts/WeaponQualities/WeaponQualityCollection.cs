using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu]
public class WeaponQualityCollection : ScriptableObject
{
    public WeaponQuality[] qualities = new WeaponQuality[0];


    private static readonly string[] k_JsonSplitter =
    {
        "###WeaponQualCollSplitter###",
    };


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


    public static string GetJsonString (WeaponQualityCollection weaponQualityCollection)
    {
        string jsonString = "";
        for (int i = 0; i < weaponQualityCollection.qualities.Length; i++)
        {
            jsonString += WeaponQuality.GetJsonString(weaponQualityCollection.qualities[i]) + k_JsonSplitter[0];
        }
        return jsonString;
    }


    public static WeaponQualityCollection CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, System.StringSplitOptions.RemoveEmptyEntries);

        WeaponQualityCollection weaponQualityCollection = CreateInstance<WeaponQualityCollection>();
        weaponQualityCollection.qualities = new WeaponQuality[splitJsonString.Length];
        for (int i = 0; i < splitJsonString.Length; i++)
        {
            weaponQualityCollection.qualities[i] = WeaponQuality.CreateFromJsonString(splitJsonString[i]);
        }
        return weaponQualityCollection;
    }
}
