using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaign : ScriptableObject
{
    public Settlement[] settlements = new Settlement[0];

    public bool UsesAutomaticBonusProgressionRules
    {
        get { return m_UsesAutomaticBonusProgressionRules; }
    }


    private bool m_UsesAutomaticBonusProgressionRules;
}
