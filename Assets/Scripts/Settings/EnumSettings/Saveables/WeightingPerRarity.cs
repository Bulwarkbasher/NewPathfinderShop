using System;
using UnityEngine;
// TODO FRONTEND: to draw curve at runtime use int values for rarity and draw a line renderer between each value
[CreateAssetMenu]
public class WeightingPerRarity : SaveableWithEnumValues<WeightingPerRarity, float>
{
    public static WeightingPerRarity Create (string name, AnimationCurve weightingCurve)
    {
        WeightingPerRarity newRarityWeighting = CreateInstance<WeightingPerRarity> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newRarityWeighting.name = name;

        SaveableHolder.AddSaveable(newRarityWeighting);

        return newRarityWeighting;
    }
}
