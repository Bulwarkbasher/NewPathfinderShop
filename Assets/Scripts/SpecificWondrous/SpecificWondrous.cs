using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWondrous : SpecificItem<SpecificWondrous>
{
    // TODO: complete me

    public static SpecificWondrous CreateRandom()
    {
        return CreateInstance<SpecificWondrous>();
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
