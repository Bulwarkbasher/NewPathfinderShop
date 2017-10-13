using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeRestockSettings : Saveable<PerSizeRestockSettings>
{
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

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
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

        Save (newPerSizeRestockSettings);

        return newPerSizeRestockSettings;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_ThorpeRestockSettings) + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_HamletRestockSettings) + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_VillageRestockSettings) + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_SmallTownRestockSettings) + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_LargeTownRestockSettings) + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_SmallCityRestockSettings) + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_LargeCityRestockSettings) + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(m_MetropolisRestockSettings) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_ThorpeRestockSettings = RestockSettings.CreateFromJsonString (splitJsonString[1]);
        m_HamletRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[2]);
        m_VillageRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[3]);
        m_SmallTownRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[4]);
        m_LargeTownRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[5]);
        m_SmallCityRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[6]);
        m_LargeCityRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[7]);
        m_MetropolisRestockSettings = RestockSettings.CreateFromJsonString(splitJsonString[8]);
    }
}
