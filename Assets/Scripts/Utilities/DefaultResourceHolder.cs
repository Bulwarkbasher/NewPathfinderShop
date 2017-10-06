using System.Diagnostics.SymbolStore;
using UnityEngine;

public class DefaultResourceHolder : MonoBehaviour
{
    public static DefaultResourceHolder Instance
    {
        get { return s_Instance; }
        private set { s_Instance = value; }
    }

    static DefaultResourceHolder s_Instance;

    [Header("Settings")]
    [SerializeField]
    protected PerSizeRestockSettings m_DefaultPerSizeRestockSettings;
    [SerializeField]
    protected PerStockTypePerSizeAvailability m_DefaultPerStockTypePerSizeAvailability;
    [SerializeField]
    protected PerSizeRestockFrequencyModifiers m_DefaultPerSizeRestockFrequencyModifiers;

    [Header ("Items")]
    [SerializeField]
    protected ArmourCollection m_DefaultArmourCollection;
    [SerializeField]
    protected SpellCollection m_DefaultSpellCollection;
    [SerializeField]
    protected WeaponCollection m_DefaultWeaponCollection;

    [Header ("Qualities")]
    [SerializeField]
    protected ArmourQualityCollection m_DefaultArmourQualityCollection;
    [SerializeField]
    protected WeaponQualityCollection m_DefaultWeaponQualityCollection;

    public static PerSizeRestockSettings DefaultPerSizeRestockSettings
    {
        get { return Instance.m_DefaultPerSizeRestockSettings; }
    }

    public static PerStockTypePerSizeAvailability DefaultPerStockTypePerSizeAvailability
    {
        get { return Instance.m_DefaultPerStockTypePerSizeAvailability; }
    }

    public static PerSizeRestockFrequencyModifiers DefaultPerSizeRestockFrequencyModifiers
    {
        get { return Instance.m_DefaultPerSizeRestockFrequencyModifiers; }
    }

    public static ArmourCollection DefaultArmourCollection
    {
        get { return Instance.m_DefaultArmourCollection; }
    }

    public static SpellCollection DefaultSpellCollection
    {
        get { return Instance.m_DefaultSpellCollection; }
    }

    public static WeaponCollection DefaultWeaponCollection
    {
        get { return Instance.m_DefaultWeaponCollection; }
    }

    public static ArmourQualityCollection DefaultArmourQualityCollection
    {
        get { return Instance.m_DefaultArmourQualityCollection; }
    }

    public static WeaponQualityCollection DefaultWeaponQualityCollection
    {
        get { return Instance.m_DefaultWeaponQualityCollection; }
    }

    void Awake ()
    {
        if (Instance != null)
        {
            Destroy (gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
