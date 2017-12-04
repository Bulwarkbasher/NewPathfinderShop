using System;
using System.IO;
using System.Reflection;
using UnityEngine;

// NOTE: all front end saving should just call SaveableHolder.Save
public abstract class Saveable<TChild> : ScriptableObject
    where TChild : Saveable<TChild>
{
    public enum NameCheckResult
    {
        Good, Bad, IsDefault,
    }

    protected static NameCheckResult CheckName(string name)
    {
        if (name.StartsWith("Default"))
            return NameCheckResult.IsDefault;

        if (name.Contains("#"))
            return NameCheckResult.Bad;

        if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            return NameCheckResult.Bad;

        return NameCheckResult.Good;
    }
    // TODO: review all settingsd
    // Base class settings
    // params for create functions?
    // TODO: duplicate filtered
    // NOTE: created settings should be blank, duplicated settings should have original settings

    public TChild Duplicate()
    {
        TChild duplicate = CreateInstance<TChild>();
        duplicate.name = "New" + name;

        Type jsonableType = typeof(Jsonable<>);
        MethodInfo jsonableDuplicateMethod = jsonableType.GetMethod("Duplicate", BindingFlags.Public | BindingFlags.Instance);

        FieldInfo[] allFieldInfos = typeof(TChild).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        for(int i = 0; i < allFieldInfos.Length; i++)
        {
            FieldInfo field = allFieldInfos[i];
            Type fieldType = field.FieldType;
            
            if(fieldType.IsArray && fieldType.GetElementType().IsSubclassOf(jsonableType))
            {
                object[] array = (object[])field.GetValue(this);                
                object[] duplicateArray = new object[array.Length];
                for(int j = 0; j < array.Length; j++)
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

    public TChild DeepDuplicate ()
    {
        TChild duplicate = CreateInstance<TChild>();
        duplicate.name = "New" + name;

        Type jsonableType = typeof(Jsonable<>);
        MethodInfo jsonableDuplicateMethod = jsonableType.GetMethod("Duplicate", BindingFlags.Public | BindingFlags.Instance);

        Type saveableType = typeof(Saveable<>);
        MethodInfo saveableDeepDuplicateMethod = saveableType.GetMethod("DeepDuplicate", BindingFlags.Public | BindingFlags.Instance);

        FieldInfo[] allFieldInfos = typeof(TChild).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        for (int i = 0; i < allFieldInfos.Length; i++)
        {
            FieldInfo field = allFieldInfos[i];
            Type fieldType = field.FieldType;

            if (fieldType.IsArray && fieldType.GetElementType().IsSubclassOf(jsonableType))
            {
                Type elementType = fieldType.GetElementType();

                if(elementType.IsSubclassOf(jsonableType))
                {
                    object[] array = (object[])field.GetValue(this);
                    object[] duplicateArray = new object[array.Length];
                    for (int j = 0; j < array.Length; j++)
                    {
                        duplicateArray[j] = jsonableDuplicateMethod.Invoke(array[j], null);
                    }
                    field.SetValue(duplicate, duplicateArray);
                }
                else if (elementType.IsSubclassOf(saveableType))
                {
                    object[] array = (object[])field.GetValue(this);
                    object[] duplicateArray = new object[array.Length];
                    for (int j = 0; j < array.Length; j++)
                    {
                        duplicateArray[j] = saveableDeepDuplicateMethod.Invoke(array[j], null);
                    }
                    field.SetValue(duplicate, duplicateArray);
                }
                else
                {
                    object originalFieldObject = field.GetValue(this);
                    field.SetValue(duplicate, originalFieldObject);
                }
                
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

    public static string[] GetFileNames()
    {
        string folderPath = Application.persistentDataPath + "/" + typeof(TChild).Name;
        if (Directory.Exists(folderPath))
        {
            string[] filePaths = Directory.GetFiles(folderPath);
            char[] splitterChars = { '.', '/' };
            string[] names = new string[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++)
            {
                string[] splitPath = filePaths[i].Split(splitterChars, StringSplitOptions.RemoveEmptyEntries);
                names[i] = splitPath[splitPath.Length - 2];
            }
            return names;
        }

        return new string[0];
    }

    public static TChild Load (string name)
    {
        if (SaveableHolder.IsLoaded (name))
            return SaveableHolder.GetSaveable<TChild> (name);

        string fileLocation = Application.persistentDataPath + "/" + typeof(TChild).Name + "/" + name + ".dat";

        if (!File.Exists(fileLocation))
            throw new Exception("A file doesn't exist at the given location.");

        string jsonString = "";
        using (StreamReader sr = File.OpenText(fileLocation))
        {
            jsonString = sr.ReadToEnd();
        }

        TChild childSaveable = CreateInstance<TChild>();

        string[] splitJsonString = jsonString.Split(GetJsonSplitter(), StringSplitOptions.RemoveEmptyEntries);

        childSaveable.SetupFromSplitJsonString(splitJsonString);

        SaveableHolder.AddSaveable (childSaveable);

        return childSaveable;
    }

    public static void Save (TChild childSaveable)
    {
        string[] jsonSplitter = GetJsonSplitter();
        string jsonString = childSaveable.ConvertToJsonString(jsonSplitter);

        string folderPath = Application.persistentDataPath + "/" + typeof(TChild).Name;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = folderPath + "/" + childSaveable.name + ".dat";
        File.WriteAllText(filePath, jsonString);
    }

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

    static string[] GetJsonSplitter()
    {
        string[] jsonSplitter = { "###" + typeof(TChild) + "Splitter###" };
        return jsonSplitter;
    }

    protected abstract string ConvertToJsonString(string[] jsonSplitter);

    protected abstract void SetupFromSplitJsonString(string[] splitJsonString);
}