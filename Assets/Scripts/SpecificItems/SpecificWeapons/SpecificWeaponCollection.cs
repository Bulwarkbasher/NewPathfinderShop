using UnityEngine;
using System;

public class SpecificWeaponCollection : SpecificItemCollection<SpecificWeapon, SpecificWeaponCollection, WeaponQualityConstraintsMatrix>
{
    protected override SpecificWeapon CreateRandomSpecificItem (SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificWeapon.CreateRandom(powerLevel, ingredient, budgetRange);
    }

    protected override WeaponQualityConstraintsMatrix GetIngredient(Shop shop)
    {
        return shop.WeaponQualityConstraintsMatrix;
    }

    protected override Shop.StockType GetStockType()
    {
        return Shop.StockType.Weapon;
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificWeaponCollection = this;
    }
}
