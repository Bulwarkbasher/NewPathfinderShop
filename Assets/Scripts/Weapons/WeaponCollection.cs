using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class WeaponCollection : ItemCollection<WeaponCollection, Weapon>
{
    public static WeaponCollection Create(string name, EnumSetting books)
    {
        WeaponCollection newWeaponCollection = CreateInstance<WeaponCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Weapon Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Weapon Collection name invalid, name cannot start with Default");

        newWeaponCollection.name = name;
        newWeaponCollection.items = new Weapon[0];
        newWeaponCollection.books = books;

        SaveableHolder.AddSaveable(newWeaponCollection);

        return newWeaponCollection;
    }

    public void AddWeapon ()
    {
        Weapon newWeapon = Weapon.CreateBlank (books);
        AddItem (newWeapon);
    }
    
    public Weapon PickWeapon(ref float budget)
    {
        if (items.Length == 0)
            return null;

        List<Weapon> affordableWeaponList = new List<Weapon>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost < budget)
                affordableWeaponList.Add(items[i]);
        }
        
        Weapon[] affordableWeapons = affordableWeaponList.ToArray();

        Weapon chosenWeapon = Weapon.PickItem(affordableWeapons);
        budget -= chosenWeapon.cost;

        return chosenWeapon;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += Weapon.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        books = EnumSetting.Load(splitJsonString[1]);

        items = new Weapon[splitJsonString.Length - 2];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Weapon.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}
