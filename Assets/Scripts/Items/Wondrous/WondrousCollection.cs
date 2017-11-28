using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WondrousCollection : ItemCollection<WondrousCollection, Wondrous>
{
    public static WondrousCollection Create(string name, EnumSetting books)
    {
        WondrousCollection newWondrousCollection = CreateInstance<WondrousCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Wondrous Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Wondrous Collection name invalid, name cannot start with Default");

        newWondrousCollection.name = name;
        newWondrousCollection.items = new Wondrous[0];
        newWondrousCollection.books = books;

        SaveableHolder.AddSaveable(newWondrousCollection);

        return newWondrousCollection;
    }

    public void AddWondrous()
    {
        Wondrous newWondrous = Wondrous.CreateBlank(books);
        AddItem(newWondrous);
    }

    public Wondrous PickWondrous(FloatRange budgetRange)
    {
        if (items.Length == 0)
            return null;

        List<Wondrous> affordableWondrousList = new List<Wondrous>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost <= budgetRange.max && items[i].cost >= budgetRange.min)
                affordableWondrousList.Add(items[i]);
        }

        Wondrous[] affordableWondrouss = affordableWondrousList.ToArray();

        Wondrous chosenWondrous = Wondrous.PickItem(affordableWondrouss);

        return chosenWondrous;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += Wondrous.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        books = EnumSetting.Load(splitJsonString[1]);

        items = new Wondrous[splitJsonString.Length - 2];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Wondrous.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}
