using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRingCollection : SpecificItemCollection<SpecificRing>
{
    public static SpecificRingCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificRingCollection>();
    }

    protected override SpecificRing GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificRing.CreateRandom();
    }

    public static string GetJsonString(SpecificRingCollection specificRingCollection)
    {
        return "";
    }

    public static SpecificRingCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificRingCollection>();
    }
}