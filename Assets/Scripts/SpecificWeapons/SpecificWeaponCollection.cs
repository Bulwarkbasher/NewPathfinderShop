using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// TODO: extension methods of generic interfaces for this and similar collections/shops?
// TODO: this shouldn't be saveable by itself
[CreateAssetMenu]
public class SpecificWeaponCollection : ScriptableObject
{
    public Availability stockAvailability;
    public WeaponCollection availableWeapons;
    public WeaponQualitiesCollection availableWeaponQualities;
    public SpecificWeapon[] specificWeapons = new SpecificWeapon[0];


    private static readonly string[] k_JsonSplitter =
    {
        "###SpecWeapCollSplitter###",
    };


    public static SpecificWeaponCollection Create (Availability stockAvailability, WeaponCollection availableWeapons, WeaponQualitiesCollection availableWeaponQualities)
    {
        SpecificWeaponCollection newSpecificWeaponCollection = CreateInstance<SpecificWeaponCollection>();
        newSpecificWeaponCollection.stockAvailability = stockAvailability;
        newSpecificWeaponCollection.availableWeapons = availableWeapons;
        newSpecificWeaponCollection.availableWeaponQualities = availableWeaponQualities;
        newSpecificWeaponCollection.BuyStock();

        return newSpecificWeaponCollection;
    }


    public int GetTotalCost ()
    {
        int total = 0;
        for(int i = 0; i < specificWeapons.Length; i++)
        {
            total += specificWeapons[i].cost;
        }
        return total;
    }


    public int GetTotalCost (SpecificItem.PowerLevel powerLevel)
    {
        int total = 0;
        for (int i = 0; i < specificWeapons.Length; i++)
        {
            if(specificWeapons[i].powerLevel == powerLevel)
                total += specificWeapons[i].cost;
        }
        return total;
    }


    public int GetTotalCount ()
    {
        return specificWeapons.Length;
    }


    public int GetTotalCount (SpecificItem.PowerLevel powerLevel)
    {
        int total = 0;
        for (int i = 0; i < specificWeapons.Length; i++)
        {
            if (specificWeapons[i].powerLevel == powerLevel)
                total++;
        }
        return total;
    }

    // TODO: implement functions for adjusting weapons in the collection
    // Add
    // Remove
    // RerollSpecificWeapon (called by shop with budget passed)
    // Restock (some items added, some sold based on settings which the shop passes)

    public void Add (SpecificWeapon specificWeapon)
    {
        SpecificWeapon[] newSpecificWeapons = new SpecificWeapon[specificWeapons.Length + 1];
        for (int i = 0; i < specificWeapons.Length; i++)
        {
            newSpecificWeapons[i] = specificWeapons[i];
        }
        newSpecificWeapons[specificWeapons.Length] = specificWeapon;
        specificWeapons = newSpecificWeapons;
    }

    public void AddRandom(SpecificItem.PowerLevel powerLevel, int budget)
    {
        SpecificWeapon[] newSpecificWeapons = new SpecificWeapon[specificWeapons.Length + 1];
        for(int i = 0; i < specificWeapons.Length; i++)
        {
            newSpecificWeapons[i] = specificWeapons[i];
        }
        newSpecificWeapons[specificWeapons.Length] = SpecificWeapon.CreateRandom(powerLevel, budget, availableWeapons, availableWeaponQualities);
        specificWeapons = newSpecificWeapons;
    }


    public void RemoveAt (int index)
    {
        SpecificWeapon[] newSpecificWeapons = new SpecificWeapon[specificWeapons.Length - 1];
        int newIndex, oldIndex;
        for(newIndex = 0,oldIndex = 0; oldIndex < specificWeapons.Length; oldIndex++, newIndex++)
        {
            if (oldIndex == index)
                oldIndex++;
            newSpecificWeapons[newIndex] = specificWeapons[oldIndex];
        }
        specificWeapons = newSpecificWeapons;
    }


    public void RerollToRandom (int index, SpecificItem.PowerLevel powerLevel, int budget)
    {
        specificWeapons[index] = SpecificWeapon.CreateRandom(powerLevel, budget, availableWeapons, availableWeaponQualities);
    }


    public void Restock (RestockSettings restockSettings)
    {
        SellStock(restockSettings);

        BuyStock();
    }


    public void SellStock (RestockSettings restockSettings)
    {
        int totalCost = GetTotalCost();
        float percentToRestock = restockSettings.percent.Random() / 100f;
        int restockAmount = Mathf.FloorToInt(totalCost * percentToRestock);
        bool nothingSold = false;
        while (restockAmount > 0)
        {
            int origSellIndex = UnityEngine.Random.Range(0, specificWeapons.Length);
            int sellIndex = origSellIndex;
            do
            {
                if (specificWeapons[sellIndex].cost < restockAmount)
                {
                    restockAmount -= specificWeapons[sellIndex].cost;
                    RemoveAt(sellIndex);
                    break;
                }
                sellIndex = (sellIndex + 1) % specificWeapons.Length;

                if (sellIndex == origSellIndex)
                    nothingSold = true;
            }
            while (sellIndex != origSellIndex);

            if (nothingSold)
                break;
        }
    }


    public void BuyStock ()
    {
        BuyStock(SpecificItem.PowerLevel.Minor, stockAvailability.stock.minor, stockAvailability.budget.minor, stockAvailability.budgetVariation);
        BuyStock(SpecificItem.PowerLevel.Medium, stockAvailability.stock.medium, stockAvailability.budget.medium, stockAvailability.budgetVariation);
        BuyStock(SpecificItem.PowerLevel.Major, stockAvailability.stock.major, stockAvailability.budget.major, stockAvailability.budgetVariation);
    }


    public void BuyStock (SpecificItem.PowerLevel powerLevel, Range stockRange, Range budgetRange, float budgetVariation)
    {
        int desiredCount = stockRange.Random();
        int currentCount = GetTotalCount(powerLevel);
        int desiredTotalBudget = budgetRange.Random();
        int currentBudget = GetTotalCost(powerLevel);

        int requiredStockCount = desiredCount - currentCount;
        if (requiredStockCount > 0)
        {
            int requiredStockBudget = desiredTotalBudget - currentBudget;

            if (requiredStockBudget > 0)
            {
                int meanBudget = Mathf.FloorToInt(requiredStockBudget / requiredStockCount);

                for (int i = 0; i < requiredStockCount; i++)
                {
                    int specificBudget = Mathf.FloorToInt(meanBudget * UnityEngine.Random.Range(-budgetVariation, budgetVariation));

                    SpecificWeapon newSpecificWeapon = SpecificWeapon.CreateRandom(powerLevel, specificBudget, availableWeapons, availableWeaponQualities);
                    Add(newSpecificWeapon);
                }
            }
        }
    }

    public static string GetJsonString(SpecificWeaponCollection specificWeaponCollection)
    {
        string jsonString = "";

        jsonString += Availability.GetJsonString(specificWeaponCollection.stockAvailability) + k_JsonSplitter[0];
        jsonString += WeaponCollection.GetJsonString(specificWeaponCollection.availableWeapons) + k_JsonSplitter[0];
        jsonString += WeaponQualitiesCollection.GetJsonString(specificWeaponCollection.availableWeaponQualities) + k_JsonSplitter[0];

        for(int i = 0; i < specificWeaponCollection.specificWeapons.Length; i++)
        {
            jsonString += SpecificWeapon.GetJsonString(specificWeaponCollection.specificWeapons[i]) + k_JsonSplitter[0];
        }
        return jsonString;
    }


    public static SpecificWeaponCollection CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);

        SpecificWeaponCollection specificWeaponCollection = CreateInstance<SpecificWeaponCollection>();

        specificWeaponCollection.stockAvailability = Availability.CreateFromJsonString(splitJsonString[0]);
        specificWeaponCollection.availableWeapons = WeaponCollection.CreateFromJsonString(splitJsonString[1]);
        specificWeaponCollection.availableWeaponQualities = WeaponQualitiesCollection.CreateFromJsonString(splitJsonString[2]);

        specificWeaponCollection.specificWeapons = new SpecificWeapon[splitJsonString.Length];
        for(int i = 0; i < splitJsonString.Length; i++)
        {
            specificWeaponCollection.specificWeapons[i] = SpecificWeapon.CreateFromJsonString(splitJsonString[i + 3]);
        }
        return specificWeaponCollection;
    }
}
