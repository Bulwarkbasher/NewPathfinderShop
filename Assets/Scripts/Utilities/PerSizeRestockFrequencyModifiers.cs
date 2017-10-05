using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeRestockFrequencyModifiers : ScriptableObject, ISaveable
{
    public string GetFolderPath() { return Application.persistentDataPath + "/PerSizeRestockFrequencyModifiers"; }
    protected readonly static string[] k_JsonSplitter = { "###PerSizeRestockFrequencyModifiersSplitter###", };

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

    public void Save ()
    {
        string jsonString = "";

        jsonString += name + k_JsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(m_StallFrequencyModifier) + k_JsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(m_BoutiqueFrequencyModifier) + k_JsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(m_OutletFrequencyModifier) + k_JsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(m_EmporiumFrequencyModifier) + k_JsonSplitter[0];

        this.WriteJsonStringToFile (name, jsonString);
    }

    public static PerSizeRestockFrequencyModifiers Load (string name)
    {
        PerSizeRestockFrequencyModifiers perSizeRestockFrequencyModifiers = CreateInstance<PerSizeRestockFrequencyModifiers> ();

        string[] splitJsonString = perSizeRestockFrequencyModifiers.GetSplitJsonStringsFromFile (name, k_JsonSplitter);

        perSizeRestockFrequencyModifiers.name = splitJsonString[0];
        perSizeRestockFrequencyModifiers.m_StallFrequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[1]);
        perSizeRestockFrequencyModifiers.m_BoutiqueFrequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        perSizeRestockFrequencyModifiers.m_OutletFrequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[3]);
        perSizeRestockFrequencyModifiers.m_EmporiumFrequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[4]);

        return perSizeRestockFrequencyModifiers;
    }

    public static string[] GetSettingsNames ()
    {
        PerSizeRestockFrequencyModifiers dummy = CreateInstance<PerSizeRestockFrequencyModifiers> ();
        return dummy.GetFileNames ();
    }
}
