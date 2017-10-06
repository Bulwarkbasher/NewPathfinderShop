using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponCollection : Saveable<WeaponCollection>
{
    public Weapon[] weapons = new Weapon[0];

    public static WeaponCollection Create (string name)
    {
        WeaponCollection newWeaponCollection = CreateInstance<WeaponCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Weapon Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Weapon Collection name invalid, name cannot start with Default");

        newWeaponCollection.name = name;
        newWeaponCollection.weapons = new Weapon[0];

        Save(newWeaponCollection);

        return newWeaponCollection;
    }

    public void AddWeapon (Weapon newWeapon)
    {
        Weapon[] newWeapons = new Weapon[weapons.Length + 1];
        for (int i = 0; i < weapons.Length; i++)
        {
            newWeapons[i] = weapons[i];
        }
        newWeapons[weapons.Length] = newWeapon;
    }

    public void RemoveWeaponAt (int index)
    {
        Weapon[] newWeapons = new Weapon[weapons.Length - 1];
        for (int i = 0; i < newWeapons.Length; i++)
        {
            int oldWeaponIndex = i < index ? i : i + 1;
            newWeapons[i] = weapons[oldWeaponIndex];
        }
    }

    public Weapon PickWeapon(ref int budget)
    {
        if (weapons.Length == 0)
            return null;

        List<Weapon> affordableWeaponList = new List<Weapon>();

        for(int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].cost < budget)
                affordableWeaponList.Add(weapons[i]);
        }
        
        Weapon[] affordableWeapons = affordableWeaponList.ToArray();

        Weapon chosenWeapon = Item.PickItem(affordableWeapons) as Weapon;
        budget -= chosenWeapon.cost;

        return chosenWeapon;
    }

    protected override string GetJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];

        for (int i = 0; i < weapons.Length; i++)
        {
            jsonString += Weapon.GetJsonString(weapons[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];

        weapons = new Weapon[splitJsonString.Length - 1];
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = Weapon.CreateFromJsonString(splitJsonString[i + 1]);
        }
    }
}
