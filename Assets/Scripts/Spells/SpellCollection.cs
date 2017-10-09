using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCollection : Saveable<SpellCollection>
{
    public Spell[] spells = new Spell[0];

    public static SpellCollection Create (string name)
    {
        SpellCollection newSpellCollection = CreateInstance<SpellCollection> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Spell Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Spell Collection name invalid, name cannot start with Default");

        newSpellCollection.name = name;
        newSpellCollection.spells = new Spell[0];

        Save (newSpellCollection);

        return newSpellCollection;
    }

    public void AddSpell(Spell newSpell)
    {
        Spell[] newSpells = new Spell[spells.Length + 1];
        for (int i = 0; i < spells.Length; i++)
        {
            newSpells[i] = spells[i];
        }
        newSpells[spells.Length] = newSpell;
    }

    public void RemoveSpellAt(int index)
    {
        Spell[] newSpells = new Spell[spells.Length - 1];
        for (int i = 0; i < newSpells.Length; i++)
        {
            int oldWeaponIndex = i < index ? i : i + 1;
            newSpells[i] = spells[oldWeaponIndex];
        }
    }

    /*public Spell PickWeapon(ref float budget, Spell.Container container, Spell.Creator creator, int creatorLevel)
    {
        if (spells.Length == 0)
            return null;

        List<Spell> affordableSpellList = new List<Spell>();

        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i].GetCost (container, creator, creatorLevel) < budget)
                affordableSpellList.Add(spells[i]);
        }

        Spell chosenWeapon = Spell.PickSpell (affordableSpellList);
        budget -= chosenWeapon.GetCost (container, creator, creatorLevel);

        return chosenWeapon;
    }*/

    protected override string GetJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];

        for (int i = 0; i < spells.Length; i++)
        {
            jsonString += Spell.GetJsonString(spells[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];

        spells = new Spell[splitJsonString.Length - 1];
        for (int i = 0; i < spells.Length; i++)
        {
            spells[i] = Spell.CreateFromJsonString(splitJsonString[i + 1]);
        }
    }
}
