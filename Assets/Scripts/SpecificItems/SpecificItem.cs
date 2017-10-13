using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecificItem<TChild> : Jsonable<TChild>
    where TChild : Jsonable<TChild>
{
    public enum PowerLevel
    {
        Minor,
        Medium,
        Major,
    }
    
    public PowerLevel powerLevel;
    public int cost;
}
