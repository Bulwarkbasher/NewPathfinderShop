using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWandCollection : SpecificItemCollection<SpecificWand, SpecificWandCollection, SpellCollection>
{
    protected override SpecificWand CreateRandomSpecificItem(EnumValue powerLevel, FloatRange budget)
    {
        return SpecificWand.CreateRandom(powerLevel, ingredient, budget);
    }

    protected override SpellCollection GetIngredient(Shop shop)
    {
        return shop.SpellCollection;
    }

    protected override string GetStockType()
    {
        return "Wand";
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificWandCollection = this;
    }
}
