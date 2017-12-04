using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RingCollection : ItemCollection<RingCollectionFilter, RingCollection, Ring>
{
    public Ring PickRing(FloatRange budgetRange)
    {
        if (items.Length == 0)
            return null;

        List<Ring> affordableRingList = new List<Ring>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost <= budgetRange.max && items[i].cost >= budgetRange.min)
                affordableRingList.Add(items[i]);
        }

        Ring[] affordableRings = affordableRingList.ToArray();

        Ring chosenRing = Ring.PickItem(affordableRings);

        return chosenRing;
    }
}
