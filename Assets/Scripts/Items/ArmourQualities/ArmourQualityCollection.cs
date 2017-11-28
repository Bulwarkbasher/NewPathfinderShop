using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArmourQualityCollection : ItemCollection<ArmourQualityCollection, ArmourQuality>
{
    public static ArmourQualityCollection Create(string name, EnumSetting books)
    {
        ArmourQualityCollection armourQualityCollection = CreateInstance<ArmourQualityCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Armour Quality Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Armour Quality Collection name invalid, name cannot start with Default");

        armourQualityCollection.name = name;
        armourQualityCollection.items = new ArmourQuality[0];
        armourQualityCollection.books = books;

        SaveableHolder.AddSaveable(armourQualityCollection);

        return armourQualityCollection;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += ArmourQuality.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        books = EnumSetting.Load(splitJsonString[1]);

        items = new ArmourQuality[splitJsonString.Length - 2];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = ArmourQuality.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}