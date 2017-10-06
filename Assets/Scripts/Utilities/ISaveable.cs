using System;
using System.IO;
using UnityEngine;

// TODO: everything that implements this has a default - make sure defaults can't be saved to but have overloaded Load function
// TODO: Create functions that duplicate the an existing ISaveable - change normal Create functions to CreateBlank (Duplicate functions instead?)
// TODO: make sure all Create functions call a Save function
public interface ISaveable
{
    string GetFolderPath ();
}


public static class SaveableExtensions
{
    public enum NameCheckResult
    {
        Good, Bad, IsDefault,
    }

    public static NameCheckResult CheckName (this ISaveable saveable, string name)
    {
        if (name.StartsWith ("Default"))
            return NameCheckResult.IsDefault;
        
        if(name.Contains ("#"))
            return NameCheckResult.Bad;

        if(name.IndexOfAny (Path.GetInvalidFileNameChars ()) >= 0)
            return NameCheckResult.Bad;
        
        return NameCheckResult.Good;
    }

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
