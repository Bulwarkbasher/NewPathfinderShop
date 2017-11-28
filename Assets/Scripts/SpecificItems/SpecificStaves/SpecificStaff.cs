using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaff : SpecificItem<SpecificStaff>
{
    public Staff staff;

    public static SpecificStaff CreateRandom(SpecificItem.PowerLevel powerLevel, FloatRange budgetRange, StaffCollection staffCollection)
    {
        SpecificStaff newSpecificStaff = CreateInstance<SpecificStaff>();
        newSpecificStaff.powerLevel = powerLevel;
        newSpecificStaff.staff = staffCollection.PickStaff(budgetRange);
        newSpecificStaff.cost = newSpecificStaff.staff.cost;
        newSpecificStaff.name = newSpecificStaff.staff.name;
        return newSpecificStaff;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)powerLevel) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += Staff.GetJsonString(staff) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        powerLevel = (SpecificItem.PowerLevel)Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        notes = CreateStringFromSafeJson(splitJsonString[3]);
        staff = Staff.CreateFromJsonString(splitJsonString[4]);
    }
}