using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntStratRanges : Jsonable<IntStratRanges>
{
    public IntRange minor;
    public IntRange medium;
    public IntRange major;

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += IntRange.GetJsonString(minor) + jsonSplitter[0];
        jsonString += IntRange.GetJsonString(medium) + jsonSplitter[0];
        jsonString += IntRange.GetJsonString(major) + jsonSplitter[0];

        return jsonString;
    }


    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        minor = IntRange.CreateFromJsonString(splitJsonString[0]);
        medium = IntRange.CreateFromJsonString(splitJsonString[1]);
        major = IntRange.CreateFromJsonString(splitJsonString[2]);
    }
}