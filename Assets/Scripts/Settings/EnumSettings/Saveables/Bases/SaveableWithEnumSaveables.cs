using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveableWithEnumSaveables<TSaveable, TEnumedSaveables> : Saveable<TSaveable>
    where TSaveable : SaveableWithEnumSaveables<TSaveable, TEnumedSaveables>
    where TEnumedSaveables : Saveable<TEnumedSaveables>
{
    [SerializeField]
    protected EnumSetting m_EnumSetting;
    [SerializeField]
    protected TEnumedSaveables[] m_EnumedSaveables;

    public TEnumedSaveables this [string selectedEnum]
    {
        get { return m_EnumedSaveables[m_EnumSetting[selectedEnum]]; }
    }

    public TEnumedSaveables this [int index]
    {
        get { return m_EnumedSaveables[index]; }
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_EnumSetting.name + jsonSplitter[0];
        for (int i = 0; i < m_EnumedSaveables.Length; i++)
        {
            jsonString += m_EnumedSaveables[i].name + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_EnumSetting = EnumSetting.Load(splitJsonString[1]);

        m_EnumedSaveables = new TEnumedSaveables[splitJsonString.Length - 2];
        for (int i = 0; i < m_EnumedSaveables.Length; i++)
        {
            m_EnumedSaveables[i] = Saveable<TEnumedSaveables>.Load(splitJsonString[i + 2]);
        }
    }
}
