using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourQuality : Quality<ArmourQuality>
{
    protected override int BonusToCostInternal(int bonus)
    {
        return bonus * bonus * 1000;
    }
}
