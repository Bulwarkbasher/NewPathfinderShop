using UnityEngine;

public abstract class SpecificItemCollection<TSpecificItem> : ScriptableObject
    where TSpecificItem : SpecificItem
{
    public Availability stockAvailability;
    public TSpecificItem[] specificItems = new TSpecificItem[0];

    public int GetTotalCost ()
    {
        int total = 0;
        for (int i = 0; i < specificItems.Length; i++)
        {
            total += specificItems[i].cost;
        }
        return total;
    }

    public int GetTotalCost(SpecificItem.PowerLevel powerLevel)
    {
        int total = 0;
        for (int i = 0; i < specificItems.Length; i++)
        {
            if (specificItems[i].powerLevel == powerLevel)
                total += specificItems[i].cost;
        }
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

    public void AddRandom(SpecificItem.PowerLevel powerLevel, int budget)
    {
        TSpecificItem[] newSpecificWeapons = new TSpecificItem[specificItems.Length + 1];
        for (int i = 0; i < specificItems.Length; i++)
        {
            newSpecificWeapons[i] = specificItems[i];
        }
        newSpecificWeapons[specificItems.Length] = GetRandomSpecificItem (powerLevel, budget);
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

    public void ReplaceWithRandom (int index, SpecificItem.PowerLevel powerLevel, int budget)
    {
        specificItems[index] = GetRandomSpecificItem (powerLevel, budget);
    }

    public void Restock(RestockSettings restockSettings)
    {
        SellStock(restockSettings);

        BuyStock();
    }

    protected void SellStock(RestockSettings restockSettings)
    {
        int totalCost = GetTotalCost();
        float percentToRestock = restockSettings.percent.Random() / 100f;
        int restockAmount = Mathf.FloorToInt(totalCost * percentToRestock);
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

    protected void BuyStock()
    {
        BuyStockAtPowerLevel(SpecificItem.PowerLevel.Minor, stockAvailability.stock.minor, stockAvailability.budget.minor, stockAvailability.budgetVariation);
        BuyStockAtPowerLevel(SpecificItem.PowerLevel.Medium, stockAvailability.stock.medium, stockAvailability.budget.medium, stockAvailability.budgetVariation);
        BuyStockAtPowerLevel(SpecificItem.PowerLevel.Major, stockAvailability.stock.major, stockAvailability.budget.major, stockAvailability.budgetVariation);
    }

    protected void BuyStockAtPowerLevel(SpecificItem.PowerLevel powerLevel, Range stockRange, Range budgetRange, float budgetVariation)
    {
        int desiredCount = stockRange.Random();
        int currentCount = GetTotalCount(powerLevel);
        int desiredTotalBudget = budgetRange.Random();
        int currentBudget = GetTotalCost(powerLevel);

        int requiredStockCount = desiredCount - currentCount;
        if (requiredStockCount > 0)
        {
            int requiredStockBudget = desiredTotalBudget - currentBudget;

            if (requiredStockBudget > 0)
            {
                int meanBudget = Mathf.FloorToInt(requiredStockBudget / requiredStockCount);

                for (int i = 0; i < requiredStockCount; i++)
                {
                    int specificBudget = Mathf.FloorToInt(meanBudget * Random.Range(-budgetVariation, budgetVariation));

                    TSpecificItem randomSpecificItem = GetRandomSpecificItem(powerLevel, specificBudget);
                    Add(randomSpecificItem);
                }
            }
        }
    }

    protected abstract TSpecificItem GetRandomSpecificItem (SpecificItem.PowerLevel powerLevel, int budget);
}
