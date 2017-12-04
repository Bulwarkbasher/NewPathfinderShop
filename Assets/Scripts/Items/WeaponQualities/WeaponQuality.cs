using UnityEngine;
using System.Collections;

public class WeaponQuality : Quality<WeaponQuality>
{
    protected override int BonusToCostInternal(int bonus)
    {
        return bonus * bonus * 2000;
    }
}
