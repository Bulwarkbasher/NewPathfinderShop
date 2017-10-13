using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificPotionCollection : SpecificItemCollection<SpecificPotion, SpecificPotionCollection>
{
    public static SpecificPotionCollection Create(Availability stockAvailability, SpellCollection availableSpells)
    {
        return CreateInstance<SpecificPotionCollection>();
    }

    protected override SpecificPotion GetRandomSpecificItem(SpecificPotion.PowerLevel powerLevel, int budget)
    {
        return SpecificPotion.CreateRandom();
    }

    public static void AddToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= Shop.StockType.Potion;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Potion][shop.size];

        if (availableSpells == null)
            availableSpells = DefaultResourceHolder.DefaultSpellCollection;

        shop.specificPotionCollection = Create(stockAvailability, availableSpells);
    }

    protected override void SetupFromSplitJsonString (string[] splitJsonString)
    {
        throw new System.NotImplementedException ();
    }

    protected override string ConvertToJsonString (string[] jsonSplitter)
    {
        throw new System.NotImplementedException ();
    }
}
