using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificItem : ScriptableObject
{
    public enum PowerLevel
    {
        Minor,
        Medium,
        Major,
    }


    public PowerLevel powerLevel;
}
