using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWondrousCollection : SpecificItemCollection<SpecificWondrous, SpecificWondrousCollection, WondrousCollection>
{
    protected override SpecificWondrous CreateRandomSpecificItem(JsonableSelectedEnumSetting powerLevel, FloatRange budgetRange)
    {
        return SpecificWondrous.CreateRandom(powerLevel, budgetRange, ingredient);
    }

    protected override WondrousCollection GetIngredient(Shop shop)
    {
        return shop.WondrousCollection;
    }

    protected override string GetStockType()
    {
        return "Wondrous";
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificWondrousCollection = this;
    }
}
