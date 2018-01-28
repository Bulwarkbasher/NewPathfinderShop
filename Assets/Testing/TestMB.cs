using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Reflection;

public class TestMB : MonoBehaviour
{
    public float someFloat = 101f;
    public int weaponCount = 10;
    public int budget = 30000;
    public int budgetRange = 20000;
    public WeaponCollection allWeapons;
    public WeaponQualityCollection allWeaponQualities;


    private void Start ()
    {
        Type[] jsonableTypes = typeof(TestMB).Assembly.GetTypes().Where(t => SelectTypes(t)).ToArray();

        string debug = "";

        for(int i = 0; i < jsonableTypes.Length; i++)
        {
            debug += jsonableTypes[i].Name + "\n";
        }

        Debug.Log(debug);

        var AllTypesOfIRepository = from x in Assembly.GetAssembly(typeof(TestMB)).GetTypes()
                                    let y = x.BaseType
                                    where !x.IsAbstract && !x.IsInterface &&
                                    y != null && y.IsGenericType &&
                                    y.GetGenericTypeDefinition() == typeof(Jsonable<>)
                                    select x;
    }

    bool SelectTypes (Type t)
    {
        bool isTypeSubclassOfJsonable = t.IsSubclassOf(typeof(Jsonable<>));
        bool isTypeSubclassOfSaveable = t.IsSubclassOf(typeof(Saveable<>));
        Debug.Log(t.Name + " " + isTypeSubclassOfJsonable + " " + isTypeSubclassOfSaveable);
        return isTypeSubclassOfJsonable && !isTypeSubclassOfSaveable;
    }
}