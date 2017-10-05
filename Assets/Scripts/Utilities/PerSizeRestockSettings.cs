using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeRestockSettings : ScriptableObject
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
}
