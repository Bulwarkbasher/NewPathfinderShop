using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class Spell : Jsonable<Spell>
{
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


    public Allowances allowances = new Allowances ();
    public Rarities rarities = new Rarities ();
    public CharacterCasterTypes creatorCasterTypes = new CharacterCasterTypes();    // Not shown
    public EnumSettingIntPairing creatorLevels = new EnumSettingIntPairing();
    public EnumSettingIndex book;
    public int page;
    public int materialCost;

    public static Spell Create(string name, Allowances allowances, Rarities rarities, CharacterCasterTypes creatorCasterTypes,
        EnumSettingIntPairing creatorLevels, EnumSettingIndex book, int page, int materialCost)
    {
        Spell newSpell = CreateInstance<Spell> ();
        newSpell.name = name;
        newSpell.allowances = allowances;
        newSpell.rarities = rarities;
        newSpell.creatorCasterTypes = creatorCasterTypes;
        newSpell.creatorLevels = creatorLevels;
        newSpell.book = book;
        newSpell.page = page;
        newSpell.materialCost = materialCost;
        return newSpell;
    }

    public static Spell CreateBlank (CharacterCasterTypes characterCasterTypes, EnumSetting books)
    {
        return Create ("NAME", new Allowances (), new Rarities (), characterCasterTypes, 
            new EnumSettingIntPairing(characterCasterTypes.characterClasses), new EnumSettingIndex(books, 0), 239, 0);
    }

    public float GetPotionCost(string creator)
    {
        Allowance allowance = allowances[Container.Potion];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 3)
            return -1f;

        int creatorLevel = CharacterCasterTypes.MinCasterLevel(creatorCasterTypes[creator], creationLevel);

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return 25f + materialCost;

        return 50f * creationLevel * creatorLevel + materialCost;
    }

    public float GetPotionCost(string creator, int creatorLevel)
    {
        Allowance allowance = allowances[Container.Potion];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return 25f + materialCost;

        return 50f * creationLevel * creatorLevel + materialCost;
    }

    public float GetScrollCost(string creator)
    {
        Allowance allowance = allowances[Container.Scroll];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 3)
            return -1f;

        int creatorLevel = CharacterCasterTypes.MinCasterLevel(creatorCasterTypes[creator], creationLevel);

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return 12.5f + materialCost;

        return 25f * creationLevel * creatorLevel + materialCost;
    }

    public float GetScrollCost(string creator, int creatorLevel)
    {
        Allowance allowance = allowances[Container.Scroll];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return 12.5f + materialCost;

        return 25f * creationLevel * creatorLevel + materialCost;
    }

    public float GetWandCost(string creator, int charges)
    {
        Allowance allowance = allowances[Container.Wand];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 4)
            return -1f;

        int creatorLevel = CharacterCasterTypes.MinCasterLevel(creatorCasterTypes[creator], creationLevel);

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return (7.5f + materialCost) * charges;

        return (15f + materialCost) * charges * creationLevel * creatorLevel;
    }

    public float GetWandCost(string creator, int creatorLevel, int charges)
    {
        Allowance allowance = allowances[Container.Wand];
        if (allowance == Allowance.CannotBe)
            return -1f;

        int creationLevel = creatorLevels[creator];
        if (allowance == Allowance.Default && creationLevel > 3)
            return -1f;

        if (creationLevel == -1)
            return -1f;
        if (creationLevel == 0)
            return (7.5f + materialCost) * charges;

        return (15 + materialCost) * charges * creationLevel * creatorLevel;
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
        jsonString += creatorCasterTypes.name + jsonString[0];
        jsonString += EnumSettingIntPairing.GetJsonString(creatorLevels) + jsonSplitter[0];
        jsonString += EnumSettingIndex.GetJsonString(book) + jsonSplitter[0];
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
        creatorCasterTypes = CharacterCasterTypes.Load(splitJsonString[7]);
        creatorLevels = EnumSettingIntPairing.CreateFromJsonString(splitJsonString[8]);
        book = EnumSettingIndex.CreateFromJsonString(splitJsonString[9]);
        page = Wrapper<int>.CreateFromJsonString (splitJsonString[10]);
        materialCost = Wrapper<int>.CreateFromJsonString (splitJsonString[11]);
    }
}