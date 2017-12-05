using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RarityPerCharacterClassPerSpellContainer : SaveableWithEnumSaveables<RarityPerCharacterClassPerSpellContainer, RarityPerCharacterClass>
{
    public static RarityPerCharacterClassPerSpellContainer Create (string name, EnumSetting spellContainerEnum, RarityPerCharacterClass[] characterClassRarities)
    {
        RarityPerCharacterClassPerSpellContainer newPerContainerPerCreatorRarity = CreateInstance<RarityPerCharacterClassPerSpellContainer>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Settings name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Settings name invalid, name cannot start with Default");

        newPerContainerPerCreatorRarity.name = name;
        newPerContainerPerCreatorRarity.m_EnumSetting = spellContainerEnum;
        newPerContainerPerCreatorRarity.m_EnumedSaveables = characterClassRarities;

        SaveableHolder.AddSaveable(newPerContainerPerCreatorRarity);

        return newPerContainerPerCreatorRarity;
    }
}
