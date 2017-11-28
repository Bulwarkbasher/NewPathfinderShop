using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaffCollection : SpecificItemCollection<SpecificStaff, SpecificStaffCollection>
{
    public StaffCollection staffCollection;

    public static SpecificStaffCollection Create(IntStratRanges stockAvailability, StaffCollection staffCollection, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        SpecificStaffCollection newSpecificStaffCollection = CreateInstance<SpecificStaffCollection>();
        newSpecificStaffCollection.stockAvailability = stockAvailability;
        newSpecificStaffCollection.staffCollection = staffCollection;
        newSpecificStaffCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificStaffCollection;
    }

    public static SpecificStaffCollection Create(Shop.Size size)
    {
        SpecificStaffCollection newSpecificStaffCollection = CreateInstance<SpecificStaffCollection>();
        newSpecificStaffCollection.stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Staff][size];
        newSpecificStaffCollection.staffCollection = DefaultResourceHolder.DefaultStaffCollection;
        newSpecificStaffCollection.BuyStock(DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Staff]);
        return newSpecificStaffCollection;
    }

    protected override SpecificStaff CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificStaff.CreateRandom(powerLevel, budgetRange, staffCollection);
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability, StaffCollection staffCollection, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Wand][shop.size];

        if (staffCollection == null)
            staffCollection = DefaultResourceHolder.DefaultStaffCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Staff];

        shop.specificStaffCollection = Create(stockAvailability, staffCollection, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(stockAvailability) + jsonSplitter[0];
        jsonString += staffCollection.name + jsonSplitter[0];

        for (int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificStaff.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntStratRanges.CreateFromJsonString(splitJsonString[1]);
        staffCollection = StaffCollection.Load(splitJsonString[2]);

        specificItems = new SpecificStaff[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificStaff.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
