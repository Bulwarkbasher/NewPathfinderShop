using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PerPowerLevelRange : Saveable<PerPowerLevelRange>
{
    [SerializeField]
    protected FloatRange m_MinorRange;
    [SerializeField]
    protected FloatRange m_MediumRange;
    [SerializeField]
    protected FloatRange m_MajorRange;

    public FloatRange this [SpecificItem.PowerLevel powerLevel]
    {
        get
        {
            switch (powerLevel)
            {
                case SpecificItem.PowerLevel.Minor:
                    return m_MinorRange;
                case SpecificItem.PowerLevel.Medium:
                    return m_MediumRange;
                case SpecificItem.PowerLevel.Major:
                    return m_MajorRange;
                default:
                    throw new ArgumentOutOfRangeException("powerLevel", powerLevel, "Unknown Power Level.");
            }
        }
    }

    public static PerPowerLevelRange Create (string name, FloatRange minorRange, FloatRange mediumRange, FloatRange majorRange)
    {
        PerPowerLevelRange newPerPowerLevelRange = CreateInstance<PerPowerLevelRange>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerPowerLevelRange.name = name;
        newPerPowerLevelRange.m_MinorRange = minorRange;
        newPerPowerLevelRange.m_MediumRange = mediumRange;
        newPerPowerLevelRange.m_MajorRange = majorRange;

        SaveableHolder.AddSaveable(newPerPowerLevelRange);

        return newPerPowerLevelRange;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += FloatRange.GetJsonString(m_MinorRange) + jsonSplitter[0];
        jsonString += FloatRange.GetJsonString(m_MediumRange) + jsonSplitter[0];
        jsonString += FloatRange.GetJsonString(m_MajorRange) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_MinorRange = FloatRange.CreateFromJsonString(splitJsonString[1]);
        m_MediumRange = FloatRange.CreateFromJsonString(splitJsonString[2]);
        m_MajorRange = FloatRange.CreateFromJsonString(splitJsonString[3]);
    }
}
