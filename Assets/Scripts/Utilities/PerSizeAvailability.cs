using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeAvailability : ScriptableObject, ISaveable
{
    public string GetFolderPath() { return Application.persistentDataPath + "/PerSizeRestockSettings"; }
    protected static readonly string[] k_JsonSplitter = { "###PerSizeAvailabilitySplitter###", };

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

    public void Save ()
    {
        string jsonString = "";

        jsonString += name + k_JsonSplitter[0];
        jsonString += Availability.GetJsonString(m_StallAvailability) + k_JsonSplitter[0];
        jsonString += Availability.GetJsonString(m_BoutiqueAvailability) + k_JsonSplitter[0];
        jsonString += Availability.GetJsonString(m_OutletAvailability) + k_JsonSplitter[0];
        jsonString += Availability.GetJsonString(m_EmporiumAvailability) + k_JsonSplitter[0];

        this.WriteJsonStringToFile (name, jsonString);
    }

    public static PerSizeAvailability Load (string name)
    {
        PerSizeAvailability perSizeAvailability = CreateInstance<PerSizeAvailability> ();

        string[] splitJsonString = perSizeAvailability.GetSplitJsonStringsFromFile (name, k_JsonSplitter);

        perSizeAvailability.name = splitJsonString[0];
        perSizeAvailability.m_StallAvailability = Availability.CreateFromJsonString(splitJsonString[1]);
        perSizeAvailability.m_BoutiqueAvailability = Availability.CreateFromJsonString(splitJsonString[2]);
        perSizeAvailability.m_OutletAvailability = Availability.CreateFromJsonString(splitJsonString[3]);
        perSizeAvailability.m_EmporiumAvailability = Availability.CreateFromJsonString(splitJsonString[4]);

        return perSizeAvailability;
    }

    // Shouldn't be used as the ones loaded are dependent on PerStockTypePerSizeAvailability.
    public static string[] GetSettingsNames ()
    {
        PerSizeAvailability dummy = CreateInstance<PerSizeAvailability> ();
        return dummy.GetFileNames ();
    }
}
