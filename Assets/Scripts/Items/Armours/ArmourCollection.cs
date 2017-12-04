using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArmourCollection : ItemCollection<ArmourCollectionFilter, ArmourCollection, Armour>
{
    public Armour PickArmour(ref float budget)
    {
        if (items.Length == 0)
            return null;

        List<Armour> affordableArmourList = new List<Armour>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost < budget)
                affordableArmourList.Add(items[i]);
        }

        Armour[] affordableArmours = affordableArmourList.ToArray();

        Armour chosenArmour = Armour.PickItem(affordableArmours);
        budget -= chosenArmour.cost;

        return chosenArmour;
    }
}