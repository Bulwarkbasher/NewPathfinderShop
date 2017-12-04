using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StaffCollection : ItemCollection<StaffCollectionFilter, StaffCollection, Staff>
{
    public Staff PickStaff(FloatRange budgetRange)
    {
        if (items.Length == 0)
            return null;

        List<Staff> affordableStaffList = new List<Staff>();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].cost <= budgetRange.max && items[i].cost >= budgetRange.min)
                affordableStaffList.Add(items[i]);
        }

        Staff[] affordableStaffs = affordableStaffList.ToArray();

        Staff chosenStaff = Staff.PickItem(affordableStaffs);

        return chosenStaff;
    }
}