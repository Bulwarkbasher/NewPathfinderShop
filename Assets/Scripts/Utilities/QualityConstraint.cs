using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityConstraint : Jsonable<QualityConstraint>
{
    public bool allowed;

    protected override string ConvertToJsonString (string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<bool>.GetJsonString(allowed) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        allowed = Wrapper<bool>.CreateFromJsonString(splitJsonString[1]);
    }
}
