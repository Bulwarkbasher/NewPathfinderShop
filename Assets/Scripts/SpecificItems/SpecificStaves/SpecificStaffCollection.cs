using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaffCollection : SpecificItemCollection<SpecificStaff, SpecificStaffCollection>
{
    public StaffCollection staffCollection;

    public static SpecificStaffCollection Create(IntRangePerPowerLevel stockAvailability, StaffCollection staffCollection, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificStaffCollection newSpecificStaffCollection = CreateInstance<SpecificStaffCollection>();
        newSpecificStaffCollection.stockAvailability = stockAvailability;
        newSpecificStaffCollection.staffCollection = staffCollection;
        newSpecificStaffCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificStaffCollection;
    }

    public static SpecificStaffCollection Create(string shopSize)
    {
        SpecificStaffCollection newSpecificStaffCollection = CreateInstance<SpecificStaffCollection>();
        newSpecificStaffCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Staff][shopSize];
        newSpecificStaffCollection.staffCollection = Campaign.StaffCollection;
        newSpecificStaffCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Staff]);
        return newSpecificStaffCollection;
    }

    protected override SpecificStaff CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificStaff.CreateRandom(powerLevel, budgetRange, staffCollection);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, StaffCollection staffCollection, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Wand][shop.size];

        if (staffCollection == null)
            staffCollection = Campaign.StaffCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Staff];

        shop.specificStaffCollection = Create(stockAvailability, staffCollection, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
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
        stockAvailability = IntRangePerPowerLevel.Load(splitJsonString[1]);
        staffCollection = StaffCollection.Load(splitJsonString[2]);

        specificItems = new SpecificStaff[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificStaff.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
