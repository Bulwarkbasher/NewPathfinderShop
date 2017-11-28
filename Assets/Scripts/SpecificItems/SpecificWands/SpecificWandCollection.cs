using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWandCollection : SpecificItemCollection<SpecificWand, SpecificWandCollection>
{
    public SpellCollection spellCollection;

    public static SpecificWandCollection Create(IntStratRanges stockAvailability, SpellCollection availableSpells, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        SpecificWandCollection newSpecificWandCollection = CreateInstance<SpecificWandCollection>();
        newSpecificWandCollection.stockAvailability = stockAvailability;
        newSpecificWandCollection.spellCollection = availableSpells;
        newSpecificWandCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificWandCollection;
    }

    public static SpecificWandCollection Create (Shop.Size size)
    {
        SpecificWandCollection newSpecificWandCollection = CreateInstance<SpecificWandCollection>();
        newSpecificWandCollection.stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Wand][size];
        newSpecificWandCollection.spellCollection = DefaultResourceHolder.DefaultSpellCollection;
        newSpecificWandCollection.BuyStock(DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Wand]);
        return newSpecificWandCollection;
    }

    protected override SpecificWand CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        return SpecificWand.CreateRandom(powerLevel, spellCollection, budget);
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability, SpellCollection availableSpells, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Wand][shop.size];

        if (availableSpells == null)
            availableSpells = DefaultResourceHolder.DefaultSpellCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Wand];

        shop.specificWandCollection = Create(stockAvailability, availableSpells, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(stockAvailability) + jsonSplitter[0];
        jsonString += spellCollection.name + jsonSplitter[0];

        for (int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificWand.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntStratRanges.CreateFromJsonString(splitJsonString[1]);
        spellCollection = SpellCollection.Load(splitJsonString[2]);

        specificItems = new SpecificWand[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificWand.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
