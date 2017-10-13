using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWand : SpecificItem<SpecificWand>
{
    // TODO: complete me

    public static SpecificWand CreateRandom()
    {
        return CreateInstance<SpecificWand>();
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
