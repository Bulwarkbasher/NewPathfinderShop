using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class WeaponCollection : ItemCollection<WeaponCollectionFilter, WeaponCollection, Weapon>
{
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
}
