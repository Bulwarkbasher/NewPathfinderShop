using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaff : SpecificItem<SpecificStaff>
{
    // TODO: complete me

    public static SpecificStaff CreateRandom()
    {
        return CreateInstance<SpecificStaff>();
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