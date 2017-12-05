using UnityEngine;
using System;

public class SpecificWeaponCollection : SpecificItemCollection<SpecificWeapon, SpecificWeaponCollection, WeaponQualityConstraintsMatrix>
{
    protected override SpecificWeapon CreateRandomSpecificItem (EnumValue powerLevel, FloatRange budgetRange)
    {
        return SpecificWeapon.CreateRandom(powerLevel, ingredient, budgetRange);
    }

    protected override WeaponQualityConstraintsMatrix GetIngredient(Shop shop)
    {
        return shop.WeaponQualityConstraintsMatrix;
    }

    protected override string GetStockType()
    {
        return "Weapon";
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificWeaponCollection = this;
    }
}
