using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeRestockSettings : ScriptableObject, ISaveable
{
    public string GetFolderPath() { return Application.persistentDataPath + "/PerSizeRestockSettings"; }
    protected readonly static string[] k_JsonSplitter = { "###PerSizeRestockSplitter###", };

    [SerializeField]
    protected RestockSettings m_ThorpeRestockSettings;
    [SerializeField]
    protected RestockSettings m_HamletRestockSettings;
    [SerializeField]
    protected RestockSettings m_VillageRestockSettings;
    [SerializeField]
    protected RestockSettings m_SmallTownRestockSettings;
    [SerializeField]
    protected RestockSettings m_LargeTownRestockSettings;
    [SerializeField]
    protected RestockSettings m_SmallCityRestockSettings;
    [SerializeField]
    protected RestockSettings m_LargeCityRestockSettings;
    [SerializeField]
    protected RestockSettings m_MetropolisRestockSettings;

    public RestockSettings this [Settlement.Size size]
    {
        get
        {
            switch (size)
            {
                case Settlement.Size.Thorpe:
                    return m_ThorpeRestockSettings;
                case Settlement.Size.Hamlet:
                    return m_HamletRestockSettings;
                case Settlement.Size.Village:
                    return m_VillageRestockSettings;
                case Settlement.Size.SmallTown:
                    return m_SmallTownRestockSettings;
                case Settlement.Size.LargeTown:
                    return m_LargeTownRestockSettings;
                case Settlement.Size.SmallCity:
                    return m_SmallCityRestockSettings;
                case Settlement.Size.LargeCity:
                    return m_LargeCityRestockSettings;
                case Settlement.Size.Metropolis:
                    return m_MetropolisRestockSettings;
                default:
                    throw new ArgumentOutOfRangeException ("size", size, "Unknown Settlement Size.");
            }
        }
    }

    public static PerSizeRestockSettings Create (string name, RestockSettings thorpe, RestockSettings hamlet, RestockSettings village, RestockSettings smallTown, RestockSettings largeTown, RestockSettings smallCity, RestockSettings largeCity, RestockSettings metropolis)
    {
        PerSizeRestockSettings newPerSizeRestockSettings = CreateInstance<PerSizeRestockSettings> ();

        if (newPerSizeRestockSettings.CheckName(name) == SaveableExtensions.NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (newPerSizeRestockSettings.CheckName(name) == SaveableExtensions.NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerSizeRestockSettings.name = name;
        newPerSizeRestockSettings.m_ThorpeRestockSettings = thorpe;
        newPerSizeRestockSettings.m_HamletRestockSettings = hamlet;
        newPerSizeRestockSettings.m_VillageRestockSettings = village;
        newPerSizeRestockSettings.m_SmallTownRestockSettings = smallTown;
        newPerSizeRestockSettings.m_LargeTownRestockSettings = largeTown;
        newPerSizeRestockSettings.m_SmallCityRestockSettings = smallCity;
        newPerSizeRestockSettings.m_LargeCityRestockSettings = largeCity;
        newPerSizeRestockSettings.m_MetropolisRestockSettings = metropolis;

        newPerSizeRestockSettings.Save ();

        return newPerSizeRestockSettings;
    }

    public void Save ()
    {
        string jsonString = "";

        jsonString += name + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_ThorpeRestockSettings) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_HamletRestockSettings) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_VillageRestockSettings) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_SmallTownRestockSettings) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_LargeTownRestockSettings) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_SmallCityRestockSettings) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_LargeCityRestockSettings) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_MetropolisRestockSettings) + k_JsonSplitter[0];

        this.WriteJsonStringToFile (name, jsonString);
    }

    public static PerSizeRestockSettings Load (string name)
    {
        PerSizeRestockSettings perSizeRestockSettings = CreateInstance<PerSizeRestockSettings> ();

        string[] splitJsonString = perSizeRestockSettings.GetSplitJsonStringsFromFile (name, k_JsonSplitter);

        perSizeRestockSettings.name = splitJsonString[0];
        perSizeRestockSettings.m_ThorpeRestockSettings = RestockSettings.CreateFromJsonString (splitJsonString[1]);
        perSizeRestockSettings.m_HamletRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[2]);
        perSizeRestockSettings.m_VillageRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[3]);
        perSizeRestockSettings.m_SmallTownRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[4]);
        perSizeRestockSettings.m_LargeTownRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[5]);
        perSizeRestockSettings.m_SmallCityRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[6]);
        perSizeRestockSettings.m_LargeCityRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[7]);
        perSizeRestockSettings.m_MetropolisRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[8]);

        return perSizeRestockSettings;
    }

    public static string[] GetSettingsNames ()
    {
        PerSizeRestockSettings dummy = CreateInstance<PerSizeRestockSettings> ();
        return dummy.GetFileNames ();
    }
}
