using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRing : SpecificItem<SpecificRing>
{
    public Ring ring;

    public static SpecificRing CreateRandom(JsonableSelectedEnumSetting powerLevel, FloatRange budgetRange, RingCollection ringCollection)
    {
        SpecificRing newSpecificRing = CreateInstance<SpecificRing>();
        newSpecificRing.powerLevel = powerLevel;
        newSpecificRing.ring = ringCollection.PickRing(budgetRange);
        newSpecificRing.cost = newSpecificRing.ring.cost;
        newSpecificRing.name = newSpecificRing.ring.name;
        return newSpecificRing;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += JsonableSelectedEnumSetting.GetJsonString(powerLevel) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += Ring.GetJsonString(ring) + jsonSplitter[0];
        
        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        powerLevel = JsonableSelectedEnumSetting.CreateFromJsonString(splitJsonString[1]);
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        notes = CreateStringFromSafeJson(splitJsonString[3]);
        ring = Ring.CreateFromJsonString(splitJsonString[4]);
    }
}
