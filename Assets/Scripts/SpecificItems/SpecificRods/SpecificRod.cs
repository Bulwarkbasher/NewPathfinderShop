using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRod : SpecificItem<SpecificRod>
{
    public Rod rod;

    public static SpecificRod CreateRandom(JsonableSelectedEnumSetting powerLevel, FloatRange budgetRange, RodCollection rodCollection)
    {
        SpecificRod newSpecificRod = CreateInstance<SpecificRod>();
        newSpecificRod.powerLevel = powerLevel;
        newSpecificRod.rod = rodCollection.PickRod(budgetRange);
        newSpecificRod.cost = newSpecificRod.rod.cost;
        newSpecificRod.name = newSpecificRod.rod.name;
        return newSpecificRod;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += JsonableSelectedEnumSetting.GetJsonString(powerLevel) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += Rod.GetJsonString(rod) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        powerLevel = JsonableSelectedEnumSetting.CreateFromJsonString(splitJsonString[1]);
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        notes = CreateStringFromSafeJson(splitJsonString[3]);
        rod = Rod.CreateFromJsonString(splitJsonString[4]);
    }
}
