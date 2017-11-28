using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRingCollection : SpecificItemCollection<SpecificRing, SpecificRingCollection>
{
    public RingCollection ringCollection;

    public static SpecificRingCollection Create(IntStratRanges stockAvailability, RingCollection ringCollection, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        SpecificRingCollection newSpecificRingCollection = CreateInstance<SpecificRingCollection>();
        newSpecificRingCollection.stockAvailability = stockAvailability;
        newSpecificRingCollection.ringCollection = ringCollection;
        newSpecificRingCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificRingCollection;
    }

    public static SpecificRingCollection Create(Shop.Size size)
    {
        SpecificRingCollection newSpecificRingCollection = CreateInstance<SpecificRingCollection>();
        newSpecificRingCollection.stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Ring][size];
        newSpecificRingCollection.ringCollection = DefaultResourceHolder.DefaultRingCollection;
        newSpecificRingCollection.BuyStock(DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Ring]);
        return newSpecificRingCollection;
    }

    protected override SpecificRing CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificRing.CreateRandom(powerLevel, budgetRange, ringCollection);
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability, RingCollection ringCollection, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Wand][shop.size];

        if (ringCollection == null)
            ringCollection = DefaultResourceHolder.DefaultRingCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Ring];

        shop.specificRingCollection = Create(stockAvailability, ringCollection, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(stockAvailability) + jsonSplitter[0];
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
        stockAvailability = IntStratRanges.CreateFromJsonString(splitJsonString[1]);
        ringCollection = RingCollection.Load(splitJsonString[2]);

        specificItems = new SpecificRing[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificRing.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}