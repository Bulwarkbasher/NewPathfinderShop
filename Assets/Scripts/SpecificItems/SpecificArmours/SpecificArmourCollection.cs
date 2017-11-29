using UnityEngine;
using System;

[CreateAssetMenu]
public class SpecificArmourCollection : SpecificItemCollection<SpecificArmour, SpecificArmourCollection>
{
    public ArmourQualityConstraintsMatrix matrix;

    public static SpecificArmourCollection Create(IntRangePerPowerLevel stockAvailability, ArmourQualityConstraintsMatrix matrix, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SpecificArmourCollection newSpecificArmourCollection = CreateInstance<SpecificArmourCollection>();
        newSpecificArmourCollection.stockAvailability = stockAvailability;
        newSpecificArmourCollection.matrix = matrix;
        newSpecificArmourCollection.BuyStock(perPowerLevelItemBudgetRange);

        return newSpecificArmourCollection;
    }

    public static SpecificArmourCollection Create(string shopSize)
    {
        SpecificArmourCollection newSpecificArmourCollection = CreateInstance<SpecificArmourCollection>();
        newSpecificArmourCollection.stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Armour][shopSize];
        newSpecificArmourCollection.matrix = Campaign.ArmourQualityConstraintsMatrix;
        newSpecificArmourCollection.BuyStock(Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Armour]);

        return newSpecificArmourCollection;
    }

    protected override SpecificArmour CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange)
    {
        return SpecificArmour.CreateRandom(powerLevel, matrix, budgetRange);
    }

    public static void AddToShop(Shop shop, IntRangePerPowerLevel stockAvailability, ArmourQualityConstraintsMatrix matrix, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        shop.stockTypes |= Shop.StockType.Armour;

        if (stockAvailability == null)
            stockAvailability = Campaign.AvailabilityPerShopSizePerStockType[Shop.StockType.Armour][shop.size];

        if (matrix == null)
            matrix = Campaign.ArmourQualityConstraintsMatrix;

        if (perPowerLevelItemBudgetRange == null)
            perPowerLevelItemBudgetRange = Campaign.BudgetRangePerPowerLevelPerStockType[Shop.StockType.Armour];

        shop.specificArmourCollection = Create(stockAvailability, matrix, perPowerLevelItemBudgetRange);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += stockAvailability.name + jsonSplitter[0];
        jsonString += matrix.name + jsonSplitter[0];

        for (int i = 0; i < specificItems.Length; i++)
        {
            jsonString += SpecificArmour.GetJsonString(specificItems[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        stockAvailability = IntRangePerPowerLevel.Load(splitJsonString[1]);
        matrix = ArmourQualityConstraintsMatrix.Load(splitJsonString[2]);

        specificItems = new SpecificArmour[splitJsonString.Length - 3];
        for (int i = 0; i < specificItems.Length; i++)
        {
            specificItems[i] = SpecificArmour.CreateFromJsonString(splitJsonString[i + 3]);
        }
    }
}
