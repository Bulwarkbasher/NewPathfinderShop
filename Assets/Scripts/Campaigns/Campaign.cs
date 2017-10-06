using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Campaign : ScriptableObject, ISaveable
{
    public static Campaign Current
    {
        get { return current; }
    }

    protected static Campaign current;
    public string GetFolderPath() { return Application.persistentDataPath + "/Campaigns"; }
    protected readonly static string[] k_JsonSplitter = { "###CampaignSplitter###", };

    public static Campaign Create (string name, bool usesAutomaticBonusProgressionRules)
    {
        Campaign campaign = CreateInstance<Campaign> ();

        if(campaign.CheckName (name) == SaveableExtensions.NameCheckResult.Bad)
            throw new UnityException("Campaign name invalid, contains invalid characters.");
        if(campaign.CheckName (name) == SaveableExtensions.NameCheckResult.IsDefault)
            throw new UnityException("Campaign name invalid, name cannot start with Default");
        
        campaign.name = name;
        campaign.m_UsesAutomaticBonusProgressionRules = usesAutomaticBonusProgressionRules;
        return campaign;
    }

    public static string[] GetCampaignNames ()
    {
        Campaign dummy = CreateInstance<Campaign> ();
        return dummy.GetFileNames ();
    }

    public static void Load (string campaignName)
    {
        Campaign campaign = CreateInstance<Campaign> ();

        string[] splitJsonString = campaign.GetSplitJsonStringsFromFile (campaignName, k_JsonSplitter);
        campaign.name = splitJsonString[0];

        campaign.m_UsesAutomaticBonusProgressionRules = Wrapper<bool>.CreateFromJsonString (splitJsonString[1]);

        campaign.settlements = new Settlement[splitJsonString.Length - 2];
        for (int i = 0; i < campaign.settlements.Length; i++)
        {
            campaign.settlements[i] = Settlement.CreateFromJsonString(splitJsonString[i + 2]);
        }
        
        current = campaign;
    }

    public static void Save()
    {
        string jsonString = "";

        jsonString += Current.name + k_JsonSplitter[0];

        jsonString += Wrapper<bool>.GetJsonString(Current.m_UsesAutomaticBonusProgressionRules) + k_JsonSplitter[0];

        for (int i = 0; i < Current.settlements.Length; i++)
        {
            jsonString += Settlement.GetJsonString(Current.settlements[i]) + k_JsonSplitter[0];
        }

        Current.WriteJsonStringToFile (Current.name, jsonString);
    }

    public Settlement[] settlements = new Settlement[0];

    public static bool UsesAutomaticBonusProgressionRules
    {
        get { return Current.m_UsesAutomaticBonusProgressionRules; }
    }

    private bool m_UsesAutomaticBonusProgressionRules;
}
