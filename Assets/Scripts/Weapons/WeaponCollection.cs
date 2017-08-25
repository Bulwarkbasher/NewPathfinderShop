using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class WeaponCollection : ScriptableObject
{
    public Weapon[] weapons = new Weapon[0];


    private static readonly string[] k_JsonSplitter =
    {
        "###WeaponCollSplitter###",
    };


    public Weapon PickWeapon (ref int budget)
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


    public static string GetJsonString (WeaponCollection weaponCollection)
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
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, System.StringSplitOptions.RemoveEmptyEntries);

        WeaponCollection weaponCollection = CreateInstance<WeaponCollection>();
        weaponCollection.weapons = new Weapon[splitJsonString.Length];
        for(int i = 0; i < splitJsonString.Length; i++)
        {
            weaponCollection.weapons[i] = Weapon.CreateFromJsonString(splitJsonString[i]);
        }

        return weaponCollection;
    }
}
