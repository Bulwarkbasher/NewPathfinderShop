using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class IntRangePerPowerLevel : Saveable<IntRangePerPowerLevel>
{
    [SerializeField]
    protected IntRange m_MinorRange;
    [SerializeField]
    protected IntRange m_MediumRange;
    [SerializeField]
    protected IntRange m_MajorRange;

    public IntRange this[SpecificItem.PowerLevel powerLevel]
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

    public static IntRangePerPowerLevel Create(string name, IntRange minorRange, IntRange mediumRange, IntRange majorRange)
    {
        IntRangePerPowerLevel newPerPowerLevelRange = CreateInstance<IntRangePerPowerLevel>();

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
        jsonString += IntRange.GetJsonString(m_MinorRange) + jsonSplitter[0];
        jsonString += IntRange.GetJsonString(m_MediumRange) + jsonSplitter[0];
        jsonString += IntRange.GetJsonString(m_MajorRange) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_MinorRange = IntRange.CreateFromJsonString(splitJsonString[1]);
        m_MediumRange = IntRange.CreateFromJsonString(splitJsonString[2]);
        m_MajorRange = IntRange.CreateFromJsonString(splitJsonString[3]);
    }
}