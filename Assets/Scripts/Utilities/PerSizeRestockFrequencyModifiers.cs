using System;
using UnityEngine;

[CreateAssetMenu]
public class PerSizeRestockFrequencyModifiers : ScriptableObject
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
}
