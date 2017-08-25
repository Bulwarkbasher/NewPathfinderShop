using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Range
{
    public int min;
    public int max;


    public int Random()
    {
        return UnityEngine.Random.Range(min, max + 1);
    }


    public static string GetJsonString (Range range)
    {
        return JsonUtility.ToJson(range);
    }


    public static Range CreateFromJsonString (string jsonString)
    {
        return JsonUtility.FromJson<Range>(jsonString);
    }
}