using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArmourCollection : ItemCollection<ArmourCollection, Armour>
{
    public static ArmourCollection Create(string name, EnumSetting books)
    {
        ArmourCollection newArmourCollection = CreateInstance<ArmourCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Armour Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Armour Collection name invalid, name cannot start with Default");

        newArmourCollection.name = name;
        newArmourCollection.items = new Armour[0];
        newArmourCollection.books = books;

        SaveableHolder.AddSaveable(newArmourCollection);

        return newArmourCollection;
    }

    public void AddArmour()
    {
        Armour newArmour = Armour.CreateBlank(books);
        AddItem(newArmour);
    }

    public Armour PickArmour(ref float budget)
    {
        if (items.Length == 0)
            return null;

        List<Armour> affordableArmourList = new List<Armour>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost < budget)
                affordableArmourList.Add(items[i]);
        }

        Armour[] affordableArmours = affordableArmourList.ToArray();

        Armour chosenArmour = Armour.PickItem(affordableArmours);
        budget -= chosenArmour.cost;

        return chosenArmour;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += Armour.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        books = EnumSetting.Load(splitJsonString[1]);

        items = new Armour[splitJsonString.Length - 2];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Armour.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}