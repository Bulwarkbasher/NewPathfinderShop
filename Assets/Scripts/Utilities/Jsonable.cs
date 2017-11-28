using System;
using UnityEngine;

public abstract class Jsonable<TChild> : ScriptableObject
    where TChild : Jsonable<TChild>
{
    const string k_EmptyString = "EMPTYSTRING";

    public static string GetSafeJsonFromString(string notes)
    {
        if (string.IsNullOrEmpty(notes))
            return k_EmptyString;

        return notes;
    }

    public static string CreateStringFromSafeJson(string json)
    {
        if (json == k_EmptyString)
            return "";

        return json;
    }

    protected static string[] GetJsonSplitter()
    {
        string[] jsonSplitter = { "###" + typeof(TChild) + "Splitter###" };
        return jsonSplitter;
    }

    public static TChild CreateFromJsonString (string jsonString)
    {
        TChild childJsonable = CreateInstance<TChild>();

        string[] splitJsonString = jsonString.Split(GetJsonSplitter(), StringSplitOptions.RemoveEmptyEntries);

        childJsonable.SetupFromSplitJsonString(splitJsonString);

        return childJsonable;
    }

    public static string GetJsonString(TChild childJsonable)
    {
        string[] jsonSplitter = GetJsonSplitter ();
        return childJsonable.ConvertToJsonString(jsonSplitter);
    }

    protected abstract string ConvertToJsonString(string[] jsonSplitter);

    protected abstract void SetupFromSplitJsonString(string[] splitJsonString);
}
