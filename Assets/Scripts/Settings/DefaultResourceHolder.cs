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
    protected RestockSettingsPerSettlementSize m_RestockSettingsPerSettlementSize;
    [SerializeField]
    protected AvailabilityPerStockTypePerShopSize m_AvailabilityPerShopSizePerStockType;
    [SerializeField]
    protected RestockFrequencyModifiersPerShopSize m_RestockFrequencyModifiersPerShopSize;
    [SerializeField]
    protected WeightingPerRarity m_WeightingPerRarity;
    [SerializeField]
    protected ReadyCashPerShopSize m_ReadyCashPerShopSize;
    [SerializeField]
    protected RarityPerCharacterClassPerSpellContainer m_DefaultPerContainerPerCreatorRarity;
    [SerializeField]
    protected FloatRangePerPowerLevelPerStockType m_BudgetRangePerPowerLevelPerStockType;
    [SerializeField]
    protected SaveableSelectedEnumPerEnum m_CasterTypesPerCharacterClass;

    [Header ("Items")]
    [SerializeField]
    protected ArmourCollection m_ArmourCollection;
    [SerializeField]
    protected SpellCollection m_SpellCollection;
    [SerializeField]
    protected WeaponCollection m_WeaponCollection;
    [SerializeField]
    protected RingCollection m_RingCollection;
    [SerializeField]
    protected RodCollection m_RodCollection;
    [SerializeField]
    protected StaffCollection m_StaffCollection;
    [SerializeField]
    protected WondrousCollection m_WondrousCollection;


    [Header ("Qualities")]
    [SerializeField]
    protected ArmourQualityCollection m_ArmourQualityCollection;
    [SerializeField]
    protected WeaponQualityCollection m_WeaponQualityCollection;

    [Header("Matrices")]
    [SerializeField]
    protected WeaponQualityConstraintsMatrix m_WeaponQualityConstraintsMatrix;
    [SerializeField]
    protected ArmourQualityConstraintsMatrix m_ArmourQualityConstraintsMatrix;

    public static RestockSettingsPerSettlementSize RestockSettingsPerSettlementSize
    {
        get { return Instance.m_RestockSettingsPerSettlementSize; }
    }

    public static AvailabilityPerStockTypePerShopSize AvailabilityPerShopSizePerStockType
    {
        get { return Instance.m_AvailabilityPerShopSizePerStockType; }
    }

    public static RestockFrequencyModifiersPerShopSize RestockFrequencyModifiersPerShopSize
    {
        get { return Instance.m_RestockFrequencyModifiersPerShopSize; }
    }

    public static ReadyCashPerShopSize ReadyCashPerShopSize
    {
        get { return Instance.m_ReadyCashPerShopSize; }
    }

    public static WeightingPerRarity WeightingPerRarity
    {
        get { return Instance.m_WeightingPerRarity; }
    }

    public static SaveableSelectedEnumPerEnum CasterTypesPerCharacterClass
    {
        get { return Instance.m_CasterTypesPerCharacterClass; }
    }

    public static ArmourCollection ArmourCollection
    {
        get { return Instance.m_ArmourCollection; }
    }

    public static SpellCollection SpellCollection
    {
        get { return Instance.m_SpellCollection; }
    }

    public static WeaponCollection WeaponCollection
    {
        get { return Instance.m_WeaponCollection; }
    }

    public static RingCollection RingCollection
    {
        get { return Instance.m_RingCollection; }
    }

    public static RodCollection RodCollection
    {
        get { return Instance.m_RodCollection; }
    }

    public static StaffCollection StaffCollection
    {
        get { return Instance.m_StaffCollection; }
    }

    public static WondrousCollection WondrousCollection
    {
        get { return Instance.m_WondrousCollection; }
    }

    public static ArmourQualityCollection ArmourQualityCollection
    {
        get { return Instance.m_ArmourQualityCollection; }
    }

    public static WeaponQualityCollection WeaponQualityCollection
    {
        get { return Instance.m_WeaponQualityCollection; }
    }

    public static WeaponQualityConstraintsMatrix WeaponQualityConstraintsMatrix
    {
        get { return Instance.m_WeaponQualityConstraintsMatrix; }
    }

    public static ArmourQualityConstraintsMatrix ArmourQualityConstraintsMatrix
    {
        get { return Instance.m_ArmourQualityConstraintsMatrix; }
    }

    public static RarityPerCharacterClassPerSpellContainer RarityPerCharacterClassPerSpellContainer
    {
        get { return Instance.m_DefaultPerContainerPerCreatorRarity; }
    }

    public static FloatRangePerPowerLevelPerStockType BudgetRangePerPowerLevelPerStockType
    {
        get { return Instance.m_BudgetRangePerPowerLevelPerStockType; }
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
