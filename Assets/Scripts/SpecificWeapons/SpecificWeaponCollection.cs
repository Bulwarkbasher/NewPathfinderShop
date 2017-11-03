using UnityEngine;
using System;

[CreateAssetMenu]
public class SpecificWeaponCollection : SpecificItemCollection<SpecificWeapon, SpecificWeaponCollection>
{
    public WeaponCollection availableWeapons;
    public WeaponQualityCollection availableWeaponQualities;
    
    public static SpecificWeaponCollection Create (Availability stockAvailability, WeaponCollection availableWeapons, WeaponQualityCollection availableWeaponQualities)
    {
        SpecificWeaponCollection newSpecificWeaponCollection = CreateInstance<SpecificWeaponCollection>();
        newSpecificWeaponCollection.stockAvailability = stockAvailability;
        newSpecificWeaponCollection.availableWeapons = availableWeapons;
        newSpecificWeaponCollection.availableWeaponQualities = availableWeaponQualities;
        newSpecificWeaponCollection.BuyStock();

        return newSpecificWeaponCollection;
    }

    public static SpecificWeaponCollection Create (Shop.Size size)
    {
        SpecificWeaponCollection newSpecificWeaponCollection = CreateInstance<SpecificWeaponCollection>();
        newSpecificWeaponCollection.stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Weapon][size];
        newSpecificWeaponCollection.availableWeapons = DefaultResourceHolder.DefaultWeaponCollection;
        newSpecificWeaponCollection.availableWeaponQualities = DefaultResourceHolder.DefaultWeaponQualityCollection;
        newSpecificWeaponCollection.BuyStock();

        return newSpecificWeaponCollection;
    }
    
    protected override SpecificWeapon GetRandomSpecificItem (SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificWeapon.CreateRandom(powerLevel, budget, availableWeapons, availableWeaponQualities);
    }

    public static void AddToShop(Shop shop, Availability stockAvailability, WeaponCollection availableWeapons, WeaponQualityCollection availableWeaponQualities)
    {
        shop.stockTypes |= Shop.StockType.Weapon;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Weapon][shop.size];

        if (availableWeapons == null)
            availableWeapons = DefaultResourceHolder.DefaultWeaponCollection;

        if (availableWeaponQualities == null)
            availableWeaponQualities = DefaultResourceHolder.DefaultWeaponQualityCollection;

        shop.specificWeaponCollection = Create(stockAvailability, availableWeapons, availableWeaponQualities);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += Availability.GetJsonString(stockAvailability) + jsonSplitter[0];
        jsonString += availableWeapons.name + jsonSplitter[0];
        jsonString += availableWeaponQualities.name + jsonSplitter[0];

        for(int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificWeapon.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }
        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        stockAvailability = Availability.CreateFromJsonString(splitJsonString[0]);
        availableWeapons = WeaponCollection.Load (splitJsonString[1]);
        availableWeaponQualities = WeaponQualityCollection.Load (splitJsonString[2]);
        
        specificItems = new SpecificWeapon[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificWeapon.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
