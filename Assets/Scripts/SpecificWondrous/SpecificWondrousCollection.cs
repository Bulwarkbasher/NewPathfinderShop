using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWondrousCollection : SpecificItemCollection<SpecificWondrous>
{
    public static SpecificWondrousCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificWondrousCollection>();
    }

    protected override SpecificWondrous GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
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

    public static string GetJsonString (SpecificWondrousCollection specificWondrousCollection)
    {
        return "";
    }

    public static SpecificWondrousCollection CreateFromJsonString (string jsonString)
    {
        return CreateInstance<SpecificWondrousCollection> ();
    }
}
