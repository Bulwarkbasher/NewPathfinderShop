using System;
using System.IO;
using UnityEngine;

// NOTE: all front end saving should just call SaveableHolder.Save
public abstract class Saveable<TChild> : Jsonable<TChild>
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
    
    public static TChild Duplicate(string duplicatesName, TChild original)
    {
        TChild duplicate = original.MemberwiseClone () as TChild;
        duplicate.name = duplicatesName;
        Save(duplicate);

        duplicate = Load(duplicatesName);
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

        TChild childSaveable = CreateFromJsonString (jsonString);

        SaveableHolder.AddSaveable (childSaveable);

        return childSaveable;
    }

    public static void Save (TChild childSaveable)
    {
        string jsonString = GetJsonString (childSaveable);

        string folderPath = Application.persistentDataPath + "/" + typeof(TChild).Name;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = folderPath + "/" + childSaveable.name + ".dat";
        File.WriteAllText(filePath, jsonString);
    }
}