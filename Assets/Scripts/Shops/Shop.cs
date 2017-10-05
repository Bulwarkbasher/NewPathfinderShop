using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public static void AddSpecificArmourCollectionToShop(Shop shop, Availability stockAvailability, ArmourCollection availableArmours, ArmourQualityCollection availableArmourQualities)
    {
        shop.stockTypes |= StockType.Armour;
        shop.specificArmourCollection = SpecificArmourCollection.Create(stockAvailability, availableArmours, availableArmourQualities);
    }

    public static void AddSpecificArmourCollectionToShop(Shop shop, ArmourCollection availableArmours, ArmourQualityCollection availableArmourQualities)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Armour][shop.size];
        AddSpecificArmourCollectionToShop(shop, stockAvailability, availableArmours, availableArmourQualities);
    }

    public static void AddSpecificPotionCollectionToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= StockType.Potion;
        shop.specificPotionCollection = SpecificPotionCollection.Create(stockAvailability);
    }

    public static void AddSpecificPotionCollectionToShop(Shop shop, SpellCollection availableSpells)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Potion][shop.size];
        AddSpecificPotionCollectionToShop(shop, stockAvailability, availableSpells);
    }

    public static void AddSpecificRingCollectionToShop(Shop shop, Availability stockAvailability)
    {
        shop.stockTypes |= StockType.Ring;
        shop.specificRingCollection = SpecificRingCollection.Create(stockAvailability);
    }

    public static void AddSpecificRingCollectionToShop(Shop shop)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Ring][shop.size];
        AddSpecificRingCollectionToShop(shop, stockAvailability);
    }

    public static void AddSpecificRodCollectionToShop(Shop shop, Availability stockAvailability)
    {
        shop.stockTypes |= StockType.Rod;
        shop.specificRodCollection = SpecificRodCollection.Create(stockAvailability);
    }

    public static void AddSpecificRodCollectionToShop(Shop shop)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Rod][shop.size];
        AddSpecificRodCollectionToShop(shop, stockAvailability);
    }

    public static void AddSpecificScrollCollectionToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= StockType.Scroll;
        shop.specificScrollCollection = SpecificScrollCollection.Create(stockAvailability);
    }

    public static void AddSpecificScrollCollectionToShop(Shop shop, SpellCollection availableSpells)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Scroll][shop.size];
        shop.specificScrollCollection = SpecificScrollCollection.Create(stockAvailability);
    }

    public static void AddSpecificStaffCollectionToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= StockType.Staff;
        shop.specificStaffCollection = SpecificStaffCollection.Create(stockAvailability);
    }

    public static void AddSpecificStaffCollectionToShop(Shop shop, SpellCollection availableSpells)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Staff][shop.size];
        AddSpecificStaffCollectionToShop(shop, stockAvailability, availableSpells);
    }

    public static void AddSpecificWandCollectionToShop(Shop shop, Availability stockAvailability, SpellCollection availableSpells)
    {
        shop.stockTypes |= StockType.Wand;
        shop.specificWandCollection = SpecificWandCollection.Create(stockAvailability);
    }

    public static void AddSpecificWandCollectionToShop(Shop shop, SpellCollection availableSpells)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Wand][shop.size];
        AddSpecificWandCollectionToShop(shop, stockAvailability, availableSpells);
    }

    public static void AddSpecificWeaponCollectionToShop (Shop shop, Availability stockAvailability, WeaponCollection availableWeapons, WeaponQualityCollection availableWeaponQualities)
    {
        shop.stockTypes |= StockType.Weapon;
        shop.specificWeaponCollection = SpecificWeaponCollection.Create(stockAvailability, availableWeapons, availableWeaponQualities);
    }

    public static void AddSpecificWeaponCollectionToShop(Shop shop, WeaponCollection availableWeapons, WeaponQualityCollection availableWeaponQualities)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Weapon][shop.size];
        AddSpecificWeaponCollectionToShop(shop, stockAvailability, availableWeapons, availableWeaponQualities);
    }

    public static void AddSpecificWondrousCollectionToShop(Shop shop, Availability stockAvailability)
    {
        shop.stockTypes |= StockType.Wondrous;
        shop.specificWondrousCollection = SpecificWondrousCollection.Create(stockAvailability);
    }

    public static void AddSpecificWondrousCollectionToShop(Shop shop)
    {
        Availability stockAvailability = DefaultResourceHolder.DefaultPerStockTypePerSizeAvailability[StockType.Wondrous][shop.size];
        AddSpecificWondrousCollectionToShop(shop, stockAvailability);
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

    public void Restock(int daysSinceLastVisit)
    {
        int restockCount = 0;
        int dayCounter = daysSinceLastVisit;

        RestockSettings restockSettings = GetLocation ().RestockSettings;

        dayCounter -= Mathf.FloorToInt(restockSettings.days.Random() * restockFrequencyModifier);

        while (dayCounter > 0)
        {
            restockCount++;
            dayCounter -= Mathf.FloorToInt(restockSettings.days.Random() * restockFrequencyModifier);
        }

        for (int i = 0; i < restockCount; i++)
        {
            if ((stockTypes | StockType.Armour) == stockTypes)
                specificArmourCollection.Restock(restockSettings);
            if ((stockTypes | StockType.Potion) == stockTypes)
                specificPotionCollection.Restock(restockSettings);
            if ((stockTypes | StockType.Ring) == stockTypes)
                specificRingCollection.Restock (restockSettings);
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
    }

    public static string GetJsonString (Shop shop)
    {
        string jsonString = "";

        jsonString += shop.name + k_JsonSplitter[0];
        jsonString += shop.notes + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)shop.size) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)shop.stockTypes) + k_JsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString (shop.restockFrequencyModifier) + k_JsonSplitter[0];

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

        shop.specificArmourCollection = SpecificArmourCollection.CreateFromJsonString(splitJsonString[5]);
        shop.specificPotionCollection = SpecificPotionCollection.CreateFromJsonString(splitJsonString[6]);
        shop.specificRingCollection = SpecificRingCollection.CreateFromJsonString (splitJsonString[7]);
        shop.specificRodCollection = SpecificRodCollection.CreateFromJsonString (splitJsonString[8]);
        shop.specificScrollCollection = SpecificScrollCollection.CreateFromJsonString (splitJsonString[9]);
        shop.specificStaffCollection = SpecificStaffCollection.CreateFromJsonString (splitJsonString[10]);
        shop.specificWandCollection = SpecificWandCollection.CreateFromJsonString (splitJsonString[11]);
        shop.specificWeaponCollection = SpecificWeaponCollection.CreateFromJsonString(splitJsonString[12]);
        shop.specificWondrousCollection = SpecificWondrousCollection.CreateFromJsonString (splitJsonString[13]);

        return shop;
    }
}
