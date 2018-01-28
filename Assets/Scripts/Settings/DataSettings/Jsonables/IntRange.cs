using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntRange : DataPerEnumSetting<IntRange>
{
    public int min;
    public int max;

    public int Mean { get { return Mathf.FloorToInt((min + max) * 0.5f); } }

    public int Random()
    {
        return UnityEngine.Random.Range(min, max + 1);
    }

    public void SetRange (int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    protected override void SetDefaults()
    {
        min = 0;
        max = 0;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += Wrapper<int>.GetJsonString(min) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(max) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        min = Wrapper<int>.CreateFromJsonString(splitJsonString[0]);
        max = Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
    }
}