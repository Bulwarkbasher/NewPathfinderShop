using UnityEngine;
using System.Collections;
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

    public static Weapon Create (string name, int cost, Item.Rarity rarity, int page,
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
        jsonString += Wrapper<int>.GetJsonString((int)special) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)attackConstraints) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)materialConstraints) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)specialConstraints) + jsonSplitter[0];

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
        special = (Special)Wrapper<int>.CreateFromJsonString (splitJsonString[7]);
        attackConstraints = (AttackConstraints)Wrapper<int>.CreateFromJsonString (splitJsonString[8]);
        materialConstraints = (MaterialConstraints)Wrapper<int>.CreateFromJsonString (splitJsonString[9]);
        specialConstraints = (SpecialConstraints)Wrapper<int>.CreateFromJsonString(splitJsonString[10]);
    }
}
