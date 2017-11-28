using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWand : SpecificItem<SpecificWand>
{
    public Spell spell;
    public string creator;
    public int casterLevel;
    public int charges;

    public static SpecificWand CreateRandom(SpecificItem.PowerLevel powerLevel, SpellCollection availableSpells, FloatRange budgetRange)
    {
        SpecificWand newSpecificWand = CreateInstance<SpecificWand> ();
        newSpecificWand.powerLevel = powerLevel;
        newSpecificWand.charges = Random.Range(1, 11) + Random.Range(1, 11) + Random.Range(1, 11) + Random.Range(1, 11) + Random.Range(1, 11);
        availableSpells.PickAndApplySpell(budgetRange, newSpecificWand);
        return newSpecificWand;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)powerLevel) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(cost) + jsonSplitter[0];
        jsonString += Spell.GetJsonString(spell) + jsonSplitter[0];
        jsonString += creator + jsonString[0];
        jsonString += Wrapper<int>.GetJsonString(casterLevel) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(charges) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString (string[] splitJsonString)
    {
        name = splitJsonString[0];
        powerLevel = (SpecificItem.PowerLevel)Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
        cost = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
        spell = Spell.CreateFromJsonString(splitJsonString[3]);
        creator = splitJsonString[4];
        casterLevel = Wrapper<int>.CreateFromJsonString(splitJsonString[5]);
        charges = Wrapper<int>.CreateFromJsonString(splitJsonString[6]);
    }
}
