using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RingCollection : ItemCollection<RingCollection, Ring>
{
    public static RingCollection Create(string name, EnumSetting books)
    {
        RingCollection newRingCollection = CreateInstance<RingCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Ring Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Ring Collection name invalid, name cannot start with Default");

        newRingCollection.name = name;
        newRingCollection.items = new Ring[0];
        newRingCollection.books = books;

        SaveableHolder.AddSaveable(newRingCollection);

        return newRingCollection;
    }

    public void AddRing()
    {
        Ring newRing = Ring.CreateBlank(books);
        AddItem(newRing);
    }

    public Ring PickRing(FloatRange budgetRange)
    {
        if (items.Length == 0)
            return null;

        List<Ring> affordableRingList = new List<Ring>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost <= budgetRange.max && items[i].cost >= budgetRange.min)
                affordableRingList.Add(items[i]);
        }

        Ring[] affordableRings = affordableRingList.ToArray();

        Ring chosenRing = Ring.PickItem(affordableRings);

        return chosenRing;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += Ring.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        books = EnumSetting.Load(splitJsonString[1]);

        items = new Ring[splitJsonString.Length - 2];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Ring.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}
