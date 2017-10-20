using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeAvailability : Saveable<PerSizeAvailability>
{
    [SerializeField]
    protected Availability m_StallAvailability;
    [SerializeField]
    protected Availability m_BoutiqueAvailability;
    [SerializeField]
    protected Availability m_OutletAvailability;
    [SerializeField]
    protected Availability m_EmporiumAvailability;

    public Availability this [Shop.Size size]
    {
        get
        {
            switch (size)
            {
                case Shop.Size.Stall:
                    return m_StallAvailability;
                case Shop.Size.Boutique:
                    return m_BoutiqueAvailability;
                case Shop.Size.Outlet:
                    return m_OutletAvailability;
                case Shop.Size.Emporium:
                    return m_EmporiumAvailability;
                default:
                    throw new ArgumentOutOfRangeException ("size", size, "Unknown Shop Size.");
            }
        }
    }

    public static PerSizeAvailability Create (string name, Availability stall, Availability boutique, Availability outlet, Availability emporium)
    {
        PerSizeAvailability newPerSizeAvailability = CreateInstance<PerSizeAvailability> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerSizeAvailability.name = name;
        newPerSizeAvailability.m_StallAvailability = stall;
        newPerSizeAvailability.m_BoutiqueAvailability = boutique;
        newPerSizeAvailability.m_OutletAvailability = outlet;
        newPerSizeAvailability.m_EmporiumAvailability = emporium;

        SaveableHolder.AddSaveable (newPerSizeAvailability);

        return newPerSizeAvailability;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Availability.GetJsonString(m_StallAvailability) + jsonSplitter[0];
        jsonString += Availability.GetJsonString(m_BoutiqueAvailability) + jsonSplitter[0];
        jsonString += Availability.GetJsonString(m_OutletAvailability) + jsonSplitter[0];
        jsonString += Availability.GetJsonString(m_EmporiumAvailability) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_StallAvailability = Availability.CreateFromJsonString(splitJsonString[1]);
        m_BoutiqueAvailability = Availability.CreateFromJsonString(splitJsonString[2]);
        m_OutletAvailability = Availability.CreateFromJsonString(splitJsonString[3]);
        m_EmporiumAvailability = Availability.CreateFromJsonString(splitJsonString[4]);
    }
}
