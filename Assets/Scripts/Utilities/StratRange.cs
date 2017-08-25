using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StratRanges
{
    public Range minor;
    public Range medium;
    public Range major;


    private static readonly string[] k_JsonSplitter =
    {
        "###StratRangesSplitter###",
    };


    public static string GetJsonString (StratRanges stratRanges)
    {
        string minorJson = Range.GetJsonString(stratRanges.minor);
        string mediumJson = Range.GetJsonString(stratRanges.medium);
        string majorJson = Range.GetJsonString(stratRanges.major);

        return minorJson + k_JsonSplitter[0] + mediumJson + k_JsonSplitter[0] + majorJson;
    }


    public static StratRanges CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, System.StringSplitOptions.RemoveEmptyEntries);

        string minorString = splitJsonString[0];
        string mediumString = splitJsonString[1];
        string majorString = splitJsonString[2];

        StratRanges stratRanges = new StratRanges();

        stratRanges.minor = Range.CreateFromJsonString(minorString);
        stratRanges.medium = Range.CreateFromJsonString(mediumString);
        stratRanges.major = Range.CreateFromJsonString(majorString);

        return stratRanges;
    }
}