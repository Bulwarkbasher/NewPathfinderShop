using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeAvailability : Saveable<PerSizeAvailability>
{
    [SerializeField]
    protected IntStratRanges m_StallStratRanges;
    [SerializeField]
    protected IntStratRanges m_BoutiqueStratRanges;
    [SerializeField]
    protected IntStratRanges m_OutletStratRanges;
    [SerializeField]
    protected IntStratRanges m_EmporiumStratRanges;

    public IntStratRanges this [Shop.Size size]
    {
        get
        {
            switch (size)
            {
                case Shop.Size.Stall:
                    return m_StallStratRanges;
                case Shop.Size.Boutique:
                    return m_BoutiqueStratRanges;
                case Shop.Size.Outlet:
                    return m_OutletStratRanges;
                case Shop.Size.Emporium:
                    return m_EmporiumStratRanges;
                default:
                    throw new ArgumentOutOfRangeException ("size", size, "Unknown Shop Size.");
            }
        }
    }

    public static PerSizeAvailability Create (string name, IntStratRanges stall, IntStratRanges boutique, IntStratRanges outlet, IntStratRanges emporium)
    {
        PerSizeAvailability newPerSizeAvailability = CreateInstance<PerSizeAvailability> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerSizeAvailability.name = name;
        newPerSizeAvailability.m_StallStratRanges = stall;
        newPerSizeAvailability.m_BoutiqueStratRanges = boutique;
        newPerSizeAvailability.m_OutletStratRanges = outlet;
        newPerSizeAvailability.m_EmporiumStratRanges = emporium;

        SaveableHolder.AddSaveable (newPerSizeAvailability);

        return newPerSizeAvailability;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(m_StallStratRanges) + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(m_BoutiqueStratRanges) + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(m_OutletStratRanges) + jsonSplitter[0];
        jsonString += IntStratRanges.GetJsonString(m_EmporiumStratRanges) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_StallStratRanges = IntStratRanges.CreateFromJsonString(splitJsonString[1]);
        m_BoutiqueStratRanges = IntStratRanges.CreateFromJsonString(splitJsonString[2]);
        m_OutletStratRanges = IntStratRanges.CreateFromJsonString(splitJsonString[3]);
        m_EmporiumStratRanges = IntStratRanges.CreateFromJsonString(splitJsonString[4]);
    }
}
