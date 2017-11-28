using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class IntRange
{
    public int min;
    public int max;

    public int Mean { get { return Mathf.FloorToInt((min + max) * 0.5f); } }

    public int Random()
    {
        return UnityEngine.Random.Range(min, max + 1);
    }

    public static string GetJsonString (IntRange range)
    {
        return JsonUtility.ToJson(range);
    }
    
    public static IntRange CreateFromJsonString (string jsonString)
    {
        return JsonUtility.FromJson<IntRange>(jsonString);
    }
}


[Serializable]
public class FloatRange
{
    public float min;
    public float max;

    public float Mean { get { return (min + max) * 0.5f; } }

    public float Random()
    {
        return UnityEngine.Random.Range(min, max + 1);
    }

    public static string GetJsonString(FloatRange range)
    {
        return JsonUtility.ToJson(range);
    }

    public static FloatRange CreateFromJsonString(string jsonString)
    {
        return JsonUtility.FromJson<FloatRange>(jsonString);
    }
}