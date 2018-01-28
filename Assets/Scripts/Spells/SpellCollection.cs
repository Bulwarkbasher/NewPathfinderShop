using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class SpellCollection : Saveable<SpellCollection>
{
    public EnumSetting spellContainers;
    public EnumSetting allowances;
    public EnumSetting rarities;
    public EnumSetting characterClasses;
    public SaveableSelectedEnumPerEnum characterCasterTypes;
    public EnumSetting books;
    public RarityPerCharacterClassPerSpellContainer rarityPerCharacterClassPerSpellContainer;
    public SpellCollectionFilter spellCollectionFilter;
    public Spell[] spells = new Spell[0];
    public bool[] doesSpellPassFilter = new bool[0];

    public static SpellCollection Create (string name, EnumSetting spellContainers, EnumSetting allowances,
        EnumSetting rarities, EnumSetting characterClasses, SaveableSelectedEnumPerEnum characterCasterTypes,
        EnumSetting books, RarityPerCharacterClassPerSpellContainer rarityPerCharacterClassPerSpellContainer)
    {
        SpellCollection newSpellCollection = CreateInstance<SpellCollection> ();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Spell Collection name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Spell Collection name invalid, name cannot start with Default");

        newSpellCollection.name = name;
        newSpellCollection.spellContainers = spellContainers;
        newSpellCollection.allowances = allowances;
        newSpellCollection.rarities = rarities;
        newSpellCollection.characterClasses = characterClasses;
        newSpellCollection.characterCasterTypes = characterCasterTypes;
        newSpellCollection.books = books;
        newSpellCollection.spellCollectionFilter = SpellCollectionFilter.CreateBlank(allowances, rarities, characterClasses, books);
        newSpellCollection.spells = new Spell[0];

        SaveableHolder.AddSaveable(newSpellCollection);

        return newSpellCollection;
    }
    
    public void PickAndApplySpell (FloatRange budget, SpecificPotion specificPotion)
    {
        if (spells.Length == 0)
            return;

        RarityPerCharacterClass perCreatorRarity = rarityPerCharacterClassPerSpellContainer["Potion"];
        specificPotion.creator = perCreatorRarity.PickSpellCastingClass(characterCasterTypes);
        string creatorCasterType = characterCasterTypes[specificPotion.creator];

        List<Spell> availableSpells = new List<Spell>();
        List<int> randomCasterLevelsForSpells = new List<int>();

        for (int i = 0; i < spells.Length; i++)
        {
            Spell currentSpell = spells[i];

            // Check the spell can be cast by selected creator.
            bool canBeCreatedByCreator = false;
            for (int j = 0; j < currentSpell.creatorLevels.Length; j++)
            {
                if (currentSpell.creatorLevels.enumSetting[j] == specificPotion.creator && currentSpell.creatorLevels[j] >= 0)
                    canBeCreatedByCreator = true;
            }
            if (!canBeCreatedByCreator)
                continue;

            // Given the spell can be cast by the creator, find at which level the creator casts it.
            int spellLevelForCreator = currentSpell.creatorLevels[specificPotion.creator];

            // Given the spell's level for the creator, find a random level for the creator.
            int minCreatorLevel = Spell.MinCasterLevel(creatorCasterType, spellLevelForCreator);
            int creatorLevel = minCreatorLevel;
            if (!Campaign.UsesMinimumCasterLevelForSpellContainerItems)
            {
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
                creatorLevel = levelList[Random.Range(0, levelList.Count)];
            }

            // Given the creator's level, find the cost of the spell.
            float cost = spells[i].GetPotionCost(specificPotion.creator, creatorLevel);
            if (cost < 0f)
                continue;

            if (cost > budget.min && cost < budget.max)
            {
                availableSpells.Add(spells[i]);
                randomCasterLevelsForSpells.Add(creatorLevel);
            }
        }

        specificPotion.spell = Spell.PickSpell(availableSpells, "Potion");

        int spellIndex = -1;
        for (int i = 0; i < availableSpells.Count; i++)
        {
            if (availableSpells[i] == specificPotion.spell)
                spellIndex = -1;
        }
        specificPotion.cost = specificPotion.spell.GetPotionCost(specificPotion.creator, randomCasterLevelsForSpells[spellIndex]);
    }

    public void PickAndApplySpell (FloatRange budget, SpecificScroll specificScroll)
    {
        if (spells.Length == 0)
            return;

        RarityPerCharacterClass perCreatorRarity = rarityPerCharacterClassPerSpellContainer["Scroll"];
        specificScroll.creator = perCreatorRarity.PickSpellCastingClass(characterCasterTypes);
        string creatorCasterType = characterCasterTypes[specificScroll.creator];

        List<Spell> availableSpells = new List<Spell>();
        List<int> randomCasterLevelsForSpells = new List<int>();

        for (int i = 0; i < spells.Length; i++)
        {
            Spell currentSpell = spells[i];

            // Check the spell can be cast by selected creator.
            bool canBeCreatedByCreator = false;
            for (int j = 0; j < currentSpell.creatorLevels.Length; j++)
            {
                if (currentSpell.creatorLevels.enumSetting[j] == specificScroll.creator && currentSpell.creatorLevels[j] >= 0)
                    canBeCreatedByCreator = true;
            }
            if (!canBeCreatedByCreator)
                continue;

            // Given the spell can be cast by the creator, find at which level the creator casts it.
            int spellLevelForCreator = currentSpell.creatorLevels[specificScroll.creator];

            // Given the spell's level for the creator, find a random level for the creator.
            int minCreatorLevel = Spell.MinCasterLevel(creatorCasterType, spellLevelForCreator);
            int creatorLevel = minCreatorLevel;
            if (!Campaign.UsesMinimumCasterLevelForSpellContainerItems)
            {
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
                creatorLevel = levelList[Random.Range(0, levelList.Count)];
            }

            // Given the creator's level, find the cost of the spell.
            float cost = spells[i].GetScrollCost(specificScroll.creator, creatorLevel);
            if (cost < 0f)
                continue;

            if (cost > budget.min && cost < budget.max)
            {
                availableSpells.Add(spells[i]);
                randomCasterLevelsForSpells.Add(creatorLevel);
            }
        }

        specificScroll.spell = Spell.PickSpell(availableSpells, "Scroll");

        int spellIndex = -1;
        for (int i = 0; i < availableSpells.Count; i++)
        {
            if (availableSpells[i] == specificScroll.spell)
                spellIndex = -1;
        }
        specificScroll.cost = specificScroll.spell.GetScrollCost(specificScroll.creator, randomCasterLevelsForSpells[spellIndex]);
    }

    public void PickAndApplySpell(FloatRange budget, SpecificWand specificWand)
    {
        if (spells.Length == 0)
            return;

        RarityPerCharacterClass perCreatorRarity = rarityPerCharacterClassPerSpellContainer["Wand"];
        specificWand.creator = perCreatorRarity.PickSpellCastingClass(characterCasterTypes);
        string creatorCasterType = characterCasterTypes[specificWand.creator];

        List<Spell> availableSpells = new List<Spell>();
        List<int> randomCasterLevelsForSpells = new List<int>();

        for (int i = 0; i < spells.Length; i++)
        {
            Spell currentSpell = spells[i];

            // Check the spell can be cast by selected creator.
            bool canBeCreatedByCreator = false;
            for (int j = 0; j < currentSpell.creatorLevels.Length; j++)
            {
                if (currentSpell.creatorLevels.enumSetting[j] == specificWand.creator && currentSpell.creatorLevels[j] >= 0)
                    canBeCreatedByCreator = true;
            }
            if (!canBeCreatedByCreator)
                continue;

            // Given the spell can be cast by the creator, find at which level the creator casts it.
            int spellLevelForCreator = currentSpell.creatorLevels[specificWand.creator];

            // Given the spell's level for the creator, find a random level for the creator.
            int minCreatorLevel = Spell.MinCasterLevel(creatorCasterType, spellLevelForCreator);
            int creatorLevel = minCreatorLevel;
            if (!Campaign.UsesMinimumCasterLevelForSpellContainerItems)
            {
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
                creatorLevel = levelList[Random.Range(0, levelList.Count)];
            }
            
            // Given the creator's level, find the cost of the spell.
            float cost = spells[i].GetWandCost(specificWand.creator, creatorLevel, specificWand.charges);
            if (cost < 0f)
                continue;

            if (cost > budget.min && cost < budget.max)
            {
                availableSpells.Add(spells[i]);
                randomCasterLevelsForSpells.Add(creatorLevel);
            }
        }

        specificWand.spell = Spell.PickSpell(availableSpells, "Wand");

        int spellIndex = -1;
        for (int i = 0; i < availableSpells.Count; i++)
        {
            if (availableSpells[i] == specificWand.spell)
                spellIndex = -1;
        }
        specificWand.cost = specificWand.spell.GetWandCost(specificWand.creator, randomCasterLevelsForSpells[spellIndex], specificWand.charges);        
    }

    public void AddSpell ()
    {
        Spell newSpell = Spell.CreateBlank(spellContainers, allowances, rarities, characterClasses, characterCasterTypes, books);
        AddSpell(newSpell);
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

    public void SortByNameAssending()
    {
        spells = spells.OrderBy(spell => spell.name).ToArray();
    }

    public void SortByNameDecending()
    {
        spells = spells.OrderByDescending(spell => spell.name).ToArray();
    }

    public void SortByCostAssending()
    {
        spells = spells.OrderBy(spell => spell.materialCost).ToArray();
    }

    public void SortByCostDecending()
    {
        spells = spells.OrderByDescending(spell => spell.materialCost).ToArray();
    }

    public void SortByBookAssending()
    {
        spells = spells.OrderBy(spell => spell.book.GetIndex()).ToArray();
    }

    public void SortByBookDecending()
    {
        spells = spells.OrderByDescending(spell => spell.book.GetIndex()).ToArray();
    }

    public void SortByPageAssending()
    {
        spells = spells.OrderBy(spell => spell.page).ToArray();
    }

    public void SortByPageDecending()
    {
        spells = spells.OrderByDescending(spell => spell.page).ToArray();
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += spellContainers.name + jsonSplitter[0];
        jsonString += allowances.name + jsonSplitter[0];
        jsonString += rarities.name + jsonSplitter[0];
        jsonString += characterClasses.name + jsonSplitter[0];
        jsonString += characterCasterTypes.name + jsonSplitter[0];
        jsonString += books.name + jsonSplitter[0];
        jsonString += rarityPerCharacterClassPerSpellContainer.name + jsonSplitter[0];
        jsonString += SpellCollectionFilter.GetJsonString(spellCollectionFilter) + jsonSplitter[0];

        for (int i = 0; i < spells.Length; i++)
        {
            jsonString += Spell.GetJsonString(spells[i]) + jsonSplitter[0];
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        spellContainers = EnumSetting.Load(splitJsonString[1]);
        allowances = EnumSetting.Load(splitJsonString[2]);
        rarities = EnumSetting.Load(splitJsonString[3]);
        characterClasses = EnumSetting.Load(splitJsonString[4]);
        characterCasterTypes = SaveableSelectedEnumPerEnum.Load(splitJsonString[5]);
        books = EnumSetting.Load(splitJsonString[6]);
        rarityPerCharacterClassPerSpellContainer = RarityPerCharacterClassPerSpellContainer.Load(splitJsonString[7]);
        spellCollectionFilter = SpellCollectionFilter.CreateFromJsonString(splitJsonString[8]);

        spells = new Spell[splitJsonString.Length - 9];
        for (int i = 0; i < spells.Length; i++)
        {
            spells[i] = Spell.CreateFromJsonString(splitJsonString[i + 9]);
        }

        spellCollectionFilter.ApplyFilter(this);
    }
}
