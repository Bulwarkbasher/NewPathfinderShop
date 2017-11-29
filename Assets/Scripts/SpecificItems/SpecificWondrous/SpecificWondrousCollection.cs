using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWondrousCollection : SpecificItemCollection<SpecificWondrous, SpecificWondrousCollection>
{
    public WondrousCollection wondrousCollection;

    public static SpecificWondrousCollection Create(IntRangePerPowerLevel stockAvailability, WondrousCollection wondrousCollection, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificWondrousCollection newSpecificWondrousCollection = CreateInstance<SpecificWondrousCollection>();
        newSpecificWondrousCollection.stockAvailability = stockAvailability;
        newSpecificWondrousCollection.wondrousCollection = wondrousCollection;
        newSpecificWondrousCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificWondrousCollection;
    }

    public static SpecificWondrousCollection Create (string shopSize)
    {
        SpecificWondrousCollection newSpecificWondrousCollection = CreateInstance<SpecificWondrousCollection>();
        newSpecificWondrousCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Wondrous][shopSize];
        newSpecificWondrousCollection.wondrousCollection = Campaign.WondrousCollection;
        newSpecificWondrousCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Wondrous]);
        return newSpecificWondrousCollection;
    }

    protected override SpecificWondrous CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificWondrous.CreateRandom(powerLevel, budgetRange, wondrousCollection);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, WondrousCollection wondrousCollection, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Wand][shop.size];

        if (wondrousCollection == null)
            wondrousCollection = Campaign.WondrousCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Wondrous];

        shop.specificWondrousCollection = Create(stockAvailability, wondrousCollection, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
        jsonString += wondrousCollection.name + jsonSplitter[0];

        for (int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificWondrous.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntRangePerPowerLevel.CreateFromJsonString(splitJsonString[1]);
        wondrousCollection = WondrousCollection.Load(splitJsonString[2]);

        specificItems = new SpecificWondrous[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificWondrous.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
