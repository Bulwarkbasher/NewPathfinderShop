using System;
using System.Reflection;
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

    public TChild Duplicate ()
    {
        TChild duplicate = CreateInstance<TChild>();

        Type jsonableType = typeof(Jsonable<>);
        MethodInfo jsonableDuplicateMethod = jsonableType.GetMethod("Duplicate", BindingFlags.Public | BindingFlags.Instance);

        FieldInfo[] allFieldInfos = typeof(TChild).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        for (int i = 0; i < allFieldInfos.Length; i++)
        {
            FieldInfo field = allFieldInfos[i];
            Type fieldType = field.FieldType;

            if (fieldType.IsArray && fieldType.GetElementType().IsSubclassOf(jsonableType))
            {
                object[] array = (object[])field.GetValue(this);
                object[] duplicateArray = new object[array.Length];
                for (int j = 0; j < array.Length; j++)
                {
                    duplicateArray[j] = jsonableDuplicateMethod.Invoke(array[j], null);
                }
                field.SetValue(duplicate, duplicateArray);
            }
            else if (fieldType.IsSubclassOf(jsonableType))
            {
                object jsonableObject = field.GetValue(this);
                object jsonableDuplicate = jsonableDuplicateMethod.Invoke(jsonableObject, null);
                field.SetValue(duplicate, jsonableDuplicate);
            }
            else
            {
                object originalFieldObject = field.GetValue(this);
                field.SetValue(duplicate, originalFieldObject);
            }
        }

        return duplicate;
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
