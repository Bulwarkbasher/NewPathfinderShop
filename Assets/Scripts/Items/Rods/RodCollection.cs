using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RodCollection : ItemCollection<RodCollection, Rod>
{
    public static RodCollection Create(string name, EnumSetting books)
    {
        RodCollection newRodCollection = CreateInstance<RodCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Rod Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Rod Collection name invalid, name cannot start with Default");

        newRodCollection.name = name;
        newRodCollection.items = new Rod[0];
        newRodCollection.books = books;

        SaveableHolder.AddSaveable(newRodCollection);

        return newRodCollection;
    }

    public void AddRod()
    {
        Rod newRod = Rod.CreateBlank(books);
        AddItem(newRod);
    }

    public Rod PickRod(FloatRange budgetRange)
    {
        if (items.Length == 0)
            return null;

        List<Rod> affordableRodList = new List<Rod>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost <= budgetRange.max && items[i].cost >= budgetRange.min)
                affordableRodList.Add(items[i]);
        }

        Rod[] affordableRods = affordableRodList.ToArray();

        Rod chosenRod = Rod.PickItem(affordableRods);

        return chosenRod;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += Rod.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        books = EnumSetting.Load(splitJsonString[1]);

        items = new Rod[splitJsonString.Length - 2];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Rod.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}