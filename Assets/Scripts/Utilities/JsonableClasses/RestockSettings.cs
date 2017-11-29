using UnityEngine;
using System;

public class RestockSettings : Jsonable<RestockSettings>
{
    public IntRange days;
    public IntRange percent;

    public static RestockSettings Create (string name, IntRange days, IntRange percent)
    {
        RestockSettings newRestockSettings = CreateInstance<RestockSettings>();
        newRestockSettings.name = name;
        newRestockSettings.days = days;
        newRestockSettings.percent = percent;
        return newRestockSettings;
    }

    public static RestockSettings CreateBlank ()
    {
        return Create("NAME", new IntRange(), new IntRange());
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