using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificArmourCollection : SpecificItemCollection<SpecificArmour>
{
    public static SpecificArmourCollection Create(Availability stockAvailability, ArmourCollection availableArmours, ArmourQualityCollection availableArmourQualities)
    {
        return CreateInstance<SpecificArmourCollection>();
    }

    protected override SpecificArmour GetRandomSpecificItem (SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificArmour.CreateRandom ();
    }

    public static string GetJsonString(SpecificArmourCollection specificArmourCollection)
    {
        return "";
    }

    public static SpecificArmourCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificArmourCollection>();
    }
}
