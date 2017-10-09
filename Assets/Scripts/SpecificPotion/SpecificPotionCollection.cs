using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificPotionCollection : SpecificItemCollection<SpecificPotion>
{
    public static SpecificPotionCollection Create(Availability stockAvailability, SpellCollection availableSpells)
    {
        return CreateInstance<SpecificPotionCollection>();
    }

    protected override SpecificPotion GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
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

    public static string GetJsonString(SpecificPotionCollection specificPotionCollection)
    {
        return "";
    }

    public static SpecificPotionCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificPotionCollection>();
    }
}
