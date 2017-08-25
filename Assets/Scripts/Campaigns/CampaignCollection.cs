using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignCollection : SaveableToDiskScriptableObject<CampaignCollection>
{
    public static CampaignCollection Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = CreateInstance<CampaignCollection>();
            return m_Instance;
        }
    }

    public static Campaign Current
    {
        get { return Instance.m_Campaigns[Instance.m_CurrentCampaignIndex]; }
    }


    private Campaign[] m_Campaigns = new Campaign[0];
    private static CampaignCollection m_Instance;
    private int m_CurrentCampaignIndex;

    public override string GetJsonString()
    {
        throw new NotImplementedException();
    }
}
