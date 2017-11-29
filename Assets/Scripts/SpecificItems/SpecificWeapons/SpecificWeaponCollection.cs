using UnityEngine;
using System;

[CreateAssetMenu]
public class SpecificWeaponCollection : SpecificItemCollection<SpecificWeapon, SpecificWeaponCollection>
{
    public WeaponQualityConstraintsMatrix matrix;

    public static SpecificWeaponCollection Create (IntRangePerPowerLevel stockAvailability, WeaponQualityConstraintsMatrix matrix, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificWeaponCollection newSpecificWeaponCollection = CreateInstance<SpecificWeaponCollection>();
        newSpecificWeaponCollection.stockAvailability = stockAvailability;
        newSpecificWeaponCollection.matrix = matrix;
        newSpecificWeaponCollection.BuyStock(perPowerLevelItemBudgetRange);

        return newSpecificWeaponCollection;
    }

    public static SpecificWeaponCollection Create (string shopSize)
    {
        SpecificWeaponCollection newSpecificWeaponCollection = CreateInstance<SpecificWeaponCollection>();
        newSpecificWeaponCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Weapon][shopSize];
        newSpecificWeaponCollection.matrix = Campaign.WeaponQualityConstraintsMatrix;
        newSpecificWeaponCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Weapon]);

        return newSpecificWeaponCollection;
    }
    
    protected override SpecificWeapon CreateRandomSpecificItem (SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificWeapon.CreateRandom(powerLevel, matrix, budgetRange);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, WeaponQualityConstraintsMatrix matrix, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Weapon;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Weapon][shop.size];

        if (matrix == null)
            matrix = Campaign.WeaponQualityConstraintsMatrix;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Weapon];

        shop.specificWeaponCollection = Create(stockAvailability, matrix, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
        jsonString += matrix.name + jsonSplitter[0];

        for(int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificWeapon.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntRangePerPowerLevel.Load(splitJsonString[1]);
        matrix = WeaponQualityConstraintsMatrix.Load(splitJsonString[2]);
        
        specificItems = new SpecificWeapon[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificWeapon.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
