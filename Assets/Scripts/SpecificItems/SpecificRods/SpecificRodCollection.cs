using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRodCollection : SpecificItemCollection<SpecificRod, SpecificRodCollection>
{
    public RodCollection rodCollection;

    public static SpecificRodCollection Create(IntRangePerPowerLevel stockAvailability, RodCollection rodCollection, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificRodCollection newSpecificRodCollection = CreateInstance<SpecificRodCollection>();
        newSpecificRodCollection.stockAvailability = stockAvailability;
        newSpecificRodCollection.rodCollection = rodCollection;
        newSpecificRodCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificRodCollection;
    }

    public static SpecificRodCollection Create(string shopSize)
    {
        SpecificRodCollection newSpecificRodCollection = CreateInstance<SpecificRodCollection>();
        newSpecificRodCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Rod][shopSize];
        newSpecificRodCollection.rodCollection = Campaign.RodCollection;
        newSpecificRodCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Rod]);
        return newSpecificRodCollection;
    }

    protected override SpecificRod CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificRod.CreateRandom(powerLevel, budgetRange, rodCollection);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, RodCollection rodCollection, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Wand][shop.size];

        if (rodCollection == null)
            rodCollection = Campaign.RodCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Rod];

        shop.specificRodCollection = Create(stockAvailability, rodCollection, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
        jsonString += rodCollection.name + jsonSplitter[0];

        for (int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificRod.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntRangePerPowerLevel.Load(splitJsonString[1]);
        rodCollection = RodCollection.Load(splitJsonString[2]);

        specificItems = new SpecificRod[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificRod.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
