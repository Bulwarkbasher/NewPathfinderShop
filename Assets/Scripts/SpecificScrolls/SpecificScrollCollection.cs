using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScrollCollection : SpecificItemCollection<SpecificScroll, SpecificScrollCollection>
{
    public static SpecificScrollCollection Create (IntStratRanges stockAvailability, SpellCollection spellCollection)
    {
        return CreateInstance<SpecificScrollCollection> ();
    }

    protected override SpecificScroll CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificScroll.CreateRandom ();
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= Shop.StockType.Scroll;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Scroll][shop.size];

        if (availableSpells == null)
            availableSpells = DefaultResourceHolder.DefaultSpellCollection;

        shop.specificScrollCollection = Create(stockAvailability, availableSpells);
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
