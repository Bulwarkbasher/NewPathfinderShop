using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PerContainerPerCreatorRarity : Saveable<PerContainerPerCreatorRarity>
{
    [SerializeField]
    protected PerCreatorRarity m_PotionPerCreatorRarity;
    [SerializeField]
    protected PerCreatorRarity m_ScrollPerCreatorRarity;
    [SerializeField]
    protected PerCreatorRarity m_WandPerCreatorRarity;

    public PerCreatorRarity this [Spell.Container container]
    {
        get
        {
            switch (container)
            {
                case Spell.Container.Potion:
                    return m_PotionPerCreatorRarity;
                case Spell.Container.Scroll:
                    return m_ScrollPerCreatorRarity;
                case Spell.Container.Wand:
                    return m_WandPerCreatorRarity;
                default:
                    throw new ArgumentOutOfRangeException("container", container, "Unknown Spell Container type.");
            }
        }
    }

    public static PerContainerPerCreatorRarity Create (string name, PerCreatorRarity potion, PerCreatorRarity scroll, PerCreatorRarity wand)
    {
        PerContainerPerCreatorRarity newPerContainerPerCreatorRarity = CreateInstance<PerContainerPerCreatorRarity>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerContainerPerCreatorRarity.name = name;
        newPerContainerPerCreatorRarity.m_PotionPerCreatorRarity = potion;
        newPerContainerPerCreatorRarity.m_ScrollPerCreatorRarity = scroll;
        newPerContainerPerCreatorRarity.m_WandPerCreatorRarity = wand;

        SaveableHolder.AddSaveable(newPerContainerPerCreatorRarity);

        return newPerContainerPerCreatorRarity;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += m_PotionPerCreatorRarity.name + jsonSplitter[0];
        jsonString += m_ScrollPerCreatorRarity.name + jsonSplitter[0];
        jsonString += m_WandPerCreatorRarity.name + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        m_PotionPerCreatorRarity = PerCreatorRarity.Load(splitJsonString[1]);
        m_ScrollPerCreatorRarity = PerCreatorRarity.Load(splitJsonString[2]);
        m_WandPerCreatorRarity = PerCreatorRarity.Load(splitJsonString[3]);
    }
}
