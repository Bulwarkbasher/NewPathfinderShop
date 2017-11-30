using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContainerRarities : Jsonable<SpellContainerRarities>
{
    public Item.Rarity potionRarity = Item.Rarity.Mundane;
    public Item.Rarity scrollRarity = Item.Rarity.Mundane;
    public Item.Rarity wandRarity = Item.Rarity.Mundane;

    public Item.Rarity this[Spell.Container container]
    {
        get
        {
            switch (container)
            {
                case Spell.Container.Potion:
                    return potionRarity;
                case Spell.Container.Scroll:
                    return scrollRarity;
                case Spell.Container.Wand:
                    return wandRarity;
                default:
                    throw new ArgumentOutOfRangeException("container", container, null);
            }
        }
    }

    public static SpellContainerRarities Create (Item.Rarity potionRarity, Item.Rarity scrollRarity, Item.Rarity wandRarity)
    {
        SpellContainerRarities newSpellContainerRarities = CreateInstance<SpellContainerRarities>();
        newSpellContainerRarities.potionRarity = potionRarity;
        newSpellContainerRarities.scrollRarity = scrollRarity;
        newSpellContainerRarities.wandRarity = wandRarity;
        return newSpellContainerRarities;
    }

    public static SpellContainerRarities CreateBlank ()
    {
        return Create(Item.Rarity.Mundane, Item.Rarity.Mundane, Item.Rarity.Mundane);
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += Wrapper<int>.GetJsonString((int)potionRarity) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)scrollRarity) + jsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)wandRarity) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        potionRarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[0]);
        scrollRarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[1]);
        wandRarity = (Item.Rarity)Wrapper<int>.CreateFromJsonString(splitJsonString[2]);
    }
}
