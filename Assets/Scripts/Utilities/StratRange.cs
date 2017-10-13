using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StratRanges : Jsonable<StratRanges>
{
    public Range minor;
    public Range medium;
    public Range major;

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += Range.GetJsonString(minor) + jsonSplitter[0];
        jsonString += Range.GetJsonString(medium) + jsonSplitter[0];
        jsonString += Range.GetJsonString(major) + jsonSplitter[0];

        return jsonString;
    }


    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        minor = Range.CreateFromJsonString(splitJsonString[0]);
        medium = Range.CreateFromJsonString(splitJsonString[1]);
        major = Range.CreateFromJsonString(splitJsonString[2]);
    }
}