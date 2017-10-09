using System;
using UnityEngine;
// TODO: to draw curve at runtime use int values for rarity and draw a line renderer between each value
[CreateAssetMenu]
public class RarityWeighting : Saveable<RarityWeighting>
{
    [SerializeField]
    protected AnimationCurve m_WeightingCurve= new AnimationCurve
        (
            new Keyframe(0f, 1f, -1.5708f, -1.5708f),
            new Keyframe(1f, 0f, -0.6f, -0.6f)
        );

    public static RarityWeighting Create (string name, AnimationCurve weightingCurve)
    {
        RarityWeighting newRarityWeighting = CreateInstance<RarityWeighting> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newRarityWeighting.name = name;
        newRarityWeighting.m_WeightingCurve = weightingCurve;

        Save (newRarityWeighting);

        return newRarityWeighting;
    }
    
    public float RarityToWeight(Item.Rarity rarity)
    {
        float time = ((float)((int)rarity)) / Enum.GetNames(typeof(Item.Rarity)).Length;
        return m_WeightingCurve.Evaluate(time);
    }
    
    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        Keyframe[] keyframes = new Keyframe[splitJsonString.Length / 4];

        for (int i = 0; i < keyframes.Length; i++)
        {
            keyframes[i] = new Keyframe();
            keyframes[i].value = Wrapper<float>.CreateFromJsonString (splitJsonString[i * 4]);
            keyframes[i].time = Wrapper<float>.CreateFromJsonString (splitJsonString[i * 4 + 1]);
            keyframes[i].inTangent = Wrapper<float>.CreateFromJsonString(splitJsonString[i * 4 + 2]);
            keyframes[i].outTangent = Wrapper<float>.CreateFromJsonString(splitJsonString[i * 4 + 3]);
        }

        m_WeightingCurve = new AnimationCurve(keyframes);
    }

    protected override string GetJsonString (string[] jsonSplitter)
    {
        string jsonString = "";

        for (int i = 0; i < m_WeightingCurve.length; i++)
        {
            jsonString += Wrapper<float>.GetJsonString(m_WeightingCurve[i].value) + jsonSplitter[0];
            jsonString += Wrapper<float>.GetJsonString(m_WeightingCurve[i].time) + jsonSplitter[0];
            jsonString += Wrapper<float>.GetJsonString(m_WeightingCurve[i].inTangent) + jsonSplitter[0];
            jsonString += Wrapper<float>.GetJsonString(m_WeightingCurve[i].outTangent) + jsonSplitter[0];
        }

        return jsonString;
    }
}
