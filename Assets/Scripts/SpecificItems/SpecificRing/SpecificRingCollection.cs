using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRingCollection : SpecificItemCollection<SpecificRing, SpecificRingCollection, RingCollection>
{
    protected override SpecificRing CreateRandomSpecificItem(EnumValue powerLevel, FloatRange budgetRange)
    {
        return SpecificRing.CreateRandom(powerLevel, budgetRange, ingredient);
    }

    protected override RingCollection GetIngredient(Shop shop)
    {
        return shop.RingCollection;
    }

    protected override string GetStockType()
    {
        return "Ring";
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificRingCollection = this;
    }
}