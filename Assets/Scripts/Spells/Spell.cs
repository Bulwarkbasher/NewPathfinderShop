using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spell : ScriptableObject
{
    public enum Book
    {
        CRB,
        APG,
        UM,
        UC,
    }


    public enum Allowance
    {
        Default,
        CanBe,
        CannotBe,
    }


    public enum Container
    {
        Potion,
        Scroll,
        Wand,
    }


    public enum Creator
    {
        Alc,
        Brd,
        ClrOcl,
        Drd,
        Inq,
        Mag,
        Pal,
        Rgr,
        SorWiz,
        Sum,
        Wit,
    }


    public Dictionary<Container, Allowance> allowances = new Dictionary<Container, Allowance>
    {
        {Container.Potion, Allowance.Default},
        {Container.Scroll, Allowance.Default},
        {Container.Wand, Allowance.Default},
    };
    public Dictionary<Container, Item.Rarity> rarities = new Dictionary<Container, Item.Rarity>
    {
        {Container.Potion, Item.Rarity.Common},
        {Container.Scroll, Item.Rarity.Common},
        {Container.Wand, Item.Rarity.Common},
    };
    public Dictionary<Creator, int> creatorLevels = new Dictionary<Creator, int>
    {
        {Creator.Alc, -1},
        {Creator.Brd, -1},
        {Creator.ClrOcl, -1},
        {Creator.Drd, -1},
        {Creator.Inq, -1},
        {Creator.Mag, -1},
        {Creator.Pal, -1},
        {Creator.Rgr, -1},
        {Creator.SorWiz, -1},
        {Creator.Sum, -1},
        {Creator.Wit, -1},
    };
    public Book book;
    public int page;
    public int materialCost;

    static readonly string[] k_JsonSplitter = { "###SpellSplitter###" };

    public static Spell Create (string name, Dictionary<Container, Allowance> allowances, Dictionary<Container, Item.Rarity> rarities,
        Dictionary<Creator, int> creatorLevels, Book book, int page, int materialCost)
    {
        Spell newSpell = CreateInstance<Spell> ();
        newSpell.name = name;
        newSpell.allowances = allowances;
        newSpell.rarities = rarities;
        newSpell.book = book;
        newSpell.page = page;
        newSpell.creatorLevels = creatorLevels;
        newSpell.materialCost = materialCost;
        return newSpell;
    }

    public float GetCost (Container container, Creator creator, int creatorLevel)
    {
        int creationLevel = creatorLevels[creator];
        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
        {
            if (container == Container.Potion)
                return 25;
            if (container == Container.Scroll)
                return 12.5f;
            return 375;
        }

        if (container == Container.Potion)
            return 25 * creationLevel * creatorLevel;
        if (container == Container.Scroll)
            return 12.5f * creationLevel * creatorLevel;
        return 375 * creationLevel * creatorLevel;
    }

    public float GetCost (Container container, Creator creator)
    {
        int creationLevel = creatorLevels[creator];
        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
        {
            if (container == Container.Potion)
                return 25;
            if (container == Container.Scroll)
                return 12.5f;
            return 375;
        }

        if (container == Container.Potion)
            return 25 * MinCasterLevel (creator, creationLevel);
        if (container == Container.Scroll)
            return 12.5f * MinCasterLevel(creator, creationLevel);
        return 375 * MinCasterLevel(creator, creationLevel);
    }

    protected int MinCasterLevel (Creator creator, int spellLevel)
    {
        if (creator == Creator.Alc || creator == Creator.Brd || creator == Creator.Inq || creator == Creator.Mag || creator == Creator.Sum)
            return spellLevel * 3 - 2;
        if (creator == Creator.ClrOcl || creator == Creator.Drd || creator == Creator.SorWiz || creator == Creator.Wit)
            return spellLevel * 2 - 1;
        if (creator == Creator.Pal || creator == Creator.Rgr)
            return spellLevel * 3 + 1;

        return -1;
    }

    public static Spell PickSpell (List<Spell> spells, Container container)
    {
        float weightSum = 0f;

        for (int i = 0; i < spells.Count; i++)
        {
            Item.Rarity rarity = spells[i].rarities[container];
            weightSum += Campaign.RarityWeighting.RarityToWeight (rarity);
        }

        float randomWeightSum = Random.Range(0f, weightSum);
        float weightCounter = randomWeightSum;

        for (int i = 0; i < spells.Count; i++)
        {
            Item.Rarity rarity = spells[i].rarities[container];
            weightCounter -= Campaign.RarityWeighting.RarityToWeight(rarity);

            if (weightCounter <= 0f)
            {
                return spells[i];
            }
        }

        return null;
    }

    public static string GetJsonString(Spell spell)
    {
        string jsonString = "";

        jsonString += spell.name + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)spell.allowances[Container.Potion]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)spell.allowances[Container.Scroll]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)spell.allowances[Container.Wand]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)spell.rarities[Container.Potion]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)spell.rarities[Container.Scroll]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)spell.rarities[Container.Wand]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Alc]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Brd]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.ClrOcl]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Drd]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Inq]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Mag]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Pal]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Rgr]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.SorWiz]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Sum]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.creatorLevels[Creator.Wit]) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)spell.book) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.page) + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(spell.materialCost) + k_JsonSplitter[0];

        return jsonString;
    }

    public static Spell CreateFromJsonString(string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);

        Spell spell = CreateInstance<Spell>();

        spell.name = splitJsonString[0];
        spell.allowances[Container.Potion] = (Allowance)Wrapper<int>.CreateFromJsonString (splitJsonString[1]);
        spell.allowances[Container.Scroll] = (Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        spell.allowances[Container.Wand] = (Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[3]);
        spell.rarities[Container.Potion] = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
        spell.rarities[Container.Scroll] = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[5]);
        spell.rarities[Container.Wand] = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[6]);
        spell.creatorLevels[Creator.Alc] = Wrapper<int>.CreateFromJsonString (splitJsonString[7]);
        spell.creatorLevels[Creator.Brd] = Wrapper<int>.CreateFromJsonString (splitJsonString[8]);
        spell.creatorLevels[Creator.ClrOcl] = Wrapper<int>.CreateFromJsonString (splitJsonString[9]);
        spell.creatorLevels[Creator.Drd] = Wrapper<int>.CreateFromJsonString (splitJsonString[10]);
        spell.creatorLevels[Creator.Inq] = Wrapper<int>.CreateFromJsonString (splitJsonString[11]);
        spell.creatorLevels[Creator.Mag] = Wrapper<int>.CreateFromJsonString (splitJsonString[12]);
        spell.creatorLevels[Creator.Pal] = Wrapper<int>.CreateFromJsonString (splitJsonString[13]);
        spell.creatorLevels[Creator.Rgr] = Wrapper<int>.CreateFromJsonString (splitJsonString[14]);
        spell.creatorLevels[Creator.SorWiz] = Wrapper<int>.CreateFromJsonString (splitJsonString[15]);
        spell.creatorLevels[Creator.Sum] = Wrapper<int>.CreateFromJsonString (splitJsonString[16]);
        spell.creatorLevels[Creator.Wit] = Wrapper<int>.CreateFromJsonString(splitJsonString[17]);
        spell.book = (Book)Wrapper<int>.CreateFromJsonString (splitJsonString[18]);
        spell.page = Wrapper<int>.CreateFromJsonString (splitJsonString[19]);
        spell.materialCost = Wrapper<int>.CreateFromJsonString (splitJsonString[20]);
        
        return spell;
    }
}