using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWandCollection : SpecificItemCollection<SpecificWand, SpecificWandCollection>
{
    public SpellCollection spellCollection;

    public static SpecificWandCollection Create(IntStratRanges stockAvailability, SpellCollection availableSpells)
    {
        return CreateInstance<SpecificWandCollection>();
    }

    protected override SpecificWand CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        //return SpecificWand.CreateRandom();
        return CreateInstance<SpecificWand> ();
    }

    public static void AddToShop(Shop shop, IntStratRanges stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Wand][shop.size];

        if (availableSpells == null)
            availableSpells = DefaultResourceHolder.DefaultSpellCollection;

        shop.specificWandCollection = Create(stockAvailability, availableSpells);
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
