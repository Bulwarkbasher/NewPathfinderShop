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
    protected RestockSettingsPerSize m_DefaultPerSizeRestockSettings;
    [SerializeField]
    protected AvailabilityPerShopSizePerStockType m_DefaultPerStockTypePerSizeAvailability;
    [SerializeField]
    protected RestockFrequencyModifiersPerSize m_DefaultPerSizeRestockFrequencyModifiers;
    [SerializeField]
    protected WeightingPerRarity m_DefaultRarityWeighting;
    [SerializeField]
    protected ReadyCashPerShopSize m_DefaultPerSizeReadyCash;
    [SerializeField]
    protected RarityPerCharacterClassPerSpellContainer m_DefaultPerContainerPerCreatorRarity;
    [SerializeField]
    protected FloatRangePerPowerLevelPerStockType m_DefaultPerStockTypePerPowerLevelRange;
    [SerializeField]
    protected CasterTypesPerCharacterClass m_DefaultCharacterCasterTypes;

    [Header ("Items")]
    [SerializeField]
    protected ArmourCollection m_DefaultArmourCollection;
    [SerializeField]
    protected SpellCollection m_DefaultSpellCollection;
    [SerializeField]
    protected WeaponCollection m_DefaultWeaponCollection;
    [SerializeField]
    protected RingCollection m_DefaultRingCollection;
    [SerializeField]
    protected RodCollection m_DefaultRodCollection;
    [SerializeField]
    protected StaffCollection m_DefaultStaffCollection;
    [SerializeField]
    protected WondrousCollection m_DefaultWondrousCollection;


    [Header ("Qualities")]
    [SerializeField]
    protected ArmourQualityCollection m_DefaultArmourQualityCollection;
    [SerializeField]
    protected WeaponQualityCollection m_DefaultWeaponQualityCollection;

    [Header("Matrices")]
    [SerializeField]
    protected WeaponQualityConstraintsMatrix m_DefaultWeaponQualityConstraintsMatrix;
    [SerializeField]
    protected ArmourQualityConstraintsMatrix m_DefaultArmourQualityConstraintsMatrix;

    public static RestockSettingsPerSize DefaultPerSizeRestockSettings
    {
        get { return Instance.m_DefaultPerSizeRestockSettings; }
    }

    public static AvailabilityPerShopSizePerStockType DefaultPerStockTypePerSizeAvailability
    {
        get { return Instance.m_DefaultPerStockTypePerSizeAvailability; }
    }

    public static RestockFrequencyModifiersPerSize DefaultPerSizeRestockFrequencyModifiers
    {
        get { return Instance.m_DefaultPerSizeRestockFrequencyModifiers; }
    }

    public static ReadyCashPerShopSize DefaultPerSizeReadyCash
    {
        get { return Instance.m_DefaultPerSizeReadyCash; }
    }

    public static WeightingPerRarity DefaultRarityWeighting
    {
        get { return Instance.m_DefaultRarityWeighting; }
    }

    public static CasterTypesPerCharacterClass DefaultCharacterCasterTypes
    {
        get { return Instance.m_DefaultCharacterCasterTypes; }
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

    public static RingCollection DefaultRingCollection
    {
        get { return Instance.m_DefaultRingCollection; }
    }

    public static RodCollection DefaultRodCollection
    {
        get { return Instance.m_DefaultRodCollection; }
    }

    public static StaffCollection DefaultStaffCollection
    {
        get { return Instance.m_DefaultStaffCollection; }
    }

    public static WondrousCollection DefaultWondrousCollection
    {
        get { return Instance.m_DefaultWondrousCollection; }
    }

    public static ArmourQualityCollection DefaultArmourQualityCollection
    {
        get { return Instance.m_DefaultArmourQualityCollection; }
    }

    public static WeaponQualityCollection DefaultWeaponQualityCollection
    {
        get { return Instance.m_DefaultWeaponQualityCollection; }
    }

    public static WeaponQualityConstraintsMatrix DefaultWeaponQualityConstraintsMatrix
    {
        get { return Instance.m_DefaultWeaponQualityConstraintsMatrix; }
    }

    public static ArmourQualityConstraintsMatrix DefaultArmourQualityConstraintsMatrix
    {
        get { return Instance.m_DefaultArmourQualityConstraintsMatrix; }
    }

    public static RarityPerCharacterClassPerSpellContainer DefaultPerContainerPerCreatorRarity
    {
        get { return Instance.m_DefaultPerContainerPerCreatorRarity; }
    }

    public static FloatRangePerPowerLevelPerStockType DefaultPerStockTypePerPowerLevelRange
    {
        get { return Instance.m_DefaultPerStockTypePerPowerLevelRange; }
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
