using UnityEngine;
using System;

public class Settlement : ScriptableObject
{
    public enum Size
    {
        Thorpe,
        Hamlet,
        Village,
        SmallTown,
        LargeTown,
        SmallCity,
        LargeCity,
        Metropolis,
    }

    private static readonly string[] k_JsonSplitter =
    {
        "###SettlementSplitter###",
    };

    public string notes;
    public Size size;
    public PerSizeRestockSettings perSizeRestockSettings;
    public Shop[] shops = new Shop[0];

    public RestockSettings RestockSettings
    {
        get { return perSizeRestockSettings[size]; }
    }
    
    public static Settlement Create (string name, string notes, Size size, PerSizeRestockSettings perSizeRestockSettings)
    {
        Settlement newSettlement = CreateInstance<Settlement>();
        newSettlement.name = name;
        newSettlement.notes = notes;
        newSettlement.size = size;
        newSettlement.perSizeRestockSettings = perSizeRestockSettings;
        return newSettlement;
    }
    
    public static Settlement Create (string name, string notes, Size size)
    {
        return Create(name, notes, size, DefaultResourceHolder.DefaultPerSizeRestockSettings);
    }

    public static string GetJsonString (Settlement settlement)
    {
        string jsonString = "";

        jsonString += settlement.name + k_JsonSplitter[0];
        jsonString += settlement.notes + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)settlement.size) + k_JsonSplitter[0];
        jsonString += settlement.perSizeRestockSettings.name + k_JsonSplitter[0];
        settlement.perSizeRestockSettings.Save ();

        for (int i = 0; i < settlement.shops.Length; i++)
        {
            jsonString += Shop.GetJsonString (settlement.shops[i]) + k_JsonSplitter[0];
        }

        return jsonString;
    }

    public static Settlement CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split (k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);

        Settlement settlement = CreateInstance<Settlement> ();

        settlement.name = splitJsonString[0];
        settlement.notes = splitJsonString[1];
        settlement.size = (Size)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);
        settlement.perSizeRestockSettings = PerSizeRestockSettings.Load (splitJsonString[3]);

        settlement.shops = new Shop[splitJsonString.Length - 4];
        for (int i = 0; i < settlement.shops.Length; i++)
        {
            settlement.shops[i] = Shop.CreateFromJsonString (splitJsonString[i + 4]);
        }

        return settlement;
    }

    public void AddShop (string shopName, string shopNotes, Shop.Size shopSize)
    {
        Shop[] newShops = new Shop[shops.Length + 1];

        for (int i = 0; i < shops.Length; i++)
        {
            newShops[i] = shops[i];
        }

        newShops[shops.Length] = Shop.Create (shopName, shopNotes, shopSize);
    }

    public void Restock (int daysSinceLastVisit)
    {
        for (int i = 0; i < shops.Length; i++)
        {
            shops[i].Restock (daysSinceLastVisit);
        }
    }
}

