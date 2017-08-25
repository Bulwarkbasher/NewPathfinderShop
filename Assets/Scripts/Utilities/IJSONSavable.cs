using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJSONSavable<TSaveable>
    where TSaveable : IJSONSavable<TSaveable>
{
    string GetJsonString(TSaveable saveable);
}
