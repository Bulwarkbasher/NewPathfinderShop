using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaffCollection : SpecificItemCollection<SpecificStaff, SpecificStaffCollection, StaffCollection>
{
    protected override SpecificStaff CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificStaff.CreateRandom(powerLevel, budgetRange, ingredient);
    }

    protected override StaffCollection GetIngredient(Shop shop)
    {
        return shop.StaffCollection;
    }

    protected override Shop.StockType GetStockType()
    {
        return Shop.StockType.Staff;
    }

    protected override void SetShopCollection(Shop shop)
    {
        shop.specificStaffCollection = this;
    }
}
