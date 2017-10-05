using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO: restock function that calls all shop restocks
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
    public RestockSettings restockSettings;
    public Shop[] shops = new Shop[0];
    
    public static Settlement Create (string name, string notes, Size size, RestockSettings restockSettings)
    {
        Settlement newSettlement = CreateInstance<Settlement>();
        newSettlement.name = name;
        newSettlement.notes = notes;
        newSettlement.size = size;
        newSettlement.restockSettings = restockSettings;
        return newSettlement;
    }
    
    public static Settlement Create (string name, string notes, Size size)
    {
        return Create(name, notes, size, DefaultResourceHolder.DefaultPerSizeRestockSettings[size]);
    }

    public static string GetJsonString (Settlement settlement)
    {
        string jsonString = "";

        jsonString += settlement.name + k_JsonSplitter[0];
        jsonString += settlement.notes + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)settlement.size) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString (settlement.restockSettings) + k_JsonSplitter[0];

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
        settlement.restockSettings = RestockSettings.CreateFromJsonString (splitJsonString[3]);

        settlement.shops = new Shop[splitJsonString.Length - 4];
        for (int i = 0; i < settlement.shops.Length; i++)
        {
            settlement.shops[i] = Shop.CreateFromJsonString (splitJsonString[i + 4], settlement);
        }

        return settlement;
    }
}

