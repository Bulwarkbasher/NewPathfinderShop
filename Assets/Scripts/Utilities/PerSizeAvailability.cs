using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeAvailability : ScriptableObject
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
}
