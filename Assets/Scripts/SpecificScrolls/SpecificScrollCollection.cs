using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScrollCollection : SpecificItemCollection<SpecificScroll>
{
    public static SpecificScrollCollection Create (Availability stockAvailability, SpellCollection spellCollection)
    {
        return CreateInstance<SpecificScrollCollection> ();
    }

    protected override SpecificScroll GetRandomSpecificItem (SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificScroll.CreateRandom ();
    }

    public static void AddToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= Shop.StockType.Scroll;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Scroll][shop.size];

        if (availableSpells == null)
            availableSpells = DefaultResourceHolder.DefaultSpellCollection;

        shop.specificScrollCollection = Create(stockAvailability, availableSpells);
    }

    public static string GetJsonString(SpecificScrollCollection specificScrollCollection)
    {
        return "";
    }

    public static SpecificScrollCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificScrollCollection>();
    }
}
