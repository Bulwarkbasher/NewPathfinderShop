using System;
using UnityEngine;

public class Campaign : Saveable<Campaign>
{
    public static Campaign Current
    {
        get { return current; }
    }

    protected static Campaign current;
    
    public static Campaign Create (string name, bool usesAutomaticBonusProgressionRules, bool useMinimumCasterLevel, RarityWeighting rarityWeighting)
    {
        Campaign campaign = CreateInstance<Campaign> ();

        if(CheckName (name) == NameCheckResult.Bad)
            throw new UnityException("Campaign name invalid, contains invalid characters.");
        if(CheckName (name) == NameCheckResult.IsDefault)
            throw new UnityException("Campaign name invalid, name cannot start with Default");
        
        campaign.name = name;
        campaign.m_UsesAutomaticBonusProgressionRules = usesAutomaticBonusProgressionRules;
        campaign.m_UsesMinimumCasterLevelForSpellContainerItems = useMinimumCasterLevel;
        campaign.m_RarityWeighting = rarityWeighting;

        SaveableHolder.AddSaveable (campaign);

        return campaign;
    }

    public static Campaign Create (string name, bool usesAutomaticBonusProgressionRules, bool useMinimumCasterLevel)
    {
        return Create (name, usesAutomaticBonusProgressionRules, useMinimumCasterLevel, DefaultResourceHolder.DefaultRarityWeighting);
    }

    public static bool UsesAutomaticBonusProgressionRules
    {
        get { return Current.m_UsesAutomaticBonusProgressionRules; }
    }

    public static bool UsesMinimumCasterLevelForSpellContainerItems
    {
        get { return Current.m_UsesMinimumCasterLevelForSpellContainerItems; }
    }

    public static RarityWeighting RarityWeighting
    {
        get { return Current.m_RarityWeighting; }
    }

    public Settlement[] settlements = new Settlement[0];

    bool m_UsesAutomaticBonusProgressionRules;
    bool m_UsesMinimumCasterLevelForSpellContainerItems;
    RarityWeighting m_RarityWeighting;

    public void PassTime (int daysPassed)
    {
        for (int i = 0; i < settlements.Length; i++)
        {
            settlements[i].PassTime (daysPassed);
        }
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_UsesAutomaticBonusProgressionRules = Wrapper<bool>.CreateFromJsonString(splitJsonString[1]);
        m_UsesMinimumCasterLevelForSpellContainerItems = Wrapper<bool>.CreateFromJsonString(splitJsonString[2]);
        m_RarityWeighting = RarityWeighting.Load(splitJsonString[3]);

        settlements = new Settlement[splitJsonString.Length - 4];
        for (int i = 0; i < settlements.Length; i++)
        {
            settlements[i] = Settlement.CreateFromJsonString(splitJsonString[i + 4]);
        }

        current = this;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(m_UsesAutomaticBonusProgressionRules) + jsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(m_UsesMinimumCasterLevelForSpellContainerItems) + jsonString[0];
        jsonString += m_RarityWeighting.name + jsonString[0];

        for (int i = 0; i < settlements.Length; i++)
        {
            jsonString += Settlement.GetJsonString(settlements[i]) + jsonSplitter[0];
        }

        return jsonString;
    }
}
