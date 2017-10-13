using UnityEngine;
using System;

public class RestockSettings : Jsonable<RestockSettings>
{
    public Range days;
    public Range percent;

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += Range.GetJsonString(days) + jsonSplitter[0];
        jsonString += Range.GetJsonString(percent) + jsonSplitter[0];

        return jsonString;
    }
    
    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        days = Range.CreateFromJsonString (splitJsonString[0]);
        percent = Range.CreateFromJsonString (splitJsonString[1]);
    }
}