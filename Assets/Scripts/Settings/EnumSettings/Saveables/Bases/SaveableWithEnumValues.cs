using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveableWithEnumValues<TSaveable, TEnumedValues> : Saveable<TSaveable>
    where TSaveable : SaveableWithEnumValues<TSaveable, TEnumedValues>
    where TEnumedValues : struct, IComparable, IConvertible, IComparable<TEnumedValues>, IEquatable<TEnumedValues>
{
    [SerializeField]
    protected EnumSetting m_EnumSetting;
    [SerializeField]
    protected TEnumedValues[] m_EnumedValues;

    public TEnumedValues this[string selectedEnum]
    {
        get { return m_EnumedValues[m_EnumSetting[selectedEnum]]; }
    }

    public TEnumedValues this[int index]
    {
        get { return m_EnumedValues[index]; }
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_EnumSetting.name + jsonSplitter[0];
        for (int i = 0; i < m_EnumedValues.Length; i++)
        {
            jsonString += Wrapper<TEnumedValues>.GetJsonString(m_EnumedValues[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_EnumSetting = EnumSetting.Load(splitJsonString[1]);

        m_EnumedValues = new TEnumedValues[splitJsonString.Length - 2];
        for (int i = 0; i < m_EnumedValues.Length; i++)
        {
            m_EnumedValues[i] = Wrapper<TEnumedValues>.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}