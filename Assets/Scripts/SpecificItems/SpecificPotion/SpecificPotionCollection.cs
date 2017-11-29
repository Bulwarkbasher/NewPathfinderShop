using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificPotionCollection : SpecificItemCollection<SpecificPotion, SpecificPotionCollection>
{
    public SpellCollection spellCollection;

    public static SpecificPotionCollection Create(IntRangePerPowerLevel stockAvailability, SpellCollection availableSpells, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificPotionCollection newSpecificPotionCollection = CreateInstance<SpecificPotionCollection>();
        newSpecificPotionCollection.stockAvailability = stockAvailability;
        newSpecificPotionCollection.spellCollection = availableSpells;
        newSpecificPotionCollection.BuyStock(perPowerLevelItemBudgetRange);
        return newSpecificPotionCollection;
    }

    public static SpecificPotionCollection Create(string shopSize)
    {
        SpecificPotionCollection newSpecificPotionCollection = CreateInstance<SpecificPotionCollection>();
        newSpecificPotionCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Potion][shopSize];
        newSpecificPotionCollection.spellCollection = Campaign.SpellCollection;
        newSpecificPotionCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Potion]);
        return newSpecificPotionCollection;
    }

    protected override SpecificPotion CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        return SpecificPotion.CreateRandom(powerLevel, spellCollection, budget);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, SpellCollection availableSpells, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Potion;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Potion][shop.size];

        if (availableSpells == null)
            availableSpells = Campaign.SpellCollection;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Potion];

        shop.specificPotionCollection = Create(stockAvailability, availableSpells, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
        jsonString += spellCollection.name + jsonSplitter[0];

        for (int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificPotion.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntRangePerPowerLevel.Load(splitJsonString[1]);
        spellCollection = SpellCollection.Load(splitJsonString[2]);

        specificItems = new SpecificPotion[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificPotion.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
