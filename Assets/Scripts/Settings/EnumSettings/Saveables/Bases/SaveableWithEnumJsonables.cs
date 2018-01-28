using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveableWithEnumJsonables<TSaveable, TEnumedJsonables> : Saveable<TSaveable>
    where TSaveable : SaveableWithEnumJsonables<TSaveable, TEnumedJsonables>
    where TEnumedJsonables : Jsonable<TEnumedJsonables>
{
    [SerializeField]
    protected EnumSetting m_EnumSetting;
    [SerializeField]
    protected TEnumedJsonables[] m_EnumedJsonables;

    public TEnumedJsonables this[string selectedEnum]
    {
        get { return m_EnumedJsonables[m_EnumSetting[selectedEnum]]; }
    }

    public TEnumedJsonables this[int index]
    {
        get { return m_EnumedJsonables[index]; }
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_EnumSetting.name + jsonSplitter[0];
        for (int i = 0; i < m_EnumedJsonables.Length; i++)
        {
            jsonString += Jsonable<TEnumedJsonables>.GetJsonString(m_EnumedJsonables[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_EnumSetting = EnumSetting.Load(splitJsonString[1]);

        m_EnumedJsonables = new TEnumedJsonables[splitJsonString.Length - 2];
        for (int i = 0; i < m_EnumedJsonables.Length; i++)
        {
            m_EnumedJsonables[i] = Jsonable<TEnumedJsonables>.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}