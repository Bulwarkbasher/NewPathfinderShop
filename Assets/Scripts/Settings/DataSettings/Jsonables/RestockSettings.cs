using UnityEngine;
using System;

public class RestockSettings : DataPerEnumSetting<RestockSettings>
{
    public IntRange days;
    public IntRange percent;

    protected override void SetDefaults()
    {
        days = IntRange.Create();
        percent = IntRange.Create();
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += IntRange.GetJsonString(days) + jsonSplitter[0];
        jsonString += IntRange.GetJsonString(percent) + jsonSplitter[0];

        return jsonString;
    }
    
    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        days = IntRange.CreateFromJsonString (splitJsonString[0]);
        percent = IntRange.CreateFromJsonString (splitJsonString[1]);
    }
}