using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JsonableEnumWithJsonables<TSaveable, TEnumedJsonables> : Jsonable<TSaveable>
    where TSaveable : JsonableEnumWithJsonables<TSaveable, TEnumedJsonables>
    where TEnumedJsonables : Jsonable<TEnumedJsonables>
{
    public EnumSetting enumSetting;
    public TEnumedJsonables[] enumedJsonables;

    public TEnumedJsonables this[string selectedEnum]
    {
        get { return enumedJsonables[enumSetting[selectedEnum]]; }
    }

    public TEnumedJsonables this[int index]
    {
        get { return enumedJsonables[index]; }
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += enumSetting.name + jsonSplitter[0];
        for (int i = 0; i < enumedJsonables.Length; i++)
        {
            jsonString += Jsonable<TEnumedJsonables>.GetJsonString(enumedJsonables[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        enumSetting = EnumSetting.Load(splitJsonString[1]);

        enumedJsonables = new TEnumedJsonables[splitJsonString.Length - 2];
        for (int i = 0; i < enumedJsonables.Length; i++)
        {
            enumedJsonables[i] = Jsonable<TEnumedJsonables>.CreateFromJsonString(splitJsonString[i + 2]);
        }
    }
}