using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaffCollection : SpecificItemCollection<SpecificStaff>
{
    public static SpecificStaffCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificStaffCollection>();
    }

    protected override SpecificStaff GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
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

    public static string GetJsonString(SpecificStaffCollection specificStaffCollection)
    {
        return "";
    }

    public static SpecificStaffCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificStaffCollection>();
    }
}
