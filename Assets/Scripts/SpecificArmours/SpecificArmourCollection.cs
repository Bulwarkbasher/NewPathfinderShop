using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificArmourCollection : SpecificItemCollection<SpecificArmour, SpecificArmourCollection>
{
    public static SpecificArmourCollection Create(Availability stockAvailability, ArmourCollection availableArmours, ArmourQualityCollection availableArmourQualities)
    {
        return CreateInstance<SpecificArmourCollection>();
    }

    protected override SpecificArmour GetRandomSpecificItem (SpecificArmour.PowerLevel powerLevel, int budget)
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

    protected override void SetupFromSplitJsonString (string[] splitJsonString)
    {
        throw new System.NotImplementedException ();
    }

    protected override string ConvertToJsonString (string[] jsonSplitter)
    {
        throw new System.NotImplementedException ();
    }
}
