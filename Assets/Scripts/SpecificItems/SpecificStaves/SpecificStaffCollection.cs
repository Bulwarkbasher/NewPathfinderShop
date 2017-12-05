using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaffCollection : SpecificItemCollection<SpecificStaff, SpecificStaffCollection, StaffCollection>
{
    protected override SpecificStaff CreateRandomSpecificItem(EnumValue powerLevel, FloatRange budgetRange)
    {
        return SpecificStaff.CreateRandom(powerLevel, budgetRange, ingredient);
    }

    protected override StaffCollection GetIngredient(Shop shop)
    {
        return shop.StaffCollection;
    }

    protected override string GetStockType()
    {
        return "Staff";
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificStaffCollection = this;
    }
}
