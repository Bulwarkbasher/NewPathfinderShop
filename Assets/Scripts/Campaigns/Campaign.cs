using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Campaign : ScriptableObject
{
    public static Campaign Current
    {
        get { return current; }
    }

    protected static Campaign current;
    protected readonly static string k_FolderPath = Application.persistentDataPath + "/Campaigns";
    protected readonly static string[] k_JsonSplitter = { "###CampaignSplitter###", };

    public static Campaign Create (string name, bool usesAutomaticBonusProgressionRules)
    {
        Campaign campaign = CreateInstance<Campaign> ();
        campaign.name = name;
        campaign.m_UsesAutomaticBonusProgressionRules = usesAutomaticBonusProgressionRules;
        return campaign;
    }

    public static string[] GetCampaignNames ()
    {
        if (Directory.Exists (k_FolderPath))
        {
            string[] filePaths = Directory.GetFiles (k_FolderPath);
            char[] splitterChars = { '.', '/' };
            string[] names = new string[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++)
            {
                string[] splitPath = filePaths[i].Split (splitterChars, StringSplitOptions.RemoveEmptyEntries);
                names[i] = splitPath[splitPath.Length - 2];
            }
            return names;
        }

        return new string[0];
    }

    public static void Load (string campaignName)
    {
        string fileLocation = k_FolderPath + "/" + campaignName + ".dat";

        if (!File.Exists(fileLocation))
            throw new Exception("A file doesn't exist at the given location.");

        string jsonString = "";
        using (StreamReader sr = File.OpenText(fileLocation))
        {
            jsonString = sr.ReadToEnd();
        }

        Campaign campaign = CreateInstance<Campaign> ();

        string[] splitJsonString = jsonString.Split (k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);
        campaign.name = splitJsonString[0];

        campaign.m_UsesAutomaticBonusProgressionRules = Wrapper<bool>.CreateFromJsonString (splitJsonString[1]);

        campaign.settlements = new Settlement[splitJsonString.Length - 2];
        for (int i = 0; i < campaign.settlements.Length; i++)
        {
            campaign.settlements[i] = Settlement.CreateFromJsonString(splitJsonString[i + 2]);
        }
        
        current = campaign;
    }







    public Settlement[] settlements = new Settlement[0];

    public bool UsesAutomaticBonusProgressionRules
    {
        get { return m_UsesAutomaticBonusProgressionRules; }
    }

    private bool m_UsesAutomaticBonusProgressionRules;
    // TODO: change to static call through current
    public void Save ()
    {
        if (!Directory.Exists(k_FolderPath))
        {
            Directory.CreateDirectory(k_FolderPath);
        }

        string jsonString = "";

        jsonString += name + k_JsonSplitter[0];

        jsonString += Wrapper<bool>.GetJsonString (m_UsesAutomaticBonusProgressionRules) + k_JsonSplitter[0];

        for (int i = 0; i < settlements.Length; i++)
        {
            jsonString += Settlement.GetJsonString (settlements[i]) + k_JsonSplitter[0];
        }

        string filePath = k_FolderPath + "/" + name + ".dat";
        File.WriteAllText(filePath, jsonString);
    }
}
