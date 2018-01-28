using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example: IntRange
public abstract class DataPerEnumSetting<TDataPerEnumSetting> : Jsonable<TDataPerEnumSetting>
    where TDataPerEnumSetting : DataPerEnumSetting<TDataPerEnumSetting>
{
    public static TDataPerEnumSetting Create()
    {
        TDataPerEnumSetting newDataPerEnumSetting = CreateInstance<TDataPerEnumSetting>();
        newDataPerEnumSetting.SetDefaults();
        return newDataPerEnumSetting;
    }

    protected virtual void SetDefaults ()
    { }
}

// Example: RestockSettingsPerSettlementSize<RestockSettingsPerSettlementSize, RestockSettings>
public abstract class DataPerEnumSetting<TDataPerEnumSetting, TFirstLevelData> : Saveable<TDataPerEnumSetting>
    where TDataPerEnumSetting : DataPerEnumSetting<TDataPerEnumSetting, TFirstLevelData>
    where TFirstLevelData : DataPerEnumSetting<TFirstLevelData>
{
    [SerializeField]
    protected EnumSetting m_EnumSetting;
    [SerializeField]
    protected TFirstLevelData[] m_Data;

    public TFirstLevelData this[string enumSetting]
    {
        get { return m_Data[m_EnumSetting[enumSetting]]; }
    }

    public static TDataPerEnumSetting Create(EnumSetting enumForFirstLevelData)
    {
        TDataPerEnumSetting newDataPerEnumSetting = CreateInstance<TDataPerEnumSetting>();
        newDataPerEnumSetting.name = "New" + typeof(TDataPerEnumSetting).Name;
        newDataPerEnumSetting.m_EnumSetting = enumForFirstLevelData;
        newDataPerEnumSetting.m_Data = new TFirstLevelData[enumForFirstLevelData.Length];
        for (int i = 0; i < enumForFirstLevelData.Length; i++)
        {
            newDataPerEnumSetting.m_Data[i] = DataPerEnumSetting<TFirstLevelData>.Create();
        }

        SaveableHolder.AddSaveable(newDataPerEnumSetting);

        return newDataPerEnumSetting;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_EnumSetting.name + jsonSplitter[0];
        for (int i = 0; i < m_Data.Length; i++)
        {
            jsonString += DataPerEnumSetting<TFirstLevelData>.GetJsonString(m_Data[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_EnumSetting = EnumSetting.Load(splitJsonString[1]);
        m_Data = new TFirstLevelData[splitJsonString.Length - 2];
        for (int i = 0; i < m_Data.Length; i++)
        {
            m_Data[i] = DataPerEnumSetting<TFirstLevelData>.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}

// Example: FloatRangePerPowerLevelPerStockType<FloatRangePerPowerLevelPerStockType, FloatRangePerPowerLevel, FloatRange>
public abstract class DataPerEnumSetting<TDataPerEnumSetting, TFirstLevelData, TSecondLevelData> : Saveable<TDataPerEnumSetting>
    where TDataPerEnumSetting : DataPerEnumSetting<TDataPerEnumSetting, TFirstLevelData, TSecondLevelData>
    where TFirstLevelData : DataPerEnumSetting<TFirstLevelData, TSecondLevelData>
    where TSecondLevelData : DataPerEnumSetting<TSecondLevelData>
{
    [SerializeField]
    protected EnumSetting m_EnumSetting;
    [SerializeField]
    protected TFirstLevelData[] m_Data;

    public TFirstLevelData this[string enumSetting]
    {
        get { return m_Data[m_EnumSetting[enumSetting]]; }
    }

    public static TDataPerEnumSetting Create (EnumSetting enumForFirstLevelData, EnumSetting enumForSecondLevelData)
    {
        TDataPerEnumSetting newDataPerEnumSetting = CreateInstance<TDataPerEnumSetting>();
        newDataPerEnumSetting.name = "New" + typeof(TDataPerEnumSetting).Name;
        newDataPerEnumSetting.m_EnumSetting = enumForFirstLevelData;
        newDataPerEnumSetting.m_Data = new TFirstLevelData[enumForFirstLevelData.Length];
        for (int i = 0; i < enumForFirstLevelData.Length; i++)
        {
            newDataPerEnumSetting.m_Data[i] = DataPerEnumSetting<TFirstLevelData, TSecondLevelData>.Create(enumForSecondLevelData);
        }

        SaveableHolder.AddSaveable(newDataPerEnumSetting);

        return newDataPerEnumSetting;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_EnumSetting.name + jsonSplitter[0];
        for (int i = 0; i < m_Data.Length; i++)
        {
            jsonString += m_Data[i].name + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_EnumSetting = EnumSetting.Load(splitJsonString[1]);
        m_Data = new TFirstLevelData[splitJsonString.Length - 2];
        for (int i = 0; i < m_Data.Length; i++)
        {
            m_Data[i] = DataPerEnumSetting<TFirstLevelData, TSecondLevelData>.Load(splitJsonString[i + 2]);
        }
    }
}

// Example: AvailabilityPerStockTypePerShopSize<AvailabilityPerStockTypePerShopSize, IntRangePerPowerLevelPerStockType, IntRangePerPowerLevel, IntRange>
public abstract class DataPerEnumSetting<TDataPerEnumSetting, TFirstLevelData, TSecondLevelData, TThirdLevelData> : Saveable<TDataPerEnumSetting>
    where TDataPerEnumSetting : DataPerEnumSetting<TDataPerEnumSetting, TFirstLevelData, TSecondLevelData, TThirdLevelData>
    where TFirstLevelData : DataPerEnumSetting<TFirstLevelData, TSecondLevelData, TThirdLevelData>
    where TSecondLevelData : DataPerEnumSetting<TSecondLevelData, TThirdLevelData>
    where TThirdLevelData : DataPerEnumSetting<TThirdLevelData>
{
    [SerializeField]
    protected EnumSetting m_EnumSetting;
    [SerializeField]
    protected TFirstLevelData[] m_Data;

    public TFirstLevelData this[string enumSetting]
    {
        get { return m_Data[m_EnumSetting[enumSetting]]; }
    }

    public static TDataPerEnumSetting Create (EnumSetting enumForFirstLevelData, EnumSetting enumForSecondLevelData, EnumSetting enumForThirdLevelData)
    {
        TDataPerEnumSetting newDataPerEnumSetting = CreateInstance<TDataPerEnumSetting>();
        newDataPerEnumSetting.name = "New" + typeof(TDataPerEnumSetting).Name;
        newDataPerEnumSetting.m_EnumSetting = enumForFirstLevelData;
        newDataPerEnumSetting.m_Data = new TFirstLevelData[enumForFirstLevelData.Length];
        for(int i = 0; i < enumForFirstLevelData.Length; i++)
        {
            newDataPerEnumSetting.m_Data[i] = DataPerEnumSetting<TFirstLevelData, TSecondLevelData, TThirdLevelData>.Create(enumForSecondLevelData, enumForThirdLevelData);
        }

        SaveableHolder.AddSaveable(newDataPerEnumSetting);

        return newDataPerEnumSetting;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_EnumSetting.name + jsonSplitter[0];
        for (int i = 0; i < m_Data.Length; i++)
        {
            jsonString += m_Data[i].name + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_EnumSetting = EnumSetting.Load(splitJsonString[1]);
        m_Data = new TFirstLevelData[splitJsonString.Length - 2];
        for (int i = 0; i < m_Data.Length; i++)
        {
            m_Data[i] = DataPerEnumSetting<TFirstLevelData, TSecondLevelData, TThirdLevelData>.Load(splitJsonString[i + 2]);
        }
    }
}
