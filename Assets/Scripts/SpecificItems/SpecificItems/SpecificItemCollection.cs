using UnityEngine;

public abstract class SpecificItemCollection<TSpecificItem, TChild> : Jsonable<TChild>
    where TSpecificItem : SpecificItem<TSpecificItem>
    where TChild : Jsonable<TChild>
{
    public IntRangePerPowerLevel stockAvailability;
    public TSpecificItem[] specificItems = new TSpecificItem[0];

    public Shop GetShop ()
    {
        Settlement[] campaignSettlements = Campaign.Current.settlements;

        for (int i = 0; i < campaignSettlements.Length; i++)
        {
            Shop[] settlementShops = campaignSettlements[i].shops;

            for (int j = 0; j < settlementShops.Length; j++)
            {
                Shop shop = settlementShops[j];
                if (shop.specificArmourCollection == this)
                    return shop;
                if (shop.specificPotionCollection == this)
                    return shop;
                if (shop.specificRingCollection == this)
                    return shop;
                if (shop.specificRodCollection == this)
                    return shop;
                if (shop.specificScrollCollection == this)
                    return shop;
                if (shop.specificStaffCollection == this)
                    return shop;
                if (shop.specificWandCollection == this)
                    return shop;
                if (shop.specificWeaponCollection == this)
                    return shop;
                if (shop.specificWondrousCollection == this)
                    return shop;

            }
        }

        return null;
    }

    public Settlement GetSettlement()
    {
        Settlement[] campaignSettlements = Campaign.Current.settlements;

        for (int i = 0; i < campaignSettlements.Length; i++)
        {
            Shop[] settlementShops = campaignSettlements[i].shops;

            for (int j = 0; j < settlementShops.Length; j++)
            {
                Shop shop = settlementShops[j];
                if (shop.specificArmourCollection == this)
                    return campaignSettlements[i];
                if (shop.specificPotionCollection == this)
                    return campaignSettlements[i];
                if (shop.specificRingCollection == this)
                    return campaignSettlements[i];
                if (shop.specificRodCollection == this)
                    return campaignSettlements[i];
                if (shop.specificScrollCollection == this)
                    return campaignSettlements[i];
                if (shop.specificStaffCollection == this)
                    return campaignSettlements[i];
                if (shop.specificWandCollection == this)
                    return campaignSettlements[i];
                if (shop.specificWeaponCollection == this)
                    return campaignSettlements[i];
                if (shop.specificWondrousCollection == this)
                    return campaignSettlements[i];

            }
        }

        return null;
    }

    public float GetTotalCost ()
    {
        float total = 0;
        for (int i = 0; i < specificItems.Length; i++)
        {
            total += specificItems[i].cost;
        }
        return total;
    }

    public float GetTotalCost(SpecificItem.PowerLevel powerLevel)
    {
        float total = 0;
        for (int i = 0; i < specificItems.Length; i++)
        {
            if (specificItems[i].powerLevel == powerLevel)
                total += specificItems[i].cost;
        }
        return total;
    }

    public float GetMaxPossibleValue(FloatRangePerPowerLevel perPowerLevelBudgetRange)
    {
        float total = 0f;
        total += stockAvailability[SpecificItem.PowerLevel.Minor].max * perPowerLevelBudgetRange[SpecificItem.PowerLevel.Minor].max;
        total += stockAvailability[SpecificItem.PowerLevel.Medium].max * perPowerLevelBudgetRange[SpecificItem.PowerLevel.Medium].max;
        total += stockAvailability[SpecificItem.PowerLevel.Major].max * perPowerLevelBudgetRange[SpecificItem.PowerLevel.Major].max;
        return total;
    }

    public int GetTotalCount ()
    {
        return specificItems.Length;
    }

    public int GetTotalCount(SpecificItem.PowerLevel powerLevel)
    {
        int total = 0;
        for (int i = 0; i < specificItems.Length; i++)
        {
            if (specificItems[i].powerLevel == powerLevel)
                total++;
        }
        return total;
    }

    public void Add(TSpecificItem specificItem)
    {
        TSpecificItem[] newSpecificItems = new TSpecificItem[specificItems.Length + 1];
        for (int i = 0; i < specificItems.Length; i++)
        {
            newSpecificItems[i] = specificItems[i];
        }
        newSpecificItems[specificItems.Length] = specificItem;
        specificItems = newSpecificItems;
    }

    public void AddRandom(SpecificItem.PowerLevel powerLevel, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        TSpecificItem[] newSpecificWeapons = new TSpecificItem[specificItems.Length + 1];
        for (int i = 0; i < specificItems.Length; i++)
        {
            newSpecificWeapons[i] = specificItems[i];
        }
        newSpecificWeapons[specificItems.Length] = CreateRandomSpecificItem (powerLevel, perPowerLevelItemBudgetRange[powerLevel]);
        specificItems = newSpecificWeapons;
    }

    public void RemoveAt(int index)
    {
        TSpecificItem[] newSpecificItems = new TSpecificItem[specificItems.Length - 1];
        int newIndex, oldIndex;
        for (newIndex = 0, oldIndex = 0; oldIndex < specificItems.Length; oldIndex++, newIndex++)
        {
            if (oldIndex == index)
                oldIndex++;
            newSpecificItems[newIndex] = specificItems[oldIndex];
        }
        specificItems = newSpecificItems;
    }

    public void Replace (int index, TSpecificItem specificItem)
    {
        specificItems[index] = specificItem;
    }

    public void ReplaceWithRandom(int index, SpecificItem.PowerLevel powerLevel, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        specificItems[index] = CreateRandomSpecificItem (powerLevel, perPowerLevelItemBudgetRange[powerLevel]);
    }

    public void Restock(RestockSettings restockSettings, FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        SellStock(restockSettings);

        BuyStock(perPowerLevelItemBudgetRange);
    }

    protected void SellStock(RestockSettings restockSettings)
    {
        float totalCost = GetTotalCost();
        float percentToRestock = restockSettings.percent.Random() / 100f;
        float restockAmount = totalCost * percentToRestock;
        bool nothingSold = false;
        while (restockAmount > 0)
        {
            int origSellIndex = Random.Range(0, specificItems.Length);
            int currentSellIndex = origSellIndex;
            do
            {
                if (specificItems[currentSellIndex].cost < restockAmount)
                {
                    restockAmount -= specificItems[currentSellIndex].cost;
                    RemoveAt(currentSellIndex);
                    break;
                }
                currentSellIndex = (currentSellIndex + 1) % specificItems.Length;

                if (currentSellIndex == origSellIndex)
                    nothingSold = true;
            }
            while (currentSellIndex != origSellIndex);

            if (nothingSold)
                break;
        }
    }

    protected void BuyStock(FloatRangePerPowerLevel perPowerLevelItemBudgetRange)
    {
        BuyStockAtPowerLevel(SpecificItem.PowerLevel.Minor, stockAvailability[SpecificItem.PowerLevel.Minor], perPowerLevelItemBudgetRange[SpecificItem.PowerLevel.Minor]);
        BuyStockAtPowerLevel(SpecificItem.PowerLevel.Medium, stockAvailability[SpecificItem.PowerLevel.Medium], perPowerLevelItemBudgetRange[SpecificItem.PowerLevel.Medium]);
        BuyStockAtPowerLevel(SpecificItem.PowerLevel.Major, stockAvailability[SpecificItem.PowerLevel.Major], perPowerLevelItemBudgetRange[SpecificItem.PowerLevel.Major]);
    }

    protected void BuyStockAtPowerLevel(SpecificItem.PowerLevel powerLevel, IntRange stockRange, FloatRange itemBudgetRange)
    {
        int desiredCount = stockRange.Random();
        int currentCount = GetTotalCount(powerLevel);
        float currentBudget = GetTotalCost(powerLevel);

        int requiredStockCount = desiredCount - currentCount;
        float desiredTotalBudget = itemBudgetRange.Mean * requiredStockCount;

        if (requiredStockCount > 0)
        {
            float requiredStockBudget = desiredTotalBudget - currentBudget;

            if (requiredStockBudget > 0)
            {
                for (int i = 0; i < requiredStockCount; i++)
                {
                    TSpecificItem randomSpecificItem = CreateRandomSpecificItem(powerLevel, itemBudgetRange);
                    Add(randomSpecificItem);
                }
            }
        }
    }

    protected abstract TSpecificItem CreateRandomSpecificItem(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange);
}
