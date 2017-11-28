using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecificItem<TChild> : Jsonable<TChild>
    where TChild : Jsonable<TChild>
{
    public SpecificItem.PowerLevel powerLevel;
    public float cost;
    public string notes;
}


public abstract class SpecificItem
{
    public enum PowerLevel
    {
        Minor,
        Medium,
        Major,
    }
}