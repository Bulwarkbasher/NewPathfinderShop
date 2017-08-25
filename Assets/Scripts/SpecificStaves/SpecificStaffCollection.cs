using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificStaffCollection : SpecificItemCollection<SpecificStaff>
{
    public static SpecificStaffCollection Create(Availability stockAvailability)
    {
        return CreateInstance<SpecificStaffCollection>();
    }

    protected override SpecificStaff GetRandomSpecificItem(SpecificItem.PowerLevel powerLevel, int budget)
    {
        return SpecificStaff.CreateRandom();
    }

    public static string GetJsonString(SpecificStaffCollection specificStaffCollection)
    {
        return "";
    }

    public static SpecificStaffCollection CreateFromJsonString(string jsonString)
    {
        return CreateInstance<SpecificStaffCollection>();
    }
}
