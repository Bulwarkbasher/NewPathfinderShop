using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatRange : Jsonable<FloatRange>
{
    public float min;
    public float max;

    public float Mean { get { return (min + max) * 0.5f; } }

    public float Random()
    {
        return UnityEngine.Random.Range(min, max + 1);
    }

    public static FloatRange Create(float min, float max)
    {
        FloatRange newFloatRange = CreateInstance<FloatRange>();
        newFloatRange.min = min;
        newFloatRange.max = max;
        return newFloatRange;
    }

    public static FloatRange CreateBlank ()
    {
        return Create(0f, 0f);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += Wrapper<float>.GetJsonString(min) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(max) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        min = Wrapper<float>.CreateFromJsonString(splitJsonString[0]);
        max = Wrapper<float>.CreateFromJsonString(splitJsonString[1]);
    }
}