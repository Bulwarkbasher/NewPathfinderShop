using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRod : SpecificItem<SpecificRod>
{
    // TODO: complete me

    public static SpecificRod CreateRandom()
    {
        return CreateInstance<SpecificRod>();
    }

    protected override void SetupFromSplitJsonString (string[] splitJsonString)
    {
        throw new System.NotImplementedException ();
    }

    protected override string ConvertToJsonString (string[] jsonSplitter)
    {
        throw new System.NotImplementedException ();
    }
}
