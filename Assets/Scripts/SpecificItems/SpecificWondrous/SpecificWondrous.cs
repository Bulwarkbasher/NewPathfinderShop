using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWondrous : SpecificItem<SpecificWondrous>
{
    public Wondrous wondrous;

    public static SpecificWondrous CreateRandom(JsonableSelectedEnumSetting powerLevel, FloatRange budgetRange, WondrousCollection wondrousCollection)
    {
        SpecificWondrous newSpecificWondrous = CreateInstance<SpecificWondrous>();
        newSpecificWondrous.powerLevel = powerLevel;
        newSpecificWondrous.wondrous = wondrousCollection.PickWondrous(budgetRange);
        newSpecificWondrous.cost = newSpecificWondrous.wondrous.cost;
        newSpecificWondrous.name = newSpecificWondrous.wondrous.name;
        return newSpecificWondrous;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += JsonableSelectedEnumSetting.GetJsonString(powerLevel) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += Wondrous.GetJsonString(wondrous) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        powerLevel = JsonableSelectedEnumSetting.CreateFromJsonString(splitJsonString[1]);
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        notes = CreateStringFromSafeJson(splitJsonString[3]);
        wondrous = Wondrous.CreateFromJsonString(splitJsonString[4]);
    }
}
