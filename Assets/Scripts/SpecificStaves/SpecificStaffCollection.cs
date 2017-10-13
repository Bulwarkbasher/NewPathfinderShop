using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaffCollection : SpecificItemCollection<SpecificStaff, SpecificStaffCollection>
{
    public static SpecificStaffCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificStaffCollection>();
    }

    protected override SpecificStaff GetRandomSpecificItem(SpecificStaff.PowerLevel powerLevel, int budget)
    {
        return SpecificStaff.CreateRandom();
    }

    public static void AddToShop(Shop shop, Availability stockAvailability)
    {
        shop.stockTypes |= Shop.StockType.Staff;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Staff][shop.size];

        shop.specificStaffCollection = Create(stockAvailability);
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
