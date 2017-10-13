using UnityEngine;
using System;

public class Settlement : Jsonable<Settlement>
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

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += notes + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)size) + jsonSplitter[0];
        jsonString += perSizeRestockSettings.name + jsonSplitter[0];

        for (int i = 0; i < shops.Length; i++)
        {
            jsonString += Shop.GetJsonString(shops[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        notes = splitJsonString[1];
        size = (Size)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);
        perSizeRestockSettings = PerSizeRestockSettings.Load (splitJsonString[3]);

        shops = new Shop[splitJsonString.Length - 4];
        for (int i = 0; i < shops.Length; i++)
        {
            shops[i] = Shop.CreateFromJsonString (splitJsonString[i + 4]);
        }
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

    public void PassTime (int daysPassed)
    {
        for (int i = 0; i < shops.Length; i++)
        {
            shops[i].PassTime (daysPassed);
        }
    }
}

