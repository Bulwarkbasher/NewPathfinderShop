using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StaffCollection : ItemCollection<StaffCollection, Staff>
{
    public static StaffCollection Create(string name, EnumSetting books)
    {
        StaffCollection newStaffCollection = CreateInstance<StaffCollection>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Staff Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Staff Collection name invalid, name cannot start with Default");

        newStaffCollection.name = name;
        newStaffCollection.items = new Staff[0];
        newStaffCollection.books = books;

        SaveableHolder.AddSaveable(newStaffCollection);

        return newStaffCollection;
    }

    public void AddStaff()
    {
        Staff newStaff = Staff.CreateBlank(books);
        AddItem(newStaff);
    }

    public Staff PickStaff(FloatRange budgetRange)
    {
        if (items.Length == 0)
            return null;

        List<Staff> affordableStaffList = new List<Staff>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost <= budgetRange.max && items[i].cost >= budgetRange.min)
                affordableStaffList.Add(items[i]);
        }

        Staff[] affordableStaffs = affordableStaffList.ToArray();

        Staff chosenStaff = Staff.PickItem(affordableStaffs);

        return chosenStaff;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];

        for (int i = 0; i < items.Length; i++)
        {
            jsonString += Staff.GetJsonString(items[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        books = EnumSetting.Load(splitJsonString[1]);

        items = new Staff[splitJsonString.Length - 2];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Staff.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}