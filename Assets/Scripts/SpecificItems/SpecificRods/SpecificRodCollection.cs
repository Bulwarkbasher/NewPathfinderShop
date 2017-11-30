using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRodCollection : SpecificItemCollection<SpecificRod, SpecificRodCollection, RodCollection>
{
    protected override SpecificRod CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificRod.CreateRandom(powerLevel, budgetRange, ingredient);
    }

    protected override RodCollection GetIngredient(Shop shop)
    {
        return shop.RodCollection;
    }

    protected override Shop.StockType GetStockType()
    {
        return Shop.StockType.Rod;
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificRodCollection = this;
    }
}
