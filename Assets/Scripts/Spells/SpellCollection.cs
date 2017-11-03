using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
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

        SaveableHolder.AddSaveable(newSpellCollection);

        return newSpellCollection;
    }

    public Spell PickSpell (int budget, Spell.Creator creator, int spellLevel, int creatorLevel, int chargeCount)
    {
        if (spells.Length == 0)
            return null;
        
        List<Spell> availableSpells = new List<Spell> ();

        for (int i = 0; i < spells.Length; i++)
        {
            float cost = spells[i].GetCostFromLevel (Spell.Container.Wand, creator, creatorLevel, chargeCount * 0.02f);
            cost += spells[i].materialCost * chargeCount;
            
            if (cost < budget && spells[i].creatorLevels[creator] == spellLevel)
            {
                availableSpells.Add (spells[i]);
            }
        }

        return Spell.PickSpell (availableSpells, Spell.Container.Wand);
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

    protected override string ConvertToJsonString(string[] jsonSplitter)
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
