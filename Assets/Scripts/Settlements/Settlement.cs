using UnityEngine;
using System;

// IMPORTANT NOTE: don't do static settlements and shops as it'll mess up time passing because
// they'll all reference the current settlement/shop for their settings
public class Settlement : Jsonable<Settlement>
{
    public enum Size
    {
        Thorpe,
        Hamlet,
        Village,
        SmallTown,
        LargeTown,
        SmallCity,
        LargeCity,
        Metropolis,
    }

    public AvailabilityPerStockTypePerShopSize AvailabilityPerShopSizePerStockType
    {
        get { return m_AvailabilityPerShopSizePerStockType; }
    }

    public RestockFrequencyModifiersPerShopSize RestockFrequencyModifiersPerShopSize
    {
        get { return m_RestockFrequencyModifiersPerShopSize; }
    }

    public ReadyCashPerShopSize ReadyCashPerShopSize
    {
        get { return m_ReadyCashPerShopSize; }
    }

    public ArmourCollection ArmourCollection
    {
        get { return m_ArmourCollection; }
    }

    public SpellCollection SpellCollection
    {
        get { return m_SpellCollection; }
    }

    public WeaponCollection WeaponCollection
    {
        get { return m_WeaponCollection; }
    }

    public RingCollection RingCollection
    {
        get { return m_RingCollection; }
    }

    public RodCollection RodCollection
    {
        get { return m_RodCollection; }
    }

    public StaffCollection StaffCollection
    {
        get { return m_StaffCollection; }
    }

    public WondrousCollection WondrousCollection
    {
        get { return m_WondrousCollection; }
    }

    public ArmourQualityCollection ArmourQualityCollection
    {
        get { return m_ArmourQualityCollection; }
    }

    public WeaponQualityCollection WeaponQualityCollection
    {
        get { return m_WeaponQualityCollection; }
    }

    public WeaponQualityConstraintsMatrix WeaponQualityConstraintsMatrix
    {
        get { return m_WeaponQualityConstraintsMatrix; }
    }

    public ArmourQualityConstraintsMatrix ArmourQualityConstraintsMatrix
    {
        get { return m_ArmourQualityConstraintsMatrix; }
    }

    public RarityPerCharacterClassPerSpellContainer RarityPerCharacterClassPerSpellContainer
    {
        get { return m_RarityPerCharacterClassPerSpellContainer; }
    }

    public FloatRangePerPowerLevelPerStockType BudgetRangePerPowerLevelPerStockType
    {
        get { return m_BudgetRangePerPowerLevelPerStockType; }
    }

    public string notes;
    public Size size;
    public RestockSettings restockSettings;
    public Shop[] shops = new Shop[0];

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

    public static Settlement Create (string name, string notes, Size size)
    {
        Settlement newSettlement = CreateInstance<Settlement>();
        newSettlement.name = name;
        newSettlement.notes = notes;
        newSettlement.size = size;
        newSettlement.restockSettings = Campaign.RestockSettingsPerSettlementSize[size];
        newSettlement.m_AvailabilityPerShopSizePerStockType = Campaign.AvailabilityPerStockTypePerShopSize;
        newSettlement.m_RestockFrequencyModifiersPerShopSize = Campaign.RestockFrequencyModifiersPerShopSize;
        newSettlement.m_ReadyCashPerShopSize = Campaign.ReadyCashPerShopSize;
        newSettlement.m_RarityPerCharacterClassPerSpellContainer = Campaign.RarityPerCharacterClassPerSpellContainer;
        newSettlement.m_BudgetRangePerPowerLevelPerStockType = Campaign.BudgetRangePerPowerLevelPerStockType;
        newSettlement.m_ArmourCollection = Campaign.ArmourCollection;
        newSettlement.m_SpellCollection = Campaign.SpellCollection;
        newSettlement.m_WeaponCollection = Campaign.WeaponCollection;
        newSettlement.m_RingCollection = Campaign.RingCollection;
        newSettlement.m_RodCollection = Campaign.RodCollection;
        newSettlement.m_StaffCollection = Campaign.StaffCollection;
        newSettlement.m_WondrousCollection = Campaign.WondrousCollection;
        newSettlement.m_ArmourQualityCollection = Campaign.ArmourQualityCollection;
        newSettlement.m_WeaponQualityCollection = Campaign.WeaponQualityCollection;
        newSettlement.m_WeaponQualityConstraintsMatrix = Campaign.WeaponQualityConstraintsMatrix;
        newSettlement.m_ArmourQualityConstraintsMatrix = Campaign.ArmourQualityConstraintsMatrix;
        return newSettlement;
    }

    public void AddShop (string shopName, string shopNotes, string shopSize)
    {
        Shop[] newShops = new Shop[shops.Length + 1];

        for (int i = 0; i < shops.Length; i++)
        {
            newShops[i] = shops[i];
        }

        newShops[shops.Length] = Shop.Create (shopName, this, shopSize);
    }

    public void PassTime (int daysPassed)
    {
        for (int i = 0; i < shops.Length; i++)
        {
            shops[i].PassTime (daysPassed, this);
        }
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += notes + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)size) + jsonSplitter[0];
        jsonString += RestockSettings.GetJsonString(restockSettings) + jsonSplitter[0];

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


        for (int i = 0; i < shops.Length; i++)
        {
            jsonString += Shop.GetJsonString(shops[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        notes = splitJsonString[1];
        size = (Size)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        restockSettings = RestockSettings.CreateFromJsonString(splitJsonString[3]);

        m_AvailabilityPerShopSizePerStockType = AvailabilityPerStockTypePerShopSize.Load(splitJsonString[4]);
        m_RestockFrequencyModifiersPerShopSize = RestockFrequencyModifiersPerShopSize.Load(splitJsonString[5]);
        m_ReadyCashPerShopSize = ReadyCashPerShopSize.Load(splitJsonString[6]);
        m_RarityPerCharacterClassPerSpellContainer = RarityPerCharacterClassPerSpellContainer.Load(splitJsonString[7]);
        m_BudgetRangePerPowerLevelPerStockType = FloatRangePerPowerLevelPerStockType.Load(splitJsonString[8]);
        m_ArmourCollection = ArmourCollection.Load(splitJsonString[9]);
        m_SpellCollection = SpellCollection.Load(splitJsonString[10]);
        m_WeaponCollection = WeaponCollection.Load(splitJsonString[11]);
        m_RingCollection = RingCollection.Load(splitJsonString[12]);
        m_RodCollection = RodCollection.Load(splitJsonString[13]);
        m_StaffCollection = StaffCollection.Load(splitJsonString[14]);
        m_WondrousCollection = WondrousCollection.Load(splitJsonString[15]);
        m_ArmourQualityCollection = ArmourQualityCollection.Load(splitJsonString[16]);
        m_WeaponQualityCollection = WeaponQualityCollection.Load(splitJsonString[17]);
        m_WeaponQualityConstraintsMatrix = WeaponQualityConstraintsMatrix.Load(splitJsonString[18]);
        m_ArmourQualityConstraintsMatrix = ArmourQualityConstraintsMatrix.Load(splitJsonString[19]);

        shops = new Shop[splitJsonString.Length - 20];
        for (int i = 0; i < shops.Length; i++)
        {
            shops[i] = Shop.CreateFromJsonString(splitJsonString[i + 20]);
        }
    }
}

