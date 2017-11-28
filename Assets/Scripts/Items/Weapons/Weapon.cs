using UnityEngine;
using System;

public class Weapon : Item<Weapon>
{
    public static Weapon Create (string name, int cost, Item.Rarity rarity, EnumSettingIndex book,
        int page)
    {
        Weapon newWeapon = CreateInstance<Weapon> ();
        newWeapon.name = name;
        newWeapon.cost = cost;
        newWeapon.rarity = rarity;
        newWeapon.book = book;
        newWeapon.page = page;
        return newWeapon;
    }

    public static Weapon CreateBlank (EnumSetting books)
    {
        return Create ("NAME", 0, Item.Rarity.Mundane, new EnumSettingIndex(books, 0), 0);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)rarity) + jsonSplitter[0];
        jsonString += EnumSettingIndex.GetJsonString(book) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];
        
        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[1]);
        rarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        book = EnumSettingIndex.CreateFromJsonString(splitJsonString[3]);
        page = Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
    }
}
