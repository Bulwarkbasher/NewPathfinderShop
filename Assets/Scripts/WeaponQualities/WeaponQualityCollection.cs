using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu]
public class WeaponQualityCollection : ScriptableObject, ISaveable
{
    public WeaponQuality[] qualities = new WeaponQuality[0];

    public string GetFolderPath() { return Application.persistentDataPath + "/WeaponQualityCollections"; }
    private static readonly string[] k_JsonSplitter = { "###WeaponQualCollSplitter###", };

    public static WeaponQualityCollection Create (string name)
    {
        WeaponQualityCollection weaponQualityCollection = CreateInstance<WeaponQualityCollection>();

        if (weaponQualityCollection.CheckName(name) == SaveableExtensions.NameCheckResult.Bad)
            throw new UnityException("Weapon Quality Collection name invalid, contains invalid characters.");
        if (weaponQualityCollection.CheckName(name) == SaveableExtensions.NameCheckResult.IsDefault)
            throw new UnityException("Weapon Quality Collection name invalid, name cannot start with Default");

        weaponQualityCollection.name = name;
        weaponQualityCollection.qualities = new WeaponQuality[0];

        weaponQualityCollection.Save();

        return weaponQualityCollection;
    }

    public static WeaponQualityCollection Duplicate (string duplicatesName, WeaponQualityCollection original)
    {
        WeaponQualityCollection duplicate = original.MemberwiseClone () as WeaponQualityCollection;
        duplicate.name = duplicatesName;
        duplicate.Save ();

        duplicate = Load (duplicatesName);
        return duplicate;
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


    /*public static string GetJsonString (WeaponQualityCollection weaponQualityCollection)
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
    }*/

    public void Save ()
    {
        string jsonString = "";

        jsonString += name + k_JsonSplitter[0];

        for (int i = 0; i < qualities.Length; i++)
        {
            jsonString += WeaponQuality.GetJsonString(qualities[i]) + k_JsonSplitter[0];
        }
        this.WriteJsonStringToFile (name, jsonString);
    }

    public static WeaponQualityCollection Load (string name)
    {
        WeaponQualityCollection weaponQualityCollection = CreateInstance<WeaponQualityCollection> ();

        string[] splitJsonString = weaponQualityCollection.GetSplitJsonStringsFromFile (name, k_JsonSplitter);

        weaponQualityCollection.name = splitJsonString[0];

        weaponQualityCollection.qualities = new WeaponQuality[splitJsonString.Length - 1];
        for (int i = 0; i < weaponQualityCollection.qualities.Length; i++)
        {
            weaponQualityCollection.qualities[i] = WeaponQuality.CreateFromJsonString (splitJsonString[i + 1]);
        }
        return weaponQualityCollection;
    }

    public static string[] GetSettingsNames ()
    {
        WeaponQualityCollection dummy = CreateInstance<WeaponQualityCollection> ();
        return dummy.GetFileNames ();
    }
}
