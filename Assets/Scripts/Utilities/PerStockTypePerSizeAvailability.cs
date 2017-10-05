using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PerStockTypePerSizeAvailability : ScriptableObject
{
    [SerializeField]
    protected PerSizeAvailability m_ArmourPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_PotionPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_RingPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_RodPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_ScrollPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_StaffPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_WandPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_WeaponPerSizeAvailability;
    [SerializeField]
    protected PerSizeAvailability m_WondrousPerSizeAvailability;

    public PerSizeAvailability this [Shop.StockType type]
    {
        get
        {
            switch (type)
            {
                case Shop.StockType.Armour:
                    return m_ArmourPerSizeAvailability;
                case Shop.StockType.Potion:
                    return m_PotionPerSizeAvailability;
                case Shop.StockType.Ring:
                    return m_RingPerSizeAvailability;
                case Shop.StockType.Rod:
                    return m_RodPerSizeAvailability;
                case Shop.StockType.Scroll:
                    return m_ScrollPerSizeAvailability;
                case Shop.StockType.Staff:
                    return m_StaffPerSizeAvailability;
                case Shop.StockType.Wand:
                    return m_WandPerSizeAvailability;
                case Shop.StockType.Weapon:
                    return m_WeaponPerSizeAvailability;
                case Shop.StockType.Wondrous:
                    return m_WondrousPerSizeAvailability;
                default:
                    throw new ArgumentOutOfRangeException ("type", type, "Unknown Shop Stock Type.");
            }
        }
    }
}
