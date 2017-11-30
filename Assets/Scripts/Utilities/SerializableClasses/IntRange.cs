using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntRange : Jsonable<IntRange>
{
    public int min;
    public int max;

    public int Mean { get { return Mathf.FloorToInt((min + max) * 0.5f); } }

    public int Random()
    {
        return UnityEngine.Random.Range(min, max + 1);
    }

    public static IntRange Create (int min, int max)
    {
        IntRange newIntRange = CreateInstance<IntRange>();
        newIntRange.min = min;
        newIntRange.max = max;
        return newIntRange;
    }

    public static IntRange CreateBlank ()
    {
        return Create(0, 0);
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