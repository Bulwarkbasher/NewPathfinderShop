using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[CreateAssetMenu]
public class SpecificWeaponCollection : SpecificItemCollection<SpecificWeapon>
{
    public WeaponCollection availableWeapons;
    public WeaponQualityCollection availableWeaponQualities;
    
    private static readonly string[] k_JsonSplitter =
    {
        "###SpecWeapCollSplitter###",
    };

    public static SpecificWeaponCollection Create (Availability stockAvailability, WeaponCollection availableWeapons, WeaponQualityCollection availableWeaponQualities)
    {
        SpecificWeaponCollection newSpecificWeaponCollection = CreateInstance<SpecificWeaponCollection>();
        newSpecificWeaponCollection.stockAvailability = stockAvailability;
        newSpecificWeaponCollection.availableWeapons = availableWeapons;
        newSpecificWeaponCollection.availableWeaponQualities = availableWeaponQualities;
        newSpecificWeaponCollection.BuyStock();

        return newSpecificWeaponCollection;
    }
    
    protected override SpecificWeapon GetRandomSpecificItem (SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificWeapon.CreateRandom(powerLevel, budget, availableWeapons, availableWeaponQualities);
    }

    public static string GetJsonString(SpecificWeaponCollection specificWeaponCollection)
    {
        string jsonString = "";

        jsonString += Availability.GetJsonString(specificWeaponCollection.stockAvailability) + k_JsonSplitter[0];
        jsonString += WeaponCollection.GetJsonString(specificWeaponCollection.availableWeapons) + k_JsonSplitter[0];
        jsonString += WeaponQualityCollection.GetJsonString(specificWeaponCollection.availableWeaponQualities) + k_JsonSplitter[0];

        for(int i = 0; i < specificWeaponCollection.specificItems.Length; i++)
        {
            jsonString += SpecificWeapon.GetJsonString(specificWeaponCollection.specificItems[i]) + k_JsonSplitter[0];
        }
        return jsonString;
    }
    
    public static SpecificWeaponCollection CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);

        SpecificWeaponCollection specificWeaponCollection = CreateInstance<SpecificWeaponCollection>();

        specificWeaponCollection.stockAvailability = Availability.CreateFromJsonString(splitJsonString[0]);
        specificWeaponCollection.availableWeapons = WeaponCollection.CreateFromJsonString(splitJsonString[1]);
        specificWeaponCollection.availableWeaponQualities = WeaponQualityCollection.CreateFromJsonString(splitJsonString[2]);

        specificWeaponCollection.specificItems = new SpecificWeapon[splitJsonString.Length - 3];
        for (int i = 0; i < specificWeaponCollection.specificItems.Length; i++)
        {
            specificWeaponCollection.specificItems[i] = SpecificWeapon.CreateFromJsonString(splitJsonString[i + 3]);
        }
        return specificWeaponCollection;
    }
}
