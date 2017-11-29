using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRingCollection : SpecificItemCollection<SpecificRing, SpecificRingCollection>
{
    public RingCollection ringCollection;

    public static SpecificRingCollection Create(IntRangePerPowerLevel stockAvailability, RingCollection ringCollection, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificRingCollection newSpecificRingCollection = CreateInstance<SpecificRingCollection>();
        newSpecificRingCollection.stockAvailability = stockAvailability;
        newSpecificRingCollection.ringCollection = ringCollection;
        newSpecificRingCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificRingCollection;
    }

    public static SpecificRingCollection Create(string shopSize)
    {
        SpecificRingCollection newSpecificRingCollection = CreateInstance<SpecificRingCollection>();
        newSpecificRingCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Ring][shopSize];
        newSpecificRingCollection.ringCollection = Campaign.RingCollection;
        newSpecificRingCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Ring]);
        return newSpecificRingCollection;
    }

    protected override SpecificRing CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificRing.CreateRandom(powerLevel, budgetRange, ringCollection);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, RingCollection ringCollection, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Wand][shop.size];

        if (ringCollection == null)
            ringCollection = Campaign.RingCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Ring];

        shop.specificRingCollection = Create(stockAvailability, ringCollection, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
        jsonString += ringCollection.name + jsonSplitter[0];

        for (int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificRing.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntRangePerPowerLevel.Load(splitJsonString[1]);
        ringCollection = RingCollection.Load(splitJsonString[2]);

        specificItems = new SpecificRing[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificRing.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}