using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRodCollection : SpecificItemCollection<SpecificRod, SpecificRodCollection>
{
    public static SpecificRodCollection Create(IntStratRanges stockAvailability)
    {
        return CreateInstance<SpecificRodCollection>();
    }

    protected override SpecificRod CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificRod.CreateRandom();
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability)
    {
        shop.stockTypes |= Shop.StockType.Rod;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Rod][shop.size];

        shop.specificRodCollection = Create (stockAvailability);
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
