using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificPotionCollection : SpecificItemCollection<SpecificPotion, SpecificPotionCollection, SpellCollection>
{
    protected override SpecificPotion CreateRandomSpecificItem(JsonableSelectedEnumSetting powerLevel, FloatRange budget)
    {
        return SpecificPotion.CreateRandom(powerLevel, ingredient, budget);
    }

    protected override SpellCollection GetIngredient(Shop shop)
    {
        return shop.SpellCollection;
    }

    protected override string GetStockType()
    {
        return "Potion";
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificPotionCollection = this;
    }
}
