using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWondrousCollection : SpecificItemCollection<SpecificWondrous, SpecificWondrousCollection>
{
    public static SpecificWondrousCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificWondrousCollection>();
    }

    protected override SpecificWondrous GetRandomSpecificItem(SpecificWondrous.PowerLevel powerLevel, int budget)
    {
        return SpecificWondrous.CreateRandom();
    }

    public static void AddToShop(Shop shop, Availability stockAvailability)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Wand][shop.size];

        shop.specificWondrousCollection = Create(stockAvailability);
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
