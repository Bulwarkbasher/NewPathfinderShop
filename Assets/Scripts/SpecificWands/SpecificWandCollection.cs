using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWandCollection : SpecificItemCollection<SpecificWand>
{
    public static SpecificWandCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificWandCollection>();
    }

    protected override SpecificWand GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificWand.CreateRandom();
    }

    public static string GetJsonString(SpecificWandCollection specificWandCollection)
    {
        return "";
    }

    public static SpecificWandCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificWandCollection>();
    }
}
