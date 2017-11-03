using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWand : SpecificItem<SpecificWand>
{
    // TODO: complete me
    public Spell spell;

    public static SpecificWand CreateRandom(SpecificItem.PowerLevel powerLevel, int budget, SpellCollection availableSpells)
    {
        SpecificWand newSpecificWand = CreateInstance<SpecificWand> ();
        newSpecificWand.powerLevel = powerLevel;
        int spellLevel = 0;    // TODO: decide these based on powerlevel.  Have settings for rarity of creator classes.
        Spell.Creator creator = Spell.Creator.Alc;
        int creatorLevel = 0;
        int chargeCount = Random.Range (1, 51);
        budget /= chargeCount;
        newSpecificWand.spell = availableSpells.PickSpell (budget, creator, spellLevel, creatorLevel, chargeCount);
        return newSpecificWand;
    }

    protected override void SetupFromSplitJsonString (string[] splitJsonString)
    {
        throw new System.NotImplementedException ();
    }

    protected override string ConvertToJsonString (string[] jsonSplitter)
    {
        throw new System.NotImplementedException ();
    }
}
