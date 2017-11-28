using UnityEngine;
using System;

public class Shop : Jsonable<Shop>
{
    public enum Size    // TODO NEXT: to data
    {
        Stall,
        Boutique,
        Outlet,
        Emporium,
    }

    [Flags]
    public enum StockType
    {
        Armour = 1 << 0,
        Potion = 1 << 1,
        Ring = 1 << 2,
        Rod = 1 << 3,
        Scroll = 1 << 4,
        Staff = 1 << 5,
        Wand = 1 << 6,
        Weapon = 1 << 7,
        Wondrous = 1 << 8,
    }

    public string notes;
    public Size size;
    public StockType stockTypes;
    public PerSizeRestockFrequencyModifiers perSizeRestockFrequencyModifiers;
    public int daysSinceLastRestock;
    public PerSizeReadyCash perSizeReadyCash;
    public float totalCash;
    public PerStockTypePerPowerLevelRange perStockTypePerPowerLevelRange;

    public SpecificArmourCollection specificArmourCollection;
    public SpecificPotionCollection specificPotionCollection;
    public SpecificRingCollection specificRingCollection;
    public SpecificRodCollection specificRodCollection;
    public SpecificScrollCollection specificScrollCollection;
    public SpecificStaffCollection specificStaffCollection;
    public SpecificWandCollection specificWandCollection;
    public SpecificWeaponCollection specificWeaponCollection;
    public SpecificWondrousCollection specificWondrousCollection;

    public float RestockFrequencyModifier
    {
        get { return perSizeRestockFrequencyModifiers[size]; }
    }

    public int ReadyCash
    {
        get { return perSizeReadyCash[size]; }
    }

    public static Shop Create(string name, string notes, Size shopSize, PerSizeRestockFrequencyModifiers restockFrequencyModifiers, PerSizeReadyCash readyCash)
    {
        Shop newShop = CreateInstance<Shop>();
        newShop.name = name;
        newShop.notes = notes;
        newShop.size = shopSize;
        newShop.perSizeRestockFrequencyModifiers = restockFrequencyModifiers;
        newShop.perSizeReadyCash = readyCash;
        newShop.specificWeaponCollection = CreateInstance<SpecificWeaponCollection> ();
        newShop.specificArmourCollection = CreateInstance<SpecificArmourCollection>();
        newShop.specificScrollCollection = CreateInstance<SpecificScrollCollection>();
        newShop.specificWandCollection = CreateInstance<SpecificWandCollection>();
        newShop.specificPotionCollection = CreateInstance<SpecificPotionCollection>();
        newShop.specificStaffCollection = CreateInstance<SpecificStaffCollection>();
        newShop.specificRodCollection = CreateInstance<SpecificRodCollection>();
        newShop.specificWondrousCollection = CreateInstance<SpecificWondrousCollection>();
        return newShop;
    }

    public static Shop Create (string name, string notes, Size shopSize)
    {
        PerSizeRestockFrequencyModifiers restockFrequencyModifier = DefaultResourceHolder.DefaultPerSizeRestockFrequencyModifiers;
        PerSizeReadyCash readyCash = DefaultResourceHolder.DefaultPerSizeReadyCash;
        return Create (name, notes, shopSize, restockFrequencyModifier, readyCash);
    }

    public Settlement GetLocation ()
    {
        Settlement[] campaignSettlements = Campaign.Current.settlements;

        for (int i = 0; i < campaignSettlements.Length; i++)
        {
            Shop[] settlementShops = campaignSettlements[i].shops;

            for (int j = 0; j < settlementShops.Length; j++)
            {
                if (settlementShops[j] == this)
                    return campaignSettlements[i];
            }
        }

        return null;
    }

    public void AddToTotalCash (int additionalCash)
    {
        totalCash += additionalCash;
    }

    public void SubtractFromTotalCash (int removedCash)
    {
        totalCash -= removedCash;
    }

    public void PassTime (int daysPassed)
    {
        int totalDaysSinceLastRestock = daysPassed + daysSinceLastRestock;
        RestockSettings restockSettings = GetLocation ().RestockSettings;

        int daysUntilRestock = Mathf.FloorToInt(restockSettings.days.Random() * RestockFrequencyModifier);
        while (totalDaysSinceLastRestock - daysUntilRestock > 0)
        {
            Restock (restockSettings);
            totalDaysSinceLastRestock -= daysUntilRestock;
            daysUntilRestock = Mathf.FloorToInt(restockSettings.days.Random() * RestockFrequencyModifier);
        }

        daysSinceLastRestock = totalDaysSinceLastRestock;

        totalCash = GetMaxPossibleStockValue () - GetTotalStockValue () + ReadyCash;
    }

    protected void Restock(RestockSettings restockSettings)
    {
        if ((stockTypes | StockType.Armour) == stockTypes)
            specificArmourCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Armour]);
        if ((stockTypes | StockType.Potion) == stockTypes)
            specificPotionCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Potion]);
        if ((stockTypes | StockType.Ring) == stockTypes)
            specificRingCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Ring]);
        if ((stockTypes | StockType.Rod) == stockTypes)
            specificRodCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Rod]);
        if ((stockTypes | StockType.Scroll) == stockTypes)
            specificScrollCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Scroll]);
        if ((stockTypes | StockType.Staff) == stockTypes)
            specificStaffCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Staff]);
        if ((stockTypes | StockType.Wand) == stockTypes)
            specificWandCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Wand]);
        if ((stockTypes | StockType.Weapon) == stockTypes)
            specificWeaponCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Weapon]);
        if ((stockTypes | StockType.Wondrous) == stockTypes)
            specificWondrousCollection.Restock(restockSettings, perStockTypePerPowerLevelRange[StockType.Wondrous]);       
    }

    protected float GetTotalStockValue ()
    {
        float totalStockValue = 0;
        totalStockValue += specificArmourCollection.GetTotalCost ();
        totalStockValue += specificPotionCollection.GetTotalCost();
        totalStockValue += specificRingCollection.GetTotalCost();
        totalStockValue += specificRodCollection.GetTotalCost();
        totalStockValue += specificScrollCollection.GetTotalCost();
        totalStockValue += specificStaffCollection.GetTotalCost();
        totalStockValue += specificWandCollection.GetTotalCost();
        totalStockValue += specificWeaponCollection.GetTotalCost();
        totalStockValue += specificWondrousCollection.GetTotalCost();
        return totalStockValue;
    }

    protected float GetMaxPossibleStockValue ()
    {
        float maxStockValue = 0;
        maxStockValue += specificArmourCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Armour]);
        maxStockValue += specificPotionCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Potion]);
        maxStockValue += specificRingCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Ring]);
        maxStockValue += specificRodCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Rod]);
        maxStockValue += specificScrollCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Scroll]);
        maxStockValue += specificStaffCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Staff]);
        maxStockValue += specificWandCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Wand]);
        maxStockValue += specificWeaponCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Weapon]);
        maxStockValue += specificWondrousCollection.GetMaxPossibleValue(perStockTypePerPowerLevelRange[StockType.Wondrous]);
        return maxStockValue;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)size) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)stockTypes) + jsonSplitter[0];
        jsonString += perSizeRestockFrequencyModifiers.name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(daysSinceLastRestock) + jsonSplitter[0];
        jsonString += perSizeReadyCash.name + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(totalCash) + jsonSplitter[0];
        jsonString += perStockTypePerPowerLevelRange.name + jsonSplitter[0];

        jsonString += SpecificArmourCollection.GetJsonString(specificArmourCollection) + jsonSplitter[0];
        jsonString += SpecificPotionCollection.GetJsonString(specificPotionCollection) + jsonSplitter[0];
        jsonString += SpecificRingCollection.GetJsonString(specificRingCollection) + jsonSplitter[0];
        jsonString += SpecificRodCollection.GetJsonString(specificRodCollection) + jsonSplitter[0];
        jsonString += SpecificScrollCollection.GetJsonString(specificScrollCollection) + jsonSplitter[0];
        jsonString += SpecificStaffCollection.GetJsonString(specificStaffCollection) + jsonSplitter[0];
        jsonString += SpecificWandCollection.GetJsonString(specificWandCollection) + jsonSplitter[0];
        jsonString += SpecificWeaponCollection.GetJsonString(specificWeaponCollection) + jsonSplitter[0];
        jsonString += SpecificWondrousCollection.GetJsonString(specificWondrousCollection) + jsonSplitter[0];
        
        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        notes = CreateStringFromSafeJson(splitJsonString[1]);
        size = (Size)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);
        stockTypes = (StockType)Wrapper<int>.CreateFromJsonString (splitJsonString[3]);
        perSizeRestockFrequencyModifiers = PerSizeRestockFrequencyModifiers.Load (splitJsonString[4]);
        daysSinceLastRestock = Wrapper<int>.CreateFromJsonString (splitJsonString[5]);
        perSizeReadyCash = PerSizeReadyCash.Load (splitJsonString[6]);
        totalCash = Wrapper<float>.CreateFromJsonString (splitJsonString[7]);
        perStockTypePerPowerLevelRange = PerStockTypePerPowerLevelRange.Load(splitJsonString[8]);

        specificArmourCollection = SpecificArmourCollection.CreateFromJsonString(splitJsonString[9]);
        specificPotionCollection = SpecificPotionCollection.CreateFromJsonString(splitJsonString[10]);
        specificRingCollection = SpecificRingCollection.CreateFromJsonString (splitJsonString[11]);
        specificRodCollection = SpecificRodCollection.CreateFromJsonString (splitJsonString[12]);
        specificScrollCollection = SpecificScrollCollection.CreateFromJsonString (splitJsonString[13]);
        specificStaffCollection = SpecificStaffCollection.CreateFromJsonString (splitJsonString[14]);
        specificWandCollection = SpecificWandCollection.CreateFromJsonString (splitJsonString[15]);
        specificWeaponCollection = SpecificWeaponCollection.CreateFromJsonString(splitJsonString[16]);
        specificWondrousCollection = SpecificWondrousCollection.CreateFromJsonString (splitJsonString[17]);
    }
}
