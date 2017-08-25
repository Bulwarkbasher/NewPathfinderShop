using System;
using UnityEngine;

public class Wrapper<T>
    where T : struct, IComparable, IConvertible, IComparable<T>, IEquatable<T>
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