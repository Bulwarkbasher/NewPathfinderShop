using UnityEngine;
using System.Collections;
using System;

// IMPORTANT NOTE: deal with ammunition, firearms and cartridges separately.
public class Weapon : Item
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


    [Flags]
    public enum AttackConstraints
    {
        Melee = 1 << 0,
        Ranged = 1 << 1,
        Thrown = 1 << 2,
    }


    [Flags]
    public enum MaterialConstraints
    {
        MetalAttackingPart = 1 << 0,
        MostlyMadeFromMetal = 1 << 1,
        MetalComponent = 1 << 2,
        WoodenAttackingPart = 1 << 3,
        MostlyMadeFromWood = 1 << 4,
        WoodenComponent = 1 << 5,
    }


    [Flags]
    public enum SpecialConstraints
    {
        IsCompositeBow = 1 << 0,
        IsBow = 1 << 1,
        IsCrossbow = 1 << 2,
        UsesAmmo = 1 << 3,
    }


    public WeaponType weaponType;
    public Handedness handedness;
    public DamageType damageType;
    public Special special;
    public AttackConstraints attackConstraints;
    public MaterialConstraints materialConstraints;
    public SpecialConstraints specialConstraints;

    static readonly string[] k_JsonSplitter = { "###WeaponSplitter###", };

    public static Weapon Create (string name, int cost, Rarity rarity, int page,
        WeaponType type, Handedness handedness, DamageType damageType, Special special,
        AttackConstraints attackConstraints, MaterialConstraints materialConstraints,
        SpecialConstraints specialConstraints)
    {
        Weapon newWeapon = CreateInstance<Weapon> ();
        newWeapon.name = name;
        newWeapon.cost = cost;
        newWeapon.rarity = rarity;
        newWeapon.ulimateEquipmentPage = page;
        newWeapon.weaponType = type;
        newWeapon.handedness = handedness;
        newWeapon.damageType = damageType;
        newWeapon.special = special;
        newWeapon.attackConstraints = attackConstraints;
        newWeapon.materialConstraints = materialConstraints;
        newWeapon.specialConstraints = specialConstraints;
        return newWeapon;
    }

    public static string GetJsonString (Weapon weapon)
    {
        string jsonString = Item.GetJsonString (weapon);

        jsonString += Wrapper<int>.GetJsonString((int)weapon.weaponType) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)weapon.handedness) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)weapon.damageType) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)weapon.special) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)weapon.attackConstraints) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)weapon.materialConstraints) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)weapon.specialConstraints) + k_JsonSplitter[0];

        return jsonString;
    }

    public new static Weapon CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);
        Item weaponBase = Item.CreateFromJsonString (splitJsonString[0]);
        Weapon weapon = CreateInstance<Weapon>();

        weapon.name = weaponBase.name;
        weapon.cost = weaponBase.cost;
        weapon.rarity = weaponBase.rarity;
        weapon.ulimateEquipmentPage = weaponBase.ulimateEquipmentPage;
        weapon.weaponType = (WeaponType)Wrapper<int>.CreateFromJsonString (splitJsonString[1]);
        weapon.handedness = (Handedness)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);
        weapon.damageType = (DamageType)Wrapper<int>.CreateFromJsonString (splitJsonString[3]);
        weapon.special = (Special)Wrapper<int>.CreateFromJsonString (splitJsonString[4]);
        weapon.attackConstraints = (AttackConstraints)Wrapper<int>.CreateFromJsonString (splitJsonString[5]);
        weapon.materialConstraints = (MaterialConstraints)Wrapper<int>.CreateFromJsonString (splitJsonString[6]);
        weapon.specialConstraints = (SpecialConstraints)Wrapper<int>.CreateFromJsonString(splitJsonString[7]);

        return weapon;
    }
}
