using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificArmourCollection : SpecificItemCollection<SpecificArmour>
{
    public static SpecificArmourCollection Create(Availability stockAvailability, ArmourCollection availableArmours, ArmourQualityCollection availableArmourQualities)
    {
        return CreateInstance<SpecificArmourCollection>();
    }

    protected override SpecificArmour GetRandomSpecificItem (SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificArmour.CreateRandom ();
    }

    public static void AddToShop(Shop shop, Availability stockAvailability, ArmourCollection availableArmours, ArmourQualityCollection availableArmourQualities)
    {
        shop.stockTypes |= Shop.StockType.Armour;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Armour][shop.size];

        if (availableArmours == null)
            availableArmours = DefaultResourceHolder.DefaultArmourCollection;

        if (availableArmourQualities == null)
            availableArmourQualities = DefaultResourceHolder.DefaultArmourQualityCollection;

        shop.specificArmourCollection = Create(stockAvailability, availableArmours, availableArmourQualities);
    }

    public static string GetJsonString(SpecificArmourCollection specificArmourCollection)
    {
        return "";
    }

    public static SpecificArmourCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificArmourCollection>();
    }
}
