using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public abstract class SaveableToDiskScriptableObject<TSaveable> : ScriptableObject
    where TSaveable : SaveableToDiskScriptableObject<TSaveable>
{
    protected static string FolderLocation
    {
        get { return Application.persistentDataPath + "/" + typeof(TSaveable).Name; }
    }

    protected string FileLocation
    {
        get { return Application.persistentDataPath + "/" + typeof(TSaveable).Name + "/" + name + ".dat"; }
    }


    public abstract string GetJsonString();


    public void SaveTextFile ()
    {
        if (!Directory.Exists(FolderLocation))
        {
            Directory.CreateDirectory(FolderLocation);
        }

        string jsonString = GetJsonString();

        File.WriteAllText(FileLocation, jsonString);
    }


    public static TSaveable LoadTextFile (string fileLocation, Func<string, TSaveable> CreateFromJsonString)
    {
        if (!File.Exists(fileLocation))
            throw new Exception("A file doesn't exist at the given location.");

        string jsonString = "";
        using (StreamReader sr = File.OpenText(fileLocation))
        {
            jsonString = sr.ReadToEnd();
        }

        TSaveable saveable = CreateFromJsonString(jsonString);

        return saveable;
    }
}
