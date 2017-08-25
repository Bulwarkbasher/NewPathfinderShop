using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRodCollection : SpecificItemCollection<SpecificRod>
{
    public static SpecificRodCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificRodCollection>();
    }

    protected override SpecificRod GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificRod.CreateRandom();
    }

    public static string GetJsonString(SpecificRodCollection specificRodCollection)
    {
        return "";
    }

    public static SpecificRodCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificRodCollection>();
    }
}
