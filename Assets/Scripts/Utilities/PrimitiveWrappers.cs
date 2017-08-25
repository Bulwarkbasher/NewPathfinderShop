using System;
using UnityEngine;

[Serializable]
public class FloatWrapper
{
    public float value;

    public FloatWrapper (float value)
    {
        this.value = value;
    }


    public static string GetJsonString (float value)
    {
        return JsonUtility.ToJson(new FloatWrapper(value));
    }


    public static float CreateFromJsonString (string jsonString)
    {
        return JsonUtility.FromJson<FloatWrapper>(jsonString).value;
    }
}


public class Wrapper<T>
    where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
{
    public T value;


    public Wrapper (T value)
    {
        this.value = value;
    }


    public static string GetJsonString (T value)
    {
        return JsonUtility.ToJson(new Wrapper<T>(value));
    }


    public static T CreateFromJsonString (string jsonString)
    {
        return JsonUtility.FromJson<Wrapper<T>>(jsonString).value;
    }
}


[Serializable]
public class IntWrapper
{
    public float value;

    public IntWrapper(float value)
    {
        this.value = value;
    }
}


[Serializable]
public class BoolWrapper
{
    public float value;

    public BoolWrapper(float value)
    {
        this.value = value;
    }
}