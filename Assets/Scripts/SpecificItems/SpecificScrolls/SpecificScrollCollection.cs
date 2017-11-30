using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScrollCollection : SpecificItemCollection<SpecificScroll, SpecificScrollCollection, SpellCollection>
{
    protected override SpecificScroll CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        return SpecificScroll.CreateRandom(powerLevel, ingredient, budget);
    }

    protected override SpellCollection GetIngredient(Shop shop)
    {
        return shop.SpellCollection;
    }

    protected override Shop.StockType GetStockType()
    {
        return Shop.StockType.Scroll;
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificScrollCollection = this;
    }
}
