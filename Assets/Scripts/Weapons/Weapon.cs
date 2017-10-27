using UnityEngine;
using System;

// IMPORTANT NOTE: deal with ammunition, firearms and cartridges separately.
public class Weapon : Item<Weapon>
{
    [Flags]
    public enum WeaponType
    {
        Simple = 1 << 0,
        Martial = 1 << 1,
        Exotic = 1 << 2,
    }


    [Flags]
    public enum Handedness
    {
        Light = 1 << 0,
        OneHanded = 1 << 1,
        TwoHanded = 1 << 2,
        Ranged = 1 << 3,
    }


    [Flags]
    public enum DamageType
    {
        Bludgeoning = 1 << 0,
        Piercing = 1 << 1,
        Slashing = 1 << 2,
    }


    [Flags]
    public enum WeaponGroup
    {
        Axes = 1 << 0,
        BladesHeavy = 1 << 1,
        BladesLight = 1 << 2,
        Bows = 1 << 3,
        Close = 1 << 4,
        Crossbows = 1 << 5,
        Double = 1 << 6,
        Firearms = 1 << 7,
        Flails = 1 << 8,
        Hammers = 1 << 9,
        Monk = 1 << 10,
        Natural = 1 << 11,
        Polearms = 1 << 12,
        SiegeEngines = 1 << 13,
        Spears = 1 << 14,
        Thrown = 1 << 15,
    }


    [Flags]
    public enum Special
    {
        Blocking = 1 << 0,
        Brace = 1 << 1,
        Deadly = 1 << 2,
        Disarm = 1 << 3,
        Distracting = 1 << 4,
        Double = 1 << 5,
        Fragile = 1 << 6,
        Grapple = 1 << 7,
        Monk = 1 << 8,
        Nonlethal = 1 << 9,
        Performance = 1 << 10,
        Reach = 1 << 11,
        SeeText = 1 << 12,
        Thrown = 1 << 13,
        Trip = 1 << 14,
    }


    public WeaponType weaponType;
    public Handedness handedness;
    public DamageType damageType;
    public WeaponGroup weaponGroup;
    public Special special;
    public QualityConstraint[] constraints = new QualityConstraint[0];

    public static Weapon Create (string name, int cost, Item.Rarity rarity, int page,
        WeaponType type, Handedness handedness, DamageType damageType, WeaponGroup weaponGroup,
        Special special, WeaponQualityCollection weaponQualityCollection)
    {
        Weapon newWeapon = CreateInstance<Weapon> ();
        newWeapon.name = name;
        newWeapon.cost = cost;
        newWeapon.rarity = rarity;
        newWeapon.ulimateEquipmentPage = page;
        newWeapon.weaponType = type;
        newWeapon.handedness = handedness;
        newWeapon.damageType = damageType;
        newWeapon.weaponGroup = weaponGroup;
        newWeapon.special = special;
        newWeapon.SetupQualityConstraints (weaponQualityCollection);
        return newWeapon;
    }

    public static Weapon CreateBlank (WeaponQualityCollection weaponQualityCollection)
    {
        return Create ("NAME", 0, Item.Rarity.Mundane, 999, WeaponType.Simple,  // TODO: change this to actual ue page
            Handedness.Light, DamageType.Bludgeoning, WeaponGroup.Hammers, 0, weaponQualityCollection);
    }

    public void SetupQualityConstraints (WeaponQualityCollection weaponQualityCollection)
    {
        constraints = new QualityConstraint[weaponQualityCollection.qualities.Length];
        for (int i = 0; i < constraints.Length; i++)
        {
            constraints[i] = CreateInstance<QualityConstraint>();
            constraints[i].name = weaponQualityCollection.qualities[i].name;
            constraints[i].allowed = true;
        }
    }

    public bool CanWeaponUseQuality (WeaponQuality weaponQuality)
    {
        for (int i = 0; i < constraints.Length; i++)
        {
            if (constraints[i].name == weaponQuality.name)
                return constraints[i].allowed;
        }
        return false;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)rarity) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(ulimateEquipmentPage) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)weaponType) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)handedness) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)damageType) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)weaponGroup) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)special) + jsonSplitter[0];

        for (int i = 0; i < constraints.Length; i++)
        {
            jsonString += QualityConstraint.GetJsonString (constraints[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        cost = Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
        rarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        ulimateEquipmentPage = Wrapper<int>.CreateFromJsonString(splitJsonString[3]);
        weaponType = (WeaponType)Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
        handedness = (Handedness)Wrapper<int>.CreateFromJsonString (splitJsonString[5]);
        damageType = (DamageType)Wrapper<int>.CreateFromJsonString (splitJsonString[6]);
        weaponGroup = (WeaponGroup)Wrapper<int>.CreateFromJsonString (splitJsonString[7]);
        special = (Special)Wrapper<int>.CreateFromJsonString (splitJsonString[8]);

        constraints = new QualityConstraint[splitJsonString.Length - 9];
        for (int i = 0; i < constraints.Length; i++)
        {
            constraints[i] = QualityConstraint.CreateFromJsonString (splitJsonString[i + 9]);
        }
    }
}
