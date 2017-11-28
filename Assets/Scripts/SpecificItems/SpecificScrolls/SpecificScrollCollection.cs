using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScrollCollection : SpecificItemCollection<SpecificScroll, SpecificScrollCollection>
{
    public SpellCollection spellCollection;

    public static SpecificScrollCollection Create(IntStratRanges stockAvailability, SpellCollection availableSpells, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        SpecificScrollCollection newSpecificScrollCollection = CreateInstance<SpecificScrollCollection>();
        newSpecificScrollCollection.stockAvailability = stockAvailability;
        newSpecificScrollCollection.spellCollection = availableSpells;
        newSpecificScrollCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificScrollCollection;
    }

    public static SpecificScrollCollection Create(Shop.Size size)
    {
        SpecificScrollCollection newSpecificScrollCollection = CreateInstance<SpecificScrollCollection>();
        newSpecificScrollCollection.stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Scroll][size];
        newSpecificScrollCollection.spellCollection = DefaultResourceHolder.DefaultSpellCollection;
        newSpecificScrollCollection.BuyStock(DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Scroll]);
        return newSpecificScrollCollection;
    }

    protected override SpecificScroll CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        return SpecificScroll.CreateRandom(powerLevel, spellCollection, budget);
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability, SpellCollection availableSpells, PerPowerLevelRange perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Scroll;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Scroll][shop.size];

        if (availableSpells == null)
            availableSpells = DefaultResourceHolder.DefaultSpellCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = DefaultResourceHolder.DefaultPerStockTypePerPowerLevelRange[Shop.StockType.Scroll];

        shop.specificScrollCollection = Create(stockAvailability, availableSpells, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(stockAvailability) + jsonSplitter[0];
        jsonString += spellCollection.name + jsonSplitter[0];

        for (int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificScroll.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntStratRanges.CreateFromJsonString(splitJsonString[1]);
        spellCollection = SpellCollection.Load(splitJsonString[2]);

        specificItems = new SpecificScroll[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificScroll.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
