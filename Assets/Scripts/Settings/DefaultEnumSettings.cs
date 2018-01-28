using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnumSettings : MonoBehaviour
{
    public static DefaultEnumSettings Instance
    {
        get { return s_Instance; }
        private set { s_Instance = value; }
    }

    static DefaultEnumSettings s_Instance;

    [SerializeField]
    protected EnumSetting m_Books;
    [SerializeField]
    protected EnumSetting m_CharacterClasses;
    [SerializeField]
    protected EnumSetting m_ShopSizes;
    [SerializeField]
    protected EnumSetting m_SettlementSizes;
    [SerializeField]
    protected EnumSetting m_StockTypes;
    [SerializeField]
    protected EnumSetting m_Rarities;
    [SerializeField]
    protected EnumSetting m_SpellContainers;
    [SerializeField]
    protected EnumSetting m_CasterTypes;
    [SerializeField]
    protected EnumSetting m_Allowances;

    public static EnumSetting Books
    {
        get { return Instance.m_Books; }
    }

    public static EnumSetting CharacterClasses
    {
        get { return Instance.m_CharacterClasses; }
    }

    public static EnumSetting ShopSizes
    {
        get { return Instance.m_ShopSizes; }
    }

    public static EnumSetting SettlementSizes
    {
        get { return Instance.m_SettlementSizes; }
    }

    public static EnumSetting StockTypes
    {
        get { return Instance.m_StockTypes; }
    }

    public static EnumSetting Rarities
    {
        get { return Instance.m_Rarities; }
    }

    public static EnumSetting SpellContainers
    {
        get { return Instance.m_SpellContainers; }
    }

    public static EnumSetting CasterTypes
    {
        get { return Instance.m_CasterTypes; }
    }

    public static EnumSetting Allowances
    {
        get { return Instance.m_Allowances; }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
