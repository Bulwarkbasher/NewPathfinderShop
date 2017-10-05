using System;
using System.IO;
using UnityEngine;

// TODO NEXT: implement this on SpecificWeaponCollection, WeaponQualityCollection and WeaponCollection
// TODO AFTER THAT: make defaults for each other those and put them on DefaultResourceHolder
// TODO: sanitize inputs for file names by disallowing #
public interface ISaveable
{
    string GetFolderPath ();
}


public static class SaveableExtensions
{
    public static void WriteJsonStringToFile(this ISaveable saveable, string name, string jsonString)
    {
        if (!Directory.Exists(saveable.GetFolderPath ()))
        {
            Directory.CreateDirectory(saveable.GetFolderPath());
        }

        string filePath = saveable.GetFolderPath() + "/" + name + ".dat";
        File.WriteAllText(filePath, jsonString);
    }

    public static string[] GetFileNames (this ISaveable saveable)
    {
        if (Directory.Exists(saveable.GetFolderPath()))
        {
            string[] filePaths = Directory.GetFiles(saveable.GetFolderPath());
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

    public static string[] GetSplitJsonStringsFromFile(this ISaveable saveable, string name, string[] jsonSplitter)
    {
        string fileLocation = saveable.GetFolderPath() + "/" + name + ".dat";

        if (!File.Exists(fileLocation))
            throw new Exception("A file doesn't exist at the given location.");

        string jsonString = "";
        using (StreamReader sr = File.OpenText(fileLocation))
        {
            jsonString = sr.ReadToEnd();
        }

        return jsonString.Split(jsonSplitter, StringSplitOptions.RemoveEmptyEntries);
    }
}
