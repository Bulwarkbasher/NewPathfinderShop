using System;
using UnityEngine;

public class Campaign : Saveable<Campaign>
{
    public static Campaign Current
    {
        get { return current; }
    }

    protected static Campaign current;

    public static EnumSetting Books
    {
        get { return Current.m_Books; }
        set { if (!Current.SettingsAreLocked) Current.m_Books = value; }
    }

    public static EnumSetting CharacterClasses
    {
        get { return Current.m_CharacterClasses; }
        set { if (!Current.SettingsAreLocked) Current.m_CharacterClasses = value; }
    }

    public static EnumSetting ShopSizes
    {
        get { return Current.m_ShopSizes; }
        set { if (!Current.SettingsAreLocked) Current.m_ShopSizes = value; }
    }

    public static EnumSetting SettlementSizes
    {
        get { return Current.m_SettlementSizes; }
        set { if (!Current.SettingsAreLocked) Current.m_SettlementSizes = value; }
    }

    public static EnumSetting Rarities
    {
        get { return Current.m_Rarities; }
        set { if (!Current.SettingsAreLocked) Current.m_Rarities = value; }
    }

    public static bool UsesAutomaticBonusProgressionRules
    {
        get { return Current.m_UsesAutomaticBonusProgressionRules; }
        set { if (!Current.SettingsAreLocked) Current.m_UsesAutomaticBonusProgressionRules = value; }
    }

    public static bool UsesMinimumCasterLevelForSpellContainerItems
    {
        get { return Current.m_UsesMinimumCasterLevelForSpellContainerItems; }
        set { if (!Current.SettingsAreLocked) Current.m_UsesMinimumCasterLevelForSpellContainerItems = value; }
    }

    public static SaveableSelectedEnumPerEnum CasterTypesPerCharacterClass
    {
        get { return Current.m_CasterTypesPerCharacterClass; }
        set { if (!Current.SettingsAreLocked) Current.m_CasterTypesPerCharacterClass = value; }
    }

    public static WeightingPerRarity WeightingPerRarity
    {
        get { return Current.m_WeightingPerRarity; }
        set { if (!Current.SettingsAreLocked) Current.m_WeightingPerRarity = value; }
    }

    public static RestockSettingsPerSettlementSize RestockSettingsPerSettlementSize
    {
        get { return Current.m_RestockSettingsPerSettlementSize; }
        set { if (!Current.SettingsAreLocked) Current.m_RestockSettingsPerSettlementSize = value; }
    }

    public static AvailabilityPerStockTypePerShopSize AvailabilityPerStockTypePerShopSize
    {
        get { return Current.m_AvailabilityPerShopSizePerStockType; }
        set { if (!Current.SettingsAreLocked) Current.m_AvailabilityPerShopSizePerStockType = value; }
    }

    public static RestockFrequencyModifiersPerShopSize RestockFrequencyModifiersPerShopSize
    {
        get { return Current.m_RestockFrequencyModifiersPerShopSize; }
        set { if (!Current.SettingsAreLocked) Current.m_RestockFrequencyModifiersPerShopSize = value; }
    }

    public static ReadyCashPerShopSize ReadyCashPerShopSize
    {
        get { return Current.m_ReadyCashPerShopSize; }
        set { if (!Current.SettingsAreLocked) Current.m_ReadyCashPerShopSize = value; }
    }

    public static ArmourCollection ArmourCollection
    {
        get { return Current.m_ArmourCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_ArmourCollection = value; }
    }

    public static SpellCollection SpellCollection
    {
        get { return Current.m_SpellCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_SpellCollection = value; }
    }

    public static WeaponCollection WeaponCollection
    {
        get { return Current.m_WeaponCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_WeaponCollection = value; }
    }

    public static RingCollection RingCollection
    {
        get { return Current.m_RingCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_RingCollection = value; }
    }

    public static RodCollection RodCollection
    {
        get { return Current.m_RodCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_RodCollection = value; }
    }

    public static StaffCollection StaffCollection
    {
        get { return Current.m_StaffCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_StaffCollection = value; }
    }

    public static WondrousCollection WondrousCollection
    {
        get { return Current.m_WondrousCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_WondrousCollection = value; }
    }

    public static ArmourQualityCollection ArmourQualityCollection
    {
        get { return Current.m_ArmourQualityCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_ArmourQualityCollection = value; }
    }

    public static WeaponQualityCollection WeaponQualityCollection
    {
        get { return Current.m_WeaponQualityCollection; }
        set { if (!Current.SettingsAreLocked) Current.m_WeaponQualityCollection = value; }
    }

    public static WeaponQualityConstraintsMatrix WeaponQualityConstraintsMatrix
    {
        get { return Current.m_WeaponQualityConstraintsMatrix; }
        set { if (!Current.SettingsAreLocked) Current.m_WeaponQualityConstraintsMatrix = value; }
    }

    public static ArmourQualityConstraintsMatrix ArmourQualityConstraintsMatrix
    {
        get { return Current.m_ArmourQualityConstraintsMatrix; }
        set { if (!Current.SettingsAreLocked) Current.m_ArmourQualityConstraintsMatrix = value; }
    }

    public static RarityPerCharacterClassPerSpellContainer RarityPerCharacterClassPerSpellContainer
    {
        get { return Current.m_RarityPerCharacterClassPerSpellContainer; }
        set { if (!Current.SettingsAreLocked) Current.m_RarityPerCharacterClassPerSpellContainer = value; }
    }

    public static FloatRangePerPowerLevelPerStockType BudgetRangePerPowerLevelPerStockType
    {
        get { return Current.m_BudgetRangePerPowerLevelPerStockType; }
        set { if (!Current.SettingsAreLocked) Current.m_BudgetRangePerPowerLevelPerStockType = value; }
    }

    // Settings
    protected EnumSetting m_Books;
    protected EnumSetting m_CharacterClasses;
    protected EnumSetting m_ShopSizes;
    protected EnumSetting m_SettlementSizes;
    protected EnumSetting m_Rarities;
    protected bool m_UsesAutomaticBonusProgressionRules;
    protected bool m_UsesMinimumCasterLevelForSpellContainerItems;
    protected SaveableSelectedEnumPerEnum m_CasterTypesPerCharacterClass;
    protected WeightingPerRarity m_WeightingPerRarity;
    protected RestockSettingsPerSettlementSize m_RestockSettingsPerSettlementSize;
    protected AvailabilityPerStockTypePerShopSize m_AvailabilityPerShopSizePerStockType;
    protected RestockFrequencyModifiersPerShopSize m_RestockFrequencyModifiersPerShopSize;
    protected ReadyCashPerShopSize m_ReadyCashPerShopSize;
    protected RarityPerCharacterClassPerSpellContainer m_RarityPerCharacterClassPerSpellContainer;
    protected FloatRangePerPowerLevelPerStockType m_BudgetRangePerPowerLevelPerStockType;
    protected ArmourCollection m_ArmourCollection;
    protected SpellCollection m_SpellCollection;
    protected WeaponCollection m_WeaponCollection;
    protected RingCollection m_RingCollection;
    protected RodCollection m_RodCollection;
    protected StaffCollection m_StaffCollection;
    protected WondrousCollection m_WondrousCollection;
    protected ArmourQualityCollection m_ArmourQualityCollection;
    protected WeaponQualityCollection m_WeaponQualityCollection;
    protected WeaponQualityConstraintsMatrix m_WeaponQualityConstraintsMatrix;
    protected ArmourQualityConstraintsMatrix m_ArmourQualityConstraintsMatrix;

    public string notes;
    public Settlement[] settlements = new Settlement[0];

    public bool SettingsAreLocked
    {
        get { return settlements.Length > 0; }
    }

    public static Campaign Create(string name, bool usesAutomaticBonusProgressionRules, bool useMinimumCasterLevel)
    {
        Campaign campaign = CreateInstance<Campaign>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Campaign name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Campaign name invalid, name cannot start with Default");

        campaign.name = name;
        campaign.m_Books = DefaultEnumSettings.Books;
        campaign.m_CharacterClasses = DefaultEnumSettings.CharacterClasses;
        campaign.m_ShopSizes = DefaultEnumSettings.ShopSizes;
        campaign.m_SettlementSizes = DefaultEnumSettings.SettlementSizes;
        campaign.m_Rarities = DefaultEnumSettings.Rarities;
        campaign.m_UsesAutomaticBonusProgressionRules = usesAutomaticBonusProgressionRules;
        campaign.m_UsesMinimumCasterLevelForSpellContainerItems = useMinimumCasterLevel;
        campaign.m_CasterTypesPerCharacterClass = DefaultResourceHolder.CasterTypesPerCharacterClass;
        campaign.m_WeightingPerRarity = DefaultResourceHolder.WeightingPerRarity;
        campaign.m_RestockSettingsPerSettlementSize = DefaultResourceHolder.RestockSettingsPerSettlementSize;
        campaign.m_AvailabilityPerShopSizePerStockType = DefaultResourceHolder.AvailabilityPerShopSizePerStockType;
        campaign.m_RestockFrequencyModifiersPerShopSize = DefaultResourceHolder.RestockFrequencyModifiersPerShopSize;
        campaign.m_ReadyCashPerShopSize = DefaultResourceHolder.ReadyCashPerShopSize;
        campaign.m_RarityPerCharacterClassPerSpellContainer = DefaultResourceHolder.RarityPerCharacterClassPerSpellContainer;
        campaign.m_BudgetRangePerPowerLevelPerStockType = DefaultResourceHolder.BudgetRangePerPowerLevelPerStockType;
        campaign.m_ArmourCollection = DefaultResourceHolder.ArmourCollection;
        campaign.m_SpellCollection = DefaultResourceHolder.SpellCollection;
        campaign.m_WeaponCollection = DefaultResourceHolder.WeaponCollection;
        campaign.m_RingCollection = DefaultResourceHolder.RingCollection;
        campaign.m_RodCollection = DefaultResourceHolder.RodCollection;
        campaign.m_StaffCollection = DefaultResourceHolder.StaffCollection;
        campaign.m_WondrousCollection = DefaultResourceHolder.WondrousCollection;
        campaign.m_ArmourQualityCollection = DefaultResourceHolder.ArmourQualityCollection;
        campaign.m_WeaponQualityCollection = DefaultResourceHolder.WeaponQualityCollection;
        campaign.m_WeaponQualityConstraintsMatrix = DefaultResourceHolder.WeaponQualityConstraintsMatrix;
        campaign.m_ArmourQualityConstraintsMatrix = DefaultResourceHolder.ArmourQualityConstraintsMatrix;

        SaveableHolder.AddSaveable(campaign);

        return campaign;
    }

    public void PassTime (int daysPassed)
    {
        for (int i = 0; i < settlements.Length; i++)
        {
            settlements[i].PassTime (daysPassed);
        }
    }

    public void AddSettlement (string settlementName, EnumValue settlementSize)
    {
        Settlement[] newSettlements = new Settlement[settlements.Length + 1];

        for (int i = 0; i < settlements.Length; i++)
        {
            newSettlements[i] = settlements[i];
        }

        newSettlements[settlements.Length] = Settlement.Create(settlementName, settlementSize);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += m_Books.name + jsonSplitter[0];
        jsonString += m_CharacterClasses.name + jsonSplitter[0];
        jsonString += m_ShopSizes.name + jsonSplitter[0];
        jsonString += m_SettlementSizes.name + jsonSplitter[0];
        jsonString += m_Rarities.name + jsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(m_UsesAutomaticBonusProgressionRules) + jsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(m_UsesMinimumCasterLevelForSpellContainerItems) + jsonString[0];
        jsonString += m_CasterTypesPerCharacterClass.name + jsonSplitter[0];
        jsonString += m_WeightingPerRarity.name + jsonString[0];
        jsonString += m_RestockSettingsPerSettlementSize.name + jsonSplitter[0];
        jsonString += m_AvailabilityPerShopSizePerStockType.name + jsonSplitter[0];
        jsonString += m_RestockFrequencyModifiersPerShopSize.name + jsonSplitter[0];
        jsonString += m_ReadyCashPerShopSize.name + jsonSplitter[0];
        jsonString += m_RarityPerCharacterClassPerSpellContainer.name + jsonSplitter[0];
        jsonString += m_BudgetRangePerPowerLevelPerStockType.name + jsonSplitter[0];
        jsonString += m_ArmourCollection.name + jsonSplitter[0];
        jsonString += m_SpellCollection.name + jsonSplitter[0];
        jsonString += m_WeaponCollection.name + jsonSplitter[0];
        jsonString += m_RingCollection.name + jsonSplitter[0];
        jsonString += m_RodCollection.name + jsonSplitter[0];
        jsonString += m_StaffCollection.name + jsonSplitter[0];
        jsonString += m_WondrousCollection.name + jsonSplitter[0];
        jsonString += m_ArmourQualityCollection.name + jsonSplitter[0];
        jsonString += m_WeaponQualityCollection.name + jsonSplitter[0];
        jsonString += m_WeaponQualityConstraintsMatrix.name + jsonSplitter[0];
        jsonString += m_ArmourQualityConstraintsMatrix.name + jsonSplitter[0];

        for (int i = 0; i < settlements.Length; i++)
        {
            jsonString += Settlement.GetJsonString(settlements[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        notes = CreateStringFromSafeJson(splitJsonString[1]);
        m_Books = EnumSetting.Load(splitJsonString[2]);
        m_CharacterClasses = EnumSetting.Load(splitJsonString[3]);
        m_ShopSizes = EnumSetting.Load(splitJsonString[4]);
        m_SettlementSizes = EnumSetting.Load(splitJsonString[5]);
        m_Rarities = EnumSetting.Load(splitJsonString[6]);
        m_UsesAutomaticBonusProgressionRules = Wrapper<bool>.CreateFromJsonString(splitJsonString[7]);
        m_UsesMinimumCasterLevelForSpellContainerItems = Wrapper<bool>.CreateFromJsonString(splitJsonString[8]);
        m_CasterTypesPerCharacterClass = SaveableSelectedEnumPerEnum.Load(splitJsonString[9]);
        m_WeightingPerRarity = WeightingPerRarity.Load(splitJsonString[10]);
        m_RestockSettingsPerSettlementSize = RestockSettingsPerSettlementSize.Load(splitJsonString[11]);
        m_AvailabilityPerShopSizePerStockType = AvailabilityPerStockTypePerShopSize.Load(splitJsonString[12]);
        m_RestockFrequencyModifiersPerShopSize = RestockFrequencyModifiersPerShopSize.Load(splitJsonString[13]);
        m_ReadyCashPerShopSize = ReadyCashPerShopSize.Load(splitJsonString[14]);
        m_RarityPerCharacterClassPerSpellContainer = RarityPerCharacterClassPerSpellContainer.Load(splitJsonString[15]);
        m_BudgetRangePerPowerLevelPerStockType = FloatRangePerPowerLevelPerStockType.Load(splitJsonString[16]);
        m_ArmourCollection = ArmourCollection.Load(splitJsonString[17]);
        m_SpellCollection = SpellCollection.Load(splitJsonString[18]);
        m_WeaponCollection = WeaponCollection.Load(splitJsonString[19]);
        m_RingCollection = RingCollection.Load(splitJsonString[20]);
        m_RodCollection = RodCollection.Load(splitJsonString[21]);
        m_StaffCollection = StaffCollection.Load(splitJsonString[22]);
        m_WondrousCollection = WondrousCollection.Load(splitJsonString[23]);
        m_ArmourQualityCollection = ArmourQualityCollection.Load(splitJsonString[24]);
        m_WeaponQualityCollection = WeaponQualityCollection.Load(splitJsonString[25]);
        m_WeaponQualityConstraintsMatrix = WeaponQualityConstraintsMatrix.Load(splitJsonString[26]);
        m_ArmourQualityConstraintsMatrix = ArmourQualityConstraintsMatrix.Load(splitJsonString[27]);
        
        settlements = new Settlement[splitJsonString.Length - 28];
        for (int i = 0; i < settlements.Length; i++)
        {
            settlements[i] = Settlement.CreateFromJsonString(splitJsonString[i + 28]);
        }

        current = this;
    }
}
