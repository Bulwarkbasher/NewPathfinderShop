using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Item<Staff>
{
    public static Staff Create(string name, int cost, Item.Rarity rarity, SelectedEnumSetting book,
        int page)
    {
        Staff newStaff = CreateInstance<Staff>();
        newStaff.name = name;
        newStaff.cost = cost;
        newStaff.rarity = rarity;
        newStaff.book = book;
        newStaff.page = page;
        return newStaff;
    }

    public static Staff CreateBlank(EnumSetting books)
    {
        return Create("NAME", 0, Item.Rarity.Mundane, SelectedEnumSetting.CreateBlank(books), 0);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)rarity) + jsonSplitter[0];
        jsonString += SelectedEnumSetting.GetJsonString(book) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[1]);
        rarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        book = SelectedEnumSetting.CreateFromJsonString(splitJsonString[3]);
        page = Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
    }
}
