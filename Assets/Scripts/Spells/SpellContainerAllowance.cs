using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContainerAllowances : Jsonable<SpellContainerAllowances>
{
    public Spell.Allowance potionAllowance = Spell.Allowance.Default;
    public Spell.Allowance scrollAllowance = Spell.Allowance.Default;
    public Spell.Allowance wandAllowance = Spell.Allowance.Default;

    public Spell.Allowance this[Spell.Container container]
    {
        get
        {
            switch (container)
            {
                case Spell.Container.Potion:
                    return potionAllowance;
                case Spell.Container.Scroll:
                    return scrollAllowance;
                case Spell.Container.Wand:
                    return wandAllowance;
                default:
                    throw new ArgumentOutOfRangeException("container", container, null);
            }
        }
    }

    public static SpellContainerAllowances Create(Spell.Allowance potionAllowance, Spell.Allowance scrollAllowance, Spell.Allowance wandAllowance)
    {
        SpellContainerAllowances newSpellContainerAllowances = CreateInstance<SpellContainerAllowances>();
        newSpellContainerAllowances.potionAllowance = potionAllowance;
        newSpellContainerAllowances.scrollAllowance = scrollAllowance;
        newSpellContainerAllowances.wandAllowance = wandAllowance;
        return newSpellContainerAllowances;
    }

    public static SpellContainerAllowances CreateBlank ()
    {
        return Create(Spell.Allowance.Default, Spell.Allowance.Default, Spell.Allowance.Default);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += Wrapper<int>.GetJsonString((int)potionAllowance) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)scrollAllowance) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)wandAllowance) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        potionAllowance = (Spell.Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[0]);
        scrollAllowance = (Spell.Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
        wandAllowance = (Spell.Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
    }
}
