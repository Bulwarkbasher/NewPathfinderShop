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
    protected EnumSetting m_DefaultBooks;
    [SerializeField]
    protected EnumSetting m_DefaultCharacterClasses;
    [SerializeField]
    protected EnumSetting m_DefaultShopSizes;

    public static EnumSetting DefaultBooks
    {
        get { return Instance.m_DefaultBooks; }
    }

    public static EnumSetting DefaultCharacterClasses
    {
        get { return Instance.m_DefaultCharacterClasses; }
    }

    public static EnumSetting DefaultShopSizes
    {
        get { return Instance.m_DefaultShopSizes; }
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
