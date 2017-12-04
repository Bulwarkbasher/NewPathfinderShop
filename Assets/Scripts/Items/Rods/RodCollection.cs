using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RodCollection : ItemCollection<RodCollectionFilter, RodCollection, Rod>
{
    public Rod PickRod(FloatRange budgetRange)
    {
        if (items.Length == 0)
            return null;

        List<Rod> affordableRodList = new List<Rod>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost <= budgetRange.max && items[i].cost >= budgetRange.min)
                affordableRodList.Add(items[i]);
        }

        Rod[] affordableRods = affordableRodList.ToArray();

        Rod chosenRod = Rod.PickItem(affordableRods);

        return chosenRod;
    }
}