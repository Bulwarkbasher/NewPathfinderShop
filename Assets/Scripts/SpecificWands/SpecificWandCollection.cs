using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWandCollection : SpecificItemCollection<SpecificWand>
{
    public static SpecificWandCollection Create(Availability stockAvailability, SpellCollection availableSpells)
    {
        return CreateInstance<SpecificWandCollection>();
    }

    protected override SpecificWand GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificWand.CreateRandom();
    }

    public static void AddToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= Shop.StockType.Wand;

        if (stockAvailability == null)
            stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[Shop.StockType.Wand][shop.size];

        if (availableSpells == null)
            availableSpells = DefaultResourceHolder.DefaultSpellCollection;

        shop.specificWandCollection = Create(stockAvailability, availableSpells);
    }

    public static string GetJsonString(SpecificWandCollection specificWandCollection)
    {
        return "";
    }

    public static SpecificWandCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificWandCollection>();
    }
}
