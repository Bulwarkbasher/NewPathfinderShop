using UnityEngine;
using System;

public class Shop : Jsonable<Shop>
{
    public RarityPerCharacterClassPerSpellContainer RarityPerCharacterClassPerSpellContainer
    {
        get { return m_RarityPerCharacterClassPerSpellContainer; }
    }
    public FloatRangePerPowerLevelPerStockType BudgetRangePerPowerLevelPerStockType
    {
        get { return m_BudgetRangePerPowerLevelPerStockType; }
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

    public string notes;
    public string size;
    public float frequencyModifier;
    public float readyCash;
    public JsonableBoolPerEnumSetting stockTypes;
    public int daysSinceLastRestock;
    public float totalCash;

    public SpecificArmourCollection specificArmourCollection;
    public SpecificPotionCollection specificPotionCollection;
    public SpecificRingCollection specificRingCollection;
    public SpecificRodCollection specificRodCollection;
    public SpecificScrollCollection specificScrollCollection;
    public SpecificStaffCollection specificStaffCollection;
    public SpecificWandCollection specificWandCollection;
    public SpecificWeaponCollection specificWeaponCollection;
    public SpecificWondrousCollection specificWondrousCollection;

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

    public static Shop Create(string name, Settlement settlement, string shopSize)
    {
        Shop newShop = CreateInstance<Shop>();
        newShop.name = name;
        newShop.size = shopSize;
        newShop.frequencyModifier = settlement.RestockFrequencyModifiersPerShopSize[shopSize];
        newShop.readyCash = settlement.ReadyCashPerShopSize[shopSize];

        newShop.specificWeaponCollection = CreateInstance<SpecificWeaponCollection> ();
        newShop.specificArmourCollection = CreateInstance<SpecificArmourCollection>();
        newShop.specificScrollCollection = CreateInstance<SpecificScrollCollection>();
        newShop.specificWandCollection = CreateInstance<SpecificWandCollection>();
        newShop.specificPotionCollection = CreateInstance<SpecificPotionCollection>();
        newShop.specificStaffCollection = CreateInstance<SpecificStaffCollection>();
        newShop.specificRodCollection = CreateInstance<SpecificRodCollection>();
        newShop.specificWondrousCollection = CreateInstance<SpecificWondrousCollection>();

        newShop.m_RarityPerCharacterClassPerSpellContainer = settlement.RarityPerCharacterClassPerSpellContainer;
        newShop.m_BudgetRangePerPowerLevelPerStockType = settlement.BudgetRangePerPowerLevelPerStockType;
        newShop.m_ArmourCollection = settlement.ArmourCollection;
        newShop.m_SpellCollection = settlement.SpellCollection;
        newShop.m_WeaponCollection = settlement.WeaponCollection;
        newShop.m_RingCollection = settlement.RingCollection;
        newShop.m_RodCollection = settlement.RodCollection;
        newShop.m_StaffCollection = settlement.StaffCollection;
        newShop.m_WondrousCollection = settlement.WondrousCollection;
        newShop.m_ArmourQualityCollection = settlement.ArmourQualityCollection;
        newShop.m_WeaponQualityCollection = settlement.WeaponQualityCollection;
        newShop.m_WeaponQualityConstraintsMatrix = settlement.WeaponQualityConstraintsMatrix;
        newShop.m_ArmourQualityConstraintsMatrix = settlement.ArmourQualityConstraintsMatrix;

        return newShop;
    }

    public Settlement GetSettlement ()
    {
        Settlement[] campaignSettlements = Campaign.Current.settlements;

        for (int i = 0; i < campaignSettlements.Length; i++)
        {
            Shop[] settlementShops = campaignSettlements[i].shops;

            for (int j = 0; j < settlementShops.Length; j++)
            {
                if (settlementShops[j] == this)
                    return campaignSettlements[i];
            }
        }

        return null;
    }

    public void AddToTotalCash (int additionalCash)
    {
        totalCash += additionalCash;
    }

    public void SubtractFromTotalCash (int removedCash)
    {
        totalCash -= removedCash;
    }

    public void PassTime (int daysPassed, Settlement settlement)
    {
        int totalDaysSinceLastRestock = daysPassed + daysSinceLastRestock;
        RestockSettings restockSettings = settlement.restockSettings;

        int daysUntilRestock = Mathf.FloorToInt(restockSettings.days.Random() * settlement.RestockFrequencyModifiersPerShopSize[size]);
        while (totalDaysSinceLastRestock - daysUntilRestock > 0)
        {
            Restock (restockSettings);
            totalDaysSinceLastRestock -= daysUntilRestock;
            daysUntilRestock = Mathf.FloorToInt(restockSettings.days.Random() * settlement.RestockFrequencyModifiersPerShopSize[size]);
        }

        daysSinceLastRestock = totalDaysSinceLastRestock;

        totalCash = GetMaxPossibleStockValue () - GetTotalStockValue () + readyCash;
    }

    protected void Restock(RestockSettings restockSettings)
    {
        if (stockTypes["Armour"])
            specificArmourCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Armour"]);
        if (stockTypes["Potion"])
            specificPotionCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Potion"]);
        if (stockTypes["Ring"])
            specificRingCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Ring"]);
        if (stockTypes["Rod"])
            specificRodCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Rod"]);
        if (stockTypes["Scroll"])
            specificScrollCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Scroll"]);
        if (stockTypes["Staff"])
            specificStaffCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Staff"]);
        if (stockTypes["Wand"])
            specificWandCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Wand"]);
        if (stockTypes["Weapon"])
            specificWeaponCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Weapon"]);
        if (stockTypes["Wondrous"])
            specificWondrousCollection.Restock(restockSettings, m_BudgetRangePerPowerLevelPerStockType["Wondrous"]);       
    }

    protected float GetTotalStockValue ()
    {
        float totalStockValue = 0;
        totalStockValue += specificArmourCollection.GetTotalCost ();
        totalStockValue += specificPotionCollection.GetTotalCost();
        totalStockValue += specificRingCollection.GetTotalCost();
        totalStockValue += specificRodCollection.GetTotalCost();
        totalStockValue += specificScrollCollection.GetTotalCost();
        totalStockValue += specificStaffCollection.GetTotalCost();
        totalStockValue += specificWandCollection.GetTotalCost();
        totalStockValue += specificWeaponCollection.GetTotalCost();
        totalStockValue += specificWondrousCollection.GetTotalCost();
        return totalStockValue;
    }

    protected float GetMaxPossibleStockValue ()
    {
        float maxStockValue = 0;
        maxStockValue += specificArmourCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Armour"]);
        maxStockValue += specificPotionCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Potion"]);
        maxStockValue += specificRingCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Ring"]);
        maxStockValue += specificRodCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Rod"]);
        maxStockValue += specificScrollCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Scroll"]);
        maxStockValue += specificStaffCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Staff"]);
        maxStockValue += specificWandCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Wand"]);
        maxStockValue += specificWeaponCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Weapon"]);
        maxStockValue += specificWondrousCollection.GetMaxPossibleValue(m_BudgetRangePerPowerLevelPerStockType["Wondrous"]);
        return maxStockValue;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += size + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(frequencyModifier) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(readyCash) + jsonSplitter[0];
        jsonString += JsonableBoolPerEnumSetting.GetJsonString(stockTypes) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(daysSinceLastRestock) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(totalCash) + jsonSplitter[0];

        jsonString += SpecificArmourCollection.GetJsonString(specificArmourCollection) + jsonSplitter[0];
        jsonString += SpecificPotionCollection.GetJsonString(specificPotionCollection) + jsonSplitter[0];
        jsonString += SpecificRingCollection.GetJsonString(specificRingCollection) + jsonSplitter[0];
        jsonString += SpecificRodCollection.GetJsonString(specificRodCollection) + jsonSplitter[0];
        jsonString += SpecificScrollCollection.GetJsonString(specificScrollCollection) + jsonSplitter[0];
        jsonString += SpecificStaffCollection.GetJsonString(specificStaffCollection) + jsonSplitter[0];
        jsonString += SpecificWandCollection.GetJsonString(specificWandCollection) + jsonSplitter[0];
        jsonString += SpecificWeaponCollection.GetJsonString(specificWeaponCollection) + jsonSplitter[0];
        jsonString += SpecificWondrousCollection.GetJsonString(specificWondrousCollection) + jsonSplitter[0];

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

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        notes = CreateStringFromSafeJson(splitJsonString[1]);
        size = splitJsonString[2];
        frequencyModifier = Wrapper<float>.CreateFromJsonString(splitJsonString[3]);
        readyCash = Wrapper<float>.CreateFromJsonString(splitJsonString[4]);
        stockTypes = JsonableBoolPerEnumSetting.CreateFromJsonString(splitJsonString[5]);
        daysSinceLastRestock = Wrapper<int>.CreateFromJsonString(splitJsonString[6]);
        totalCash = Wrapper<float>.CreateFromJsonString(splitJsonString[7]);

        specificArmourCollection = SpecificArmourCollection.CreateFromJsonString(splitJsonString[8]);
        specificPotionCollection = SpecificPotionCollection.CreateFromJsonString(splitJsonString[9]);
        specificRingCollection = SpecificRingCollection.CreateFromJsonString (splitJsonString[10]);
        specificRodCollection = SpecificRodCollection.CreateFromJsonString (splitJsonString[11]);
        specificScrollCollection = SpecificScrollCollection.CreateFromJsonString (splitJsonString[12]);
        specificStaffCollection = SpecificStaffCollection.CreateFromJsonString (splitJsonString[13]);
        specificWandCollection = SpecificWandCollection.CreateFromJsonString (splitJsonString[14]);
        specificWeaponCollection = SpecificWeaponCollection.CreateFromJsonString(splitJsonString[15]);
        specificWondrousCollection = SpecificWondrousCollection.CreateFromJsonString (splitJsonString[16]);

        m_RarityPerCharacterClassPerSpellContainer = RarityPerCharacterClassPerSpellContainer.Load(splitJsonString[17]);
        m_BudgetRangePerPowerLevelPerStockType = FloatRangePerPowerLevelPerStockType.Load(splitJsonString[18]);
        m_ArmourCollection = ArmourCollection.Load(splitJsonString[19]);
        m_SpellCollection = SpellCollection.Load(splitJsonString[20]);
        m_WeaponCollection = WeaponCollection.Load(splitJsonString[21]);
        m_RingCollection = RingCollection.Load(splitJsonString[22]);
        m_RodCollection = RodCollection.Load(splitJsonString[23]);
        m_StaffCollection = StaffCollection.Load(splitJsonString[24]);
        m_WondrousCollection = WondrousCollection.Load(splitJsonString[25]);
        m_ArmourQualityCollection = ArmourQualityCollection.Load(splitJsonString[26]);
        m_WeaponQualityCollection = WeaponQualityCollection.Load(splitJsonString[27]);
        m_WeaponQualityConstraintsMatrix = WeaponQualityConstraintsMatrix.Load(splitJsonString[28]);
        m_ArmourQualityConstraintsMatrix = ArmourQualityConstraintsMatrix.Load(splitJsonString[29]);
    }
}
