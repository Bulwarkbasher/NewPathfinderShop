using System;
using System.Collections.Generic;
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


    [Serializable]
    public class Allowances
    {
        public Allowance potionAllowance = Allowance.Default;
        public Allowance scrollAllowance = Allowance.Default;
        public Allowance wandAllowance = Allowance.Default;

        public Allowance this [Container container]
        {
            get
            {
                switch (container)
                {
                    case Container.Potion:
                        return potionAllowance;
                    case Container.Scroll:
                        return scrollAllowance;
                    case Container.Wand:
                        return wandAllowance;
                    default:
                        throw new ArgumentOutOfRangeException ("container", container, null);
                }
            }
        }
    }


    [Serializable]
    public class Rarities
    {
        public Item.Rarity potionRarity = Item.Rarity.Mundane;
        public Item.Rarity scrollRarity = Item.Rarity.Mundane;
        public Item.Rarity wandRarity = Item.Rarity.Mundane;

        public Item.Rarity this[Container container]
        {
            get
            {
                switch (container)
                {
                    case Container.Potion:
                        return potionRarity;
                    case Container.Scroll:
                        return scrollRarity;
                    case Container.Wand:
                        return wandRarity;
                    default:
                        throw new ArgumentOutOfRangeException("container", container, null);
                }
            }
        }
    }


    [Serializable]
    public class CreatorLevels
    {
        public int alchemistLevel = -1;
        public int bardLevel = -1;
        public int clericOracleLevel = -1;
        public int druidLevel = -1;
        public int inquisitorLevel = -1;
        public int magusLevel = -1;
        public int paladinLevel = -1;
        public int rangerLevel = -1;
        public int sorcererWizardLevel = -1;
        public int summonerLevel = -1;
        public int witchLevel = -1;

        public int this [Creator creator]
        {
            get
            {
                switch (creator)
                {
                    case Creator.Alc:
                        return alchemistLevel;
                    case Creator.Brd:
                        return bardLevel;
                    case Creator.ClrOcl:
                        return clericOracleLevel;
                    case Creator.Drd:
                        return druidLevel;
                    case Creator.Inq:
                        return inquisitorLevel;
                    case Creator.Mag:
                        return magusLevel;
                    case Creator.Pal:
                        return paladinLevel;
                    case Creator.Rgr:
                        return rangerLevel;
                    case Creator.SorWiz:
                        return sorcererWizardLevel;
                    case Creator.Sum:
                        return summonerLevel;
                    case Creator.Wit:
                        return witchLevel;
                    default:
                        throw new ArgumentOutOfRangeException ("creator", creator, null);
                }
            }
        }
    }


    public Allowances allowances = new Allowances ();
    public Rarities rarities = new Rarities ();
    public CreatorLevels creatorLevels = new CreatorLevels ();
    public Book book;
    public int page;
    public int materialCost;

    public static Spell Create (string name, Allowances allowances, Rarities rarities,
        CreatorLevels creatorLevels, Book book, int page, int materialCost)
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

    public static Spell CreateBlank ()
    {
        return Create ("NAME", new Allowances (), new Rarities (),
            new CreatorLevels (), Book.CRB, 999, 0);    // TODO FROM HOME: change to start of spell section from CRB
    }

    public static int CostPerCreatorLevelBySpellLevel (Container container)
    {
        switch (container)
        {
            case Container.Potion:
                return 50;
            case Container.Scroll:
                return 25;
            case Container.Wand:
                return 750;
            default:
                throw new ArgumentOutOfRangeException (nameof(container), container, null);
        }
    }

    public float GetCostFromLevel (Container container, Creator creator, int creatorLevel, float chargeProportion = 1f)
    {
        int creationLevel = creatorLevels[creator];
        
        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return CostPerCreatorLevelBySpellLevel (container) * 0.5f * chargeProportion;

        return CostPerCreatorLevelBySpellLevel (container) * creationLevel * creatorLevel * chargeProportion;
    }

    public float GetCostFromLevel (Container container, Creator creator, float chargeProportion = 1f)
    {
        int creationLevel = creatorLevels[creator];
        
        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return CostPerCreatorLevelBySpellLevel (container) * 0.5f * chargeProportion;

        return CostPerCreatorLevelBySpellLevel (container) * creationLevel * MinCasterLevel (creator, creationLevel) * chargeProportion;
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
        allowances.potionAllowance = (Allowance)Wrapper<int>.CreateFromJsonString (splitJsonString[1]);
        allowances.scrollAllowance = (Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
        allowances.wandAllowance = (Allowance)Wrapper<int>.CreateFromJsonString(splitJsonString[3]);
        rarities.potionRarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[4]);
        rarities.scrollRarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[5]);
        rarities.wandRarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[6]);
        creatorLevels.alchemistLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[7]);
        creatorLevels.bardLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[8]);
        creatorLevels.clericOracleLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[9]);
        creatorLevels.druidLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[10]);
        creatorLevels.inquisitorLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[11]);
        creatorLevels.magusLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[12]);
        creatorLevels.paladinLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[13]);
        creatorLevels.rangerLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[14]);
        creatorLevels.sorcererWizardLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[15]);
        creatorLevels.summonerLevel = Wrapper<int>.CreateFromJsonString (splitJsonString[16]);
        creatorLevels.witchLevel = Wrapper<int>.CreateFromJsonString(splitJsonString[17]);
        book = (Book)Wrapper<int>.CreateFromJsonString (splitJsonString[18]);
        page = Wrapper<int>.CreateFromJsonString (splitJsonString[19]);
        materialCost = Wrapper<int>.CreateFromJsonString (splitJsonString[20]);
    }
}