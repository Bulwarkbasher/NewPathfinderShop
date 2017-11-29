using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWandCollection : SpecificItemCollection<SpecificWand, SpecificWandCollection>
{
    public SpellCollection spellCollection;

    public static SpecificWandCollection Create(IntRangePerPowerLevel stockAvailability, SpellCollection availableSpells, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificWandCollection newSpecificWandCollection = CreateInstance<SpecificWandCollection>();
        newSpecificWandCollection.stockAvailability = stockAvailability;
        newSpecificWandCollection.spellCollection = availableSpells;
        newSpecificWandCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificWandCollection;
    }

    public static SpecificWandCollection Create (string shopSize)
    {
        SpecificWandCollection newSpecificWandCollection = CreateInstance<SpecificWandCollection>();
        newSpecificWandCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Wand][shopSize];
        newSpecificWandCollection.spellCollection = Campaign.SpellCollection;
        newSpecificWandCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Wand]);
        return newSpecificWandCollection;
    }

    protected override SpecificWand CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        return SpecificWand.CreateRandom(powerLevel, spellCollection, budget);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, SpellCollection availableSpells, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Wand][shop.size];

        if (availableSpells == null)
            availableSpells = Campaign.SpellCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Wand];

        shop.specificWandCollection = Create(stockAvailability, availableSpells, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
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
        stockAvailability = IntRangePerPowerLevel.Load(splitJsonString[1]);
        spellCollection = SpellCollection.Load(splitJsonString[2]);

        specificItems = new SpecificWand[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificWand.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
