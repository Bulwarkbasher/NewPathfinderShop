using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spell : Jsonable<Spell>
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

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)allowances[Container.Potion]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)allowances[Container.Scroll]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)allowances[Container.Wand]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)rarities[Container.Potion]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)rarities[Container.Scroll]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)rarities[Container.Wand]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Alc]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Brd]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.ClrOcl]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Drd]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Inq]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Mag]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Pal]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Rgr]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.SorWiz]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Sum]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(creatorLevels[Creator.Wit]) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)book) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(page) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString(materialCost) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        allowances[Container.Potion] = (Allowance)Wrapper<int>.CreateFromJsonString (splitJsonString[1]);
        allowances[Container.Scroll] = (Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        allowances[Container.Wand] = (Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[3]);
        rarities[Container.Potion] = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
        rarities[Container.Scroll] = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[5]);
        rarities[Container.Wand] = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[6]);
        creatorLevels[Creator.Alc] = Wrapper<int>.CreateFromJsonString (splitJsonString[7]);
        creatorLevels[Creator.Brd] = Wrapper<int>.CreateFromJsonString (splitJsonString[8]);
        creatorLevels[Creator.ClrOcl] = Wrapper<int>.CreateFromJsonString (splitJsonString[9]);
        creatorLevels[Creator.Drd] = Wrapper<int>.CreateFromJsonString (splitJsonString[10]);
        creatorLevels[Creator.Inq] = Wrapper<int>.CreateFromJsonString (splitJsonString[11]);
        creatorLevels[Creator.Mag] = Wrapper<int>.CreateFromJsonString (splitJsonString[12]);
        creatorLevels[Creator.Pal] = Wrapper<int>.CreateFromJsonString (splitJsonString[13]);
        creatorLevels[Creator.Rgr] = Wrapper<int>.CreateFromJsonString (splitJsonString[14]);
        creatorLevels[Creator.SorWiz] = Wrapper<int>.CreateFromJsonString (splitJsonString[15]);
        creatorLevels[Creator.Sum] = Wrapper<int>.CreateFromJsonString (splitJsonString[16]);
        creatorLevels[Creator.Wit] = Wrapper<int>.CreateFromJsonString(splitJsonString[17]);
        book = (Book)Wrapper<int>.CreateFromJsonString (splitJsonString[18]);
        page = Wrapper<int>.CreateFromJsonString (splitJsonString[19]);
        materialCost = Wrapper<int>.CreateFromJsonString (splitJsonString[20]);
    }
}