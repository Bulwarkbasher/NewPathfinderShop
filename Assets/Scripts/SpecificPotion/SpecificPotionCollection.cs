using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificPotionCollection : SpecificItemCollection<SpecificPotion>
{
    public static SpecificPotionCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificPotionCollection>();
    }

    protected override SpecificPotion GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificPotion.CreateRandom();
    }

    public static string GetJsonString(SpecificPotionCollection specificPotionCollection)
    {
        return "";
    }

    public static SpecificPotionCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificPotionCollection>();
    }
}
