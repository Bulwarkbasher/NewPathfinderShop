using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScrollCollection : SpecificItemCollection<SpecificScroll, SpecificScrollCollection>
{
    public SpellCollection spellCollection;

    public static SpecificScrollCollection Create(IntRangePerPowerLevel stockAvailability, SpellCollection availableSpells, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificScrollCollection newSpecificScrollCollection = CreateInstance<SpecificScrollCollection>();
        newSpecificScrollCollection.stockAvailability = stockAvailability;
        newSpecificScrollCollection.spellCollection = availableSpells;
        newSpecificScrollCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificScrollCollection;
    }

    public static SpecificScrollCollection Create(string shopSize)
    {
        SpecificScrollCollection newSpecificScrollCollection = CreateInstance<SpecificScrollCollection>();
        newSpecificScrollCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Scroll][shopSize];
        newSpecificScrollCollection.spellCollection = Campaign.SpellCollection;
        newSpecificScrollCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Scroll]);
        return newSpecificScrollCollection;
    }

    protected override SpecificScroll CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        return SpecificScroll.CreateRandom(powerLevel, spellCollection, budget);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, SpellCollection availableSpells, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Scroll;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Scroll][shop.size];

        if (availableSpells == null)
            availableSpells = Campaign.SpellCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Scroll];

        shop.specificScrollCollection = Create(stockAvailability, availableSpells, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
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
        stockAvailability = IntRangePerPowerLevel.Load(splitJsonString[1]);
        spellCollection = SpellCollection.Load(splitJsonString[2]);

        specificItems = new SpecificScroll[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificScroll.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
