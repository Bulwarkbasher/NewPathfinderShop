using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellCollection : Saveable<SpellCollection>
{
    public CharacterCasterTypes characterCasterTypes;
    public EnumSetting books;
    public PerContainerPerCreatorRarity perContainerPerCreatorRarity;
    public Spell[] spells = new Spell[0];

    public static SpellCollection Create (string name, EnumSetting books, PerContainerPerCreatorRarity perContrainerPerCreatorRarity)
    {
        SpellCollection newSpellCollection = CreateInstance<SpellCollection> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Spell Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Spell Collection name invalid, name cannot start with Default");

        newSpellCollection.name = name;
        newSpellCollection.books = books;
        newSpellCollection.perContainerPerCreatorRarity = perContrainerPerCreatorRarity;
        newSpellCollection.spells = new Spell[0];

        SaveableHolder.AddSaveable(newSpellCollection);

        return newSpellCollection;
    }

    public Spell PickSpell (ref float budget, Spell.Container container, int chargeCount = 50)
    {
        if (spells.Length == 0)
            return null;

        PerCreatorRarity perCreatorRarity = perContainerPerCreatorRarity[container];
        string creator = perCreatorRarity.PickSpellCastingClass(characterCasterTypes);
        CharacterCasterTypes.CasterType creatorCasterType = characterCasterTypes[creator];

        List<Spell> availableSpells = new List<Spell> ();
        List<int> randomCasterLevelsForSpells = new List<int>();

        for (int i = 0; i < spells.Length; i++)
        {
            Spell currentSpell = spells[i];

            // Check the spell can be cast by selected creator.
            bool canBeCreatedByCreator = false;
            for(int j = 0; j < currentSpell.creatorLevels.pairings.Length; j++)
            {
                if (currentSpell.creatorLevels.enumSetting[j] == creator && currentSpell.creatorLevels.pairings[j] >= 0)
                    canBeCreatedByCreator = true;
            }
            if(!canBeCreatedByCreator)
                continue;

            // Given the spell can be cast by the creator, find at which level the creator casts it.
            int spellLevelForCreator = currentSpell.creatorLevels[creator];

            // Given the spell's level for the creator, find a random level for the creator.
            int minCreatorLevel = CharacterCasterTypes.MinCasterLevel(creatorCasterType, spellLevelForCreator);
            List<int> levelList = new List<int>();
            int currentLevel = 20;
            int levelCounter = 1;
            while(currentLevel >= minCreatorLevel)
            {
                for(int j = 0; j < levelCounter; j++)
                {
                    levelList.Add(currentLevel);
                }
                levelCounter++;
                currentLevel--;
            }
            int randomCreatorLevel = levelList[Random.Range(0, levelList.Count)];

            // Given the creator's level, find the cost of the spell.
            float cost = -1f;
            switch (container)
            {
                case Spell.Container.Potion:
                    cost = spells[i].GetPotionCost(creator, randomCreatorLevel);
                    break;
                case Spell.Container.Scroll:
                    cost = spells[i].GetScrollCost(creator, randomCreatorLevel);
                    break;
                case Spell.Container.Wand:
                    cost = spells[i].GetWandCost(creator, randomCreatorLevel, chargeCount);
                    break;
            }
            if (cost < 0f)
                continue;

            if (cost < budget)
            {
                availableSpells.Add (spells[i]);
                randomCasterLevelsForSpells.Add(randomCreatorLevel);
            }
        }

        Spell chosenSpell = Spell.PickSpell(availableSpells, container);

        int spellIndex = -1;
        for(int i = 0; i < availableSpells.Count; i++)
        {
            if (availableSpells[i] == chosenSpell)
                spellIndex = -1;
        }

        switch (container)
        {
            case Spell.Container.Potion:
                budget -= chosenSpell.GetPotionCost(creator, randomCasterLevelsForSpells[spellIndex]);
                break;
            case Spell.Container.Scroll:
                budget -= chosenSpell.GetScrollCost(creator, randomCasterLevelsForSpells[spellIndex]);
                break;
            case Spell.Container.Wand:
                budget -= chosenSpell.GetWandCost(creator, randomCasterLevelsForSpells[spellIndex], chargeCount);
                break;
        }

        return chosenSpell;
    }

    public void PickAndApplySpell (float budget, SpecificPotion specificPotion)
    {
        // TODO: complete me
    }

    public void PickAndApplySpell (float budget, SpecificScroll specificScroll)
    {
        // TODO: complete me
    }

    public void PickAndApplySpell(FloatRange budget, SpecificWand specificWand)
    {
        if (spells.Length == 0)
            return;

        PerCreatorRarity perCreatorRarity = perContainerPerCreatorRarity[Spell.Container.Wand];
        specificWand.creator = perCreatorRarity.PickSpellCastingClass(characterCasterTypes);
        CharacterCasterTypes.CasterType creatorCasterType = characterCasterTypes[specificWand.creator];

        List<Spell> availableSpells = new List<Spell>();
        List<int> randomCasterLevelsForSpells = new List<int>();

        for (int i = 0; i < spells.Length; i++)
        {
            Spell currentSpell = spells[i];

            // Check the spell can be cast by selected creator.
            bool canBeCreatedByCreator = false;
            for (int j = 0; j < currentSpell.creatorLevels.pairings.Length; j++)
            {
                if (currentSpell.creatorLevels.enumSetting[j] == specificWand.creator && currentSpell.creatorLevels.pairings[j] >= 0)
                    canBeCreatedByCreator = true;
            }
            if (!canBeCreatedByCreator)
                continue;

            // Given the spell can be cast by the creator, find at which level the creator casts it.
            int spellLevelForCreator = currentSpell.creatorLevels[specificWand.creator];

            // Given the spell's level for the creator, find a random level for the creator.
            int minCreatorLevel = CharacterCasterTypes.MinCasterLevel(creatorCasterType, spellLevelForCreator);
            List<int> levelList = new List<int>();
            int currentLevel = 20;
            int levelCounter = 1;
            while (currentLevel >= minCreatorLevel)
            {
                for (int j = 0; j < levelCounter; j++)
                {
                    levelList.Add(currentLevel);
                }
                levelCounter++;
                currentLevel--;
            }
            int randomCreatorLevel = levelList[Random.Range(0, levelList.Count)];

            // Given the creator's level, find the cost of the spell.
            float cost = spells[i].GetWandCost(specificWand.creator, randomCreatorLevel, specificWand.charges);
            if (cost < 0f)
                continue;

            if (cost > budget.min && cost < budget.max)
            {
                availableSpells.Add(spells[i]);
                randomCasterLevelsForSpells.Add(randomCreatorLevel);
            }
        }

        specificWand.spell = Spell.PickSpell(availableSpells, Spell.Container.Wand);

        int spellIndex = -1;
        for (int i = 0; i < availableSpells.Count; i++)
        {
            if (availableSpells[i] == specificWand.spell)
                spellIndex = -1;
        }
        specificWand.cost = specificWand.spell.GetWandCost(specificWand.creator, randomCasterLevelsForSpells[spellIndex], specificWand.charges);
        
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

    public void AddBlankSpell()
    {
        Spell blankSpell = Spell.CreateBlank(characterCasterTypes, books);
        AddSpell(blankSpell);
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
        jsonString += characterCasterTypes.name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];
        jsonString += perContainerPerCreatorRarity.name + jsonSplitter[0];

        for (int i = 0; i < spells.Length; i++)
        {
            jsonString += Spell.GetJsonString(spells[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        characterCasterTypes = CharacterCasterTypes.Load(splitJsonString[1]);
        books = EnumSetting.Load(splitJsonString[2]);
        perContainerPerCreatorRarity = PerContainerPerCreatorRarity.Load(splitJsonString[3]);

        spells = new Spell[splitJsonString.Length - 4];
        for (int i = 0; i < spells.Length; i++)
        {
            spells[i] = Spell.CreateFromJsonString(splitJsonString[i + 4]);
        }
    }
}
