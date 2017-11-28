using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu]
public class WeaponQualityCollection : ItemCollection<WeaponQualityCollection, WeaponQuality>
{
    public static WeaponQualityCollection Create(string name, EnumSetting books)
    {
        WeaponQualityCollection weaponQualityCollection = CreateInstance<WeaponQualityCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Weapon Quality Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Weapon Quality Collection name invalid, name cannot start with Default");

        weaponQualityCollection.name = name;
        weaponQualityCollection.items = new WeaponQuality[0];
        weaponQualityCollection.books = books;

        SaveableHolder.AddSaveable(weaponQualityCollection);

        return weaponQualityCollection;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += WeaponQuality.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        books = EnumSetting.Load(splitJsonString[1]);

        items = new WeaponQuality[splitJsonString.Length - 2];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = WeaponQuality.CreateFromJsonString (splitJsonString[i + 2]);
        }
    }
}
