using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScrollCollection : SpecificItemCollection<SpecificScroll, SpecificScrollCollection, SpellCollection>
{
    protected override SpecificScroll CreateRandomSpecificItem(EnumValue powerLevel, FloatRange budget)
    {
        return SpecificScroll.CreateRandom(powerLevel, ingredient, budget);
    }

    protected override SpellCollection GetIngredient(Shop shop)
    {
        return shop.SpellCollection;
    }

    protected override string GetStockType()
    {
        return "Scroll";
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificScrollCollection = this;
    }
}
