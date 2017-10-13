using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificArmour : SpecificItem<SpecificArmour>
{
    // TODO: complete me

    public static SpecificArmour CreateRandom ()
    {
        return CreateInstance<SpecificArmour> ();
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
