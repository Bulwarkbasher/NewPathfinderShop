using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponCollection : ScriptableObject, ISaveable
{
    public string GetFolderPath() { return Application.persistentDataPath + "/WeaponCollections"; }
    private static readonly string[] k_JsonSplitter = { "###WeaponCollSplitter###", };

    public Weapon[] weapons = new Weapon[0];

    public static WeaponCollection Create (string name)
    {
        WeaponCollection newWeaponCollection = CreateInstance<WeaponCollection>();

        if (newWeaponCollection.CheckName(name) == SaveableExtensions.NameCheckResult.Bad)
            throw new UnityException("Weapon Collection name invalid, contains invalid characters.");
        if (newWeaponCollection.CheckName(name) == SaveableExtensions.NameCheckResult.IsDefault)
            throw new UnityException("Weapon Collection name invalid, name cannot start with Default");

        newWeaponCollection.name = name;
        newWeaponCollection.weapons = new Weapon[0];

        newWeaponCollection.Save ();

        return newWeaponCollection;
    }

    public static WeaponCollection Duplicate (string duplicatesName, WeaponCollection original)
    {
        WeaponCollection duplicate = original.MemberwiseClone() as WeaponCollection;
        duplicate.name = duplicatesName;
        duplicate.Save();

        duplicate = Load(duplicatesName);
        return duplicate;
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

    public void Save ()
    {
        string jsonString = "";

        jsonString += name + k_JsonSplitter[0];

        for (int i = 0; i < weapons.Length; i++)
        {
            jsonString += Weapon.GetJsonString(weapons[i]) + k_JsonSplitter[0];
        }

        this.WriteJsonStringToFile (name, jsonString);
    }

    public static WeaponCollection Load (string name)
    {
        WeaponCollection weaponCollection = CreateInstance<WeaponCollection>();

        string[] splitJsonString = weaponCollection.GetSplitJsonStringsFromFile (name, k_JsonSplitter);

        weaponCollection.name = splitJsonString[0];

        weaponCollection.weapons = new Weapon[splitJsonString.Length - 1];
        for (int i = 0; i < weaponCollection.weapons.Length; i++)
        {
            weaponCollection.weapons[i] = Weapon.CreateFromJsonString(splitJsonString[i + 1]);
        }

        return weaponCollection;
    }

    public static string[] GetSettingsNames ()
    {
        WeaponCollection dummy = CreateInstance<WeaponCollection> ();
        return dummy.GetFileNames ();
    }

    /*public static string GetJsonString (WeaponCollection weaponCollection)
    {
        string jsonString = "";

        for(int i = 0; i < weaponCollection.weapons.Length; i++)
        {
            jsonString += Weapon.GetJsonString(weaponCollection.weapons[i]) + k_JsonSplitter[0];
        }

        return jsonString;
    }


    public static WeaponCollection CreateFromJsonString (string jsonString)
    {
        WeaponCollection weaponCollection = CreateInstance<WeaponCollection>();
        
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, System.StringSplitOptions.RemoveEmptyEntries);

        weaponCollection.weapons = new Weapon[splitJsonString.Length];
        for(int i = 0; i < splitJsonString.Length; i++)
        {
            weaponCollection.weapons[i] = Weapon.CreateFromJsonString(splitJsonString[i]);
        }

        return weaponCollection;
    }*/
}
