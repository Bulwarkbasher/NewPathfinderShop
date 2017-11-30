using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWandCollection : SpecificItemCollection<SpecificWand, SpecificWandCollection, SpellCollection>
{
    protected override SpecificWand CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        return SpecificWand.CreateRandom(powerLevel, ingredient, budget);
    }

    protected override SpellCollection GetIngredient(Shop shop)
    {
        return shop.SpellCollection;
    }

    protected override Shop.StockType GetStockType()
    {
        return Shop.StockType.Wand;
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificWandCollection = this;
    }
}
