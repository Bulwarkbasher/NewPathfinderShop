using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeRestockFrequencyModifiers : Saveable<PerSizeRestockFrequencyModifiers>
{
    [SerializeField]
    protected float m_StallFrequencyModifier;
    [SerializeField]
    protected float m_BoutiqueFrequencyModifier;
    [SerializeField]
    protected float m_OutletFrequencyModifier;
    [SerializeField]
    protected float m_EmporiumFrequencyModifier;

    public float this [Shop.Size size]
    {
        get
        {
            switch (size)
            {
                case Shop.Size.Stall:
                    return m_StallFrequencyModifier;
                case Shop.Size.Boutique:
                    return m_BoutiqueFrequencyModifier;
                case Shop.Size.Outlet:
                    return m_OutletFrequencyModifier;
                case Shop.Size.Emporium:
                    return m_EmporiumFrequencyModifier;
                default:
                    throw new ArgumentOutOfRangeException ("size", size, "Unknown Shop Size.");
            }
        }
    }

    public static PerSizeRestockFrequencyModifiers Create (string name, float stall, float boutique, float outlet, float emporium)
    {
        PerSizeRestockFrequencyModifiers newPerSizeRestockFrequencyModifiers = CreateInstance<PerSizeRestockFrequencyModifiers> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerSizeRestockFrequencyModifiers.name = name;
        newPerSizeRestockFrequencyModifiers.m_StallFrequencyModifier = stall;
        newPerSizeRestockFrequencyModifiers.m_BoutiqueFrequencyModifier = boutique;
        newPerSizeRestockFrequencyModifiers.m_OutletFrequencyModifier = outlet;
        newPerSizeRestockFrequencyModifiers.m_EmporiumFrequencyModifier = emporium;

        Save (newPerSizeRestockFrequencyModifiers);

        return newPerSizeRestockFrequencyModifiers;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(m_StallFrequencyModifier) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(m_BoutiqueFrequencyModifier) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(m_OutletFrequencyModifier) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(m_EmporiumFrequencyModifier) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_StallFrequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[1]);
        m_BoutiqueFrequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        m_OutletFrequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[3]);
        m_EmporiumFrequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[4]);
    }
}
