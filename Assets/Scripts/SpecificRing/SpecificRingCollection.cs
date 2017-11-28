using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRingCollection : SpecificItemCollection<SpecificRing, SpecificRingCollection>
{
    public static SpecificRingCollection Create(IntStratRanges stockAvailability)
    {
        return CreateInstance<SpecificRingCollection>();
    }

    protected override SpecificRing CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificRing.CreateRandom();
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability)
    {
        shop.stockTypes |= Shop.StockType.Ring;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Ring][shop.size];

        shop.specificRingCollection = Create(stockAvailability);
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