using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WondrousCollection : ItemCollection<WondrousCollectionFilter, WondrousCollection, Wondrous>
{
    public Wondrous PickWondrous(FloatRange budgetRange)
    {
        if (items.Length == 0)
            return null;

        List<Wondrous> affordableWondrousList = new List<Wondrous>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost <= budgetRange.max && items[i].cost >= budgetRange.min)
                affordableWondrousList.Add(items[i]);
        }

        Wondrous[] affordableWondrouss = affordableWondrousList.ToArray();

        Wondrous chosenWondrous = Wondrous.PickItem(affordableWondrouss);

        return chosenWondrous;
    }
}
