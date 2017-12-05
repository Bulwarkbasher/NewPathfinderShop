using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JsonableWithEnumValues<TSaveable, TEnumedValues> : Jsonable<TSaveable>
    where TSaveable : JsonableWithEnumValues<TSaveable, TEnumedValues>
    where TEnumedValues : struct, IComparable, IConvertible, IComparable<TEnumedValues>, IEquatable<TEnumedValues>
{
    public EnumSetting enumSetting;
    public TEnumedValues[] enumedValues;

    public TEnumedValues this[string selectedEnum]
    {
        get { return enumedValues[enumSetting[selectedEnum]]; }
    }

    public TEnumedValues this[int index]
    {
        get { return enumedValues[index]; }
    }

    public int Length
    {
        get { return enumSetting.Length; }
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += enumSetting.name + jsonSplitter[0];
        for (int i = 0; i < enumedValues.Length; i++)
        {
            jsonString += Wrapper<TEnumedValues>.GetJsonString(enumedValues[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        enumSetting = EnumSetting.Load(splitJsonString[1]);

        enumedValues = new TEnumedValues[splitJsonString.Length - 2];
        for (int i = 0; i < enumedValues.Length; i++)
        {
            enumedValues[i] = Wrapper<TEnumedValues>.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}