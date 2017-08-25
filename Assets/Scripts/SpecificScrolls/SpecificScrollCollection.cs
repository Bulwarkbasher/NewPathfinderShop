using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScrollCollection : SpecificItemCollection<SpecificScroll>
{
    public static SpecificScrollCollection Create (Availability stockAvailability)
    {
        return CreateInstance<SpecificScrollCollection> ();
    }

    protected override SpecificScroll GetRandomSpecificItem (SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificScroll.CreateRandom ();
    }

    public static string GetJsonString(SpecificScrollCollection specificScrollCollection)
    {
        return "";
    }

    public static SpecificScrollCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificScrollCollection>();
    }
}
