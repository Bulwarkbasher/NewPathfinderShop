using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeReadyCash : Saveable<PerSizeReadyCash>
{
    [SerializeField]
    protected int m_StallReadyCash;
    [SerializeField]
    protected int m_BoutiqueReadyCash;
    [SerializeField]
    protected int m_OutletReadyCash;
    [SerializeField]
    protected int m_EmporiumReadyCash;

    public int this [Shop.Size size]
    {
        get
        {
            switch (size)
            {
                case Shop.Size.Stall:
                    return m_StallReadyCash;
                case Shop.Size.Boutique:
                    return m_BoutiqueReadyCash;
                case Shop.Size.Outlet:
                    return m_OutletReadyCash;
                case Shop.Size.Emporium:
                    return m_EmporiumReadyCash;
                default:
                    throw new ArgumentOutOfRangeException("size", size, "Unknown Shop Size.");
            }
        }
    }

    public static PerSizeReadyCash Create (string name, int stall, int boutique, int outlet, int emporium)
    {
        PerSizeReadyCash newPerSizeReadyCash = CreateInstance<PerSizeReadyCash> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerSizeReadyCash.name = name;
        newPerSizeReadyCash.m_StallReadyCash = stall;
        newPerSizeReadyCash.m_BoutiqueReadyCash = boutique;
        newPerSizeReadyCash.m_OutletReadyCash = outlet;
        newPerSizeReadyCash.m_EmporiumReadyCash = emporium;

        Save (newPerSizeReadyCash);

        return newPerSizeReadyCash;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(m_StallReadyCash) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(m_BoutiqueReadyCash) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(m_OutletReadyCash) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(m_EmporiumReadyCash) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString (string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_StallReadyCash = Wrapper<int>.CreateFromJsonString (splitJsonString[1]);
        m_BoutiqueReadyCash = Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        m_OutletReadyCash = Wrapper<int>.CreateFromJsonString(splitJsonString[3]);
        m_EmporiumReadyCash = Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
    }
}
