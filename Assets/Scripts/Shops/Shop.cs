using UnityEngine;
using System;

// TODO: have a running gold total for shops based on what they're thoeretical stock total should be
// TODO: have an additional setting for the amount of additional money shops have based on size?
public class Shop : ScriptableObject
{
    public enum Size
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

    // TODO: some of the for Default Availabilities values cannot be set by ranges from the book.
    // TODO: ranges aren't specified for things like weapons where it can potentially go up to +10 and lead to very bad ranges
    

    private static readonly string[] k_JsonSplitter =
    {
        "###ShopSplitter###",
    };

    public string notes;
    public Size size;
    public StockType stockTypes;
    public float restockFrequencyModifier;
    public int daysSinceLastRestock;

    public SpecificArmourCollection specificArmourCollection;
    public SpecificPotionCollection specificPotionCollection;
    public SpecificRingCollection specificRingCollection;
    public SpecificRodCollection specificRodCollection;
    public SpecificScrollCollection specificScrollCollection;
    public SpecificStaffCollection specificStaffCollection;
    public SpecificWandCollection specificWandCollection;
    public SpecificWeaponCollection specificWeaponCollection;
    public SpecificWondrousCollection specificWondrousCollection;

    public static Shop Create (string name, string notes, Size shopSize, float restockFrequencyModifier)
    {
        Shop newShop = CreateInstance<Shop>();
        newShop.name = name;
        newShop.notes = notes;
        newShop.size = shopSize;
        newShop.restockFrequencyModifier = restockFrequencyModifier;
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
        float restockFrequencyModifier = DefaultResourceHolder.DefaultPerSizeRestockFrequencyModifiers[shopSize];
        return Create (name, notes, shopSize, restockFrequencyModifier);
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

    public void PassTime (int daysPassed)
    {
        int totalDaysSinceLastRestock = daysPassed + daysSinceLastRestock;
        RestockSettings restockSettings = GetLocation ().RestockSettings;

        int daysUntilRestock = Mathf.FloorToInt(restockSettings.days.Random() * restockFrequencyModifier);
        while (totalDaysSinceLastRestock - daysUntilRestock > 0)
        {
            Restock (restockSettings);
            totalDaysSinceLastRestock -= daysUntilRestock;
            daysUntilRestock = Mathf.FloorToInt(restockSettings.days.Random() * restockFrequencyModifier);
        }

        daysSinceLastRestock = totalDaysSinceLastRestock;
        // TODO: calculate ready money here based on total value of stock compared to the total value of allowed stock
    }

    protected void Restock(RestockSettings restockSettings)
    {
        if ((stockTypes | StockType.Armour) == stockTypes)
            specificArmourCollection.Restock(restockSettings);
        if ((stockTypes | StockType.Potion) == stockTypes)
            specificPotionCollection.Restock(restockSettings);
        if ((stockTypes | StockType.Ring) == stockTypes)
            specificRingCollection.Restock(restockSettings);
        if ((stockTypes | StockType.Rod) == stockTypes)
            specificRodCollection.Restock(restockSettings);
        if ((stockTypes | StockType.Scroll) == stockTypes)
            specificScrollCollection.Restock(restockSettings);
        if ((stockTypes | StockType.Staff) == stockTypes)
            specificStaffCollection.Restock(restockSettings);
        if ((stockTypes | StockType.Wand) == stockTypes)
            specificWandCollection.Restock(restockSettings);
        if ((stockTypes | StockType.Weapon) == stockTypes)
            specificWeaponCollection.Restock(restockSettings);
        if ((stockTypes | StockType.Wondrous) == stockTypes)
            specificWondrousCollection.Restock(restockSettings);       
    }

    protected int GetTotalStockValue ()
    {
        int totalStockValue = 0;
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

    protected int GetMaxPossibleStockValue ()
    {
        int maxStockValue = 0;
        maxStockValue += specificArmourCollection.stockAvailability.MaxTotalBudget ();
        maxStockValue += specificPotionCollection.stockAvailability.MaxTotalBudget ();
        maxStockValue += specificRingCollection.stockAvailability.MaxTotalBudget ();
        maxStockValue += specificRodCollection.stockAvailability.MaxTotalBudget ();
        maxStockValue += specificScrollCollection.stockAvailability.MaxTotalBudget ();
        maxStockValue += specificStaffCollection.stockAvailability.MaxTotalBudget ();
        maxStockValue += specificWandCollection.stockAvailability.MaxTotalBudget ();
        maxStockValue += specificWeaponCollection.stockAvailability.MaxTotalBudget ();
        maxStockValue += specificWondrousCollection.stockAvailability.MaxTotalBudget();
        return maxStockValue;
    }

    public static string GetJsonString (Shop shop)
    {
        string jsonString = "";

        jsonString += shop.name + k_JsonSplitter[0];
        jsonString += shop.notes + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)shop.size) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)shop.stockTypes) + k_JsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString (shop.restockFrequencyModifier) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString (shop.daysSinceLastRestock) + k_JsonSplitter[0];

        jsonString += SpecificArmourCollection.GetJsonString(shop.specificArmourCollection) + k_JsonSplitter[0];
        jsonString += SpecificPotionCollection.GetJsonString(shop.specificPotionCollection) + k_JsonSplitter[0];
        jsonString += SpecificRingCollection.GetJsonString (shop.specificRingCollection) + k_JsonSplitter[0];
        jsonString += SpecificRodCollection.GetJsonString(shop.specificRodCollection) + k_JsonSplitter[0];
        jsonString += SpecificScrollCollection.GetJsonString(shop.specificScrollCollection) + k_JsonSplitter[0];
        jsonString += SpecificStaffCollection.GetJsonString(shop.specificStaffCollection) + k_JsonSplitter[0];
        jsonString += SpecificWandCollection.GetJsonString(shop.specificWandCollection) + k_JsonSplitter[0];
        jsonString += SpecificWeaponCollection.GetJsonString(shop.specificWeaponCollection) + k_JsonSplitter[0];
        jsonString += SpecificWondrousCollection.GetJsonString(shop.specificWondrousCollection) + k_JsonSplitter[0];
        
        return jsonString;
    }

    public static Shop CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split (k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);

        Shop shop = CreateInstance<Shop> ();

        shop.name = splitJsonString[0];
        shop.notes = splitJsonString[1];
        shop.size = (Size)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);
        shop.stockTypes = (StockType)Wrapper<int>.CreateFromJsonString (splitJsonString[3]);
        shop.restockFrequencyModifier = Wrapper<float>.CreateFromJsonString (splitJsonString[4]);
        shop.daysSinceLastRestock = Wrapper<int>.CreateFromJsonString (splitJsonString[5]);

        shop.specificArmourCollection = SpecificArmourCollection.CreateFromJsonString(splitJsonString[6]);
        shop.specificPotionCollection = SpecificPotionCollection.CreateFromJsonString(splitJsonString[7]);
        shop.specificRingCollection = SpecificRingCollection.CreateFromJsonString (splitJsonString[8]);
        shop.specificRodCollection = SpecificRodCollection.CreateFromJsonString (splitJsonString[9]);
        shop.specificScrollCollection = SpecificScrollCollection.CreateFromJsonString (splitJsonString[10]);
        shop.specificStaffCollection = SpecificStaffCollection.CreateFromJsonString (splitJsonString[11]);
        shop.specificWandCollection = SpecificWandCollection.CreateFromJsonString (splitJsonString[12]);
        shop.specificWeaponCollection = SpecificWeaponCollection.CreateFromJsonString(splitJsonString[13]);
        shop.specificWondrousCollection = SpecificWondrousCollection.CreateFromJsonString (splitJsonString[14]);

        return shop;
    }
}
