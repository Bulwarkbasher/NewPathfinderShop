using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: app defaults and campaign defaults
// TODO: combine this with DefaultSettings?
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
    protected CharacterCasterTypes m_DefaultCharacterCasterTypes;

    public static EnumSetting DefaultBooks
    {
        get { return Instance.m_DefaultBooks; }
    }

    public static EnumSetting DefaultCharacterClasses
    {
        get { return Instance.m_DefaultCharacterClasses; }
    }

    public static CharacterCasterTypes DefaultCharacterCasterTypes
    {
        get { return Instance.m_DefaultCharacterCasterTypes; }
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
