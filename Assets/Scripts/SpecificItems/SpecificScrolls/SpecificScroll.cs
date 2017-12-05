using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificScroll : SpecificItem<SpecificScroll>
{
    public Spell spell;
    public string creator;
    public int casterLevel;

    public static SpecificScroll CreateRandom(EnumValue powerLevel, SpellCollection availableSpells, FloatRange budgetRange)
    {
        SpecificScroll newSpecificScroll = CreateInstance<SpecificScroll>();
        newSpecificScroll.powerLevel = powerLevel;
        availableSpells.PickAndApplySpell(budgetRange, newSpecificScroll);
        return newSpecificScroll;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += EnumValue.GetJsonString(powerLevel) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += GetSafeJsonFromString(notes) + jsonSplitter[0];
        jsonString += Spell.GetJsonString(spell) + jsonSplitter[0];
        jsonString += creator + jsonString[0];
        jsonString += Wrapper<int>.GetJsonString(casterLevel) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        powerLevel = EnumValue.CreateFromJsonString(splitJsonString[1]);
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        notes = CreateStringFromSafeJson(splitJsonString[3]);
        spell = Spell.CreateFromJsonString(splitJsonString[4]);
        creator = splitJsonString[5];
        casterLevel = Wrapper<int>.CreateFromJsonString(splitJsonString[6]);
    }
}
