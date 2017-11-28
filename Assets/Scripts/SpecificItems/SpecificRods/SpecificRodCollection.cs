using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRodCollection : SpecificItemCollection<SpecificRod, SpecificRodCollection>
{
    public RodCollection rodCollection;

    public static SpecificRodCollection Create(IntStratRanges stockAvailability, RodCollection rodCollection, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        SpecificRodCollection newSpecificRodCollection = CreateInstance<SpecificRodCollection>();
        newSpecificRodCollection.stockAvailability = stockAvailability;
        newSpecificRodCollection.rodCollection = rodCollection;
        newSpecificRodCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificRodCollection;
    }

    public static SpecificRodCollection Create(Shop.Size size)
    {
        SpecificRodCollection newSpecificRodCollection = CreateInstance<SpecificRodCollection>();
        newSpecificRodCollection.stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Rod][size];
        newSpecificRodCollection.rodCollection = DefaultResourceHolder.DefaultRodCollection;
        newSpecificRodCollection.BuyStock(DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Rod]);
        return newSpecificRodCollection;
    }

    protected override SpecificRod CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificRod.CreateRandom(powerLevel, budgetRange, rodCollection);
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability, RodCollection rodCollection, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Wand][shop.size];

        if (rodCollection == null)
            rodCollection = DefaultResourceHolder.DefaultRodCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Rod];

        shop.specificRodCollection = Create(stockAvailability, rodCollection, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(stockAvailability) + jsonSplitter[0];
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
        stockAvailability = IntStratRanges.CreateFromJsonString(splitJsonString[1]);
        rodCollection = RodCollection.Load(splitJsonString[2]);

        specificItems = new SpecificRod[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificRod.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
