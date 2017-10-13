using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScroll : SpecificItem<SpecificScroll>
{
    // TODO: complete me

    public static SpecificScroll CreateRandom()
    {
        return CreateInstance<SpecificScroll>();
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
