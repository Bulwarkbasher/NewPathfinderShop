using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWondrousCollection : SpecificItemCollection<SpecificWondrous>
{
    public static SpecificWondrousCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificWondrousCollection>();
    }

    protected override SpecificWondrous GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificWondrous.CreateRandom();
    }

    public static string GetJsonString (SpecificWondrousCollection specificWondrousCollection)
    {
        return "";
    }

    public static SpecificWondrousCollection CreateFromJsonString (string jsonString)
    {
        return CreateInstance<SpecificWondrousCollection> ();
    }
}
