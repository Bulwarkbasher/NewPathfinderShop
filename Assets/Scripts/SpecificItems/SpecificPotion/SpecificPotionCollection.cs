using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificPotionCollection : SpecificItemCollection<SpecificPotion, SpecificPotionCollection, SpellCollection>
{
    protected override SpecificPotion CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budget)
    {
        return SpecificPotion.CreateRandom(powerLevel, ingredient, budget);
    }

    protected override SpellCollection GetIngredient(Shop shop)
    {
        return shop.SpellCollection;
    }

    protected override Shop.StockType GetStockType()
    {
        return Shop.StockType.Potion;
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificPotionCollection = this;
    }
}
