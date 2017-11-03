using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerCreatorRarity : Saveable<PerCreatorRarity>
{
    [SerializeField]
    protected Item.Rarity m_AlchemistRarity;
    [SerializeField]
    protected Item.Rarity m_BardRarity;
    [SerializeField]
    protected Item.Rarity m_ClericOracleRarity;
    [SerializeField]
    protected Item.Rarity m_DruidRarity;
    [SerializeField]
    protected Item.Rarity m_InquisitorRarity;
    [SerializeField]
    protected Item.Rarity m_MagusRarity;
    [SerializeField]
    protected Item.Rarity m_PaladinRarity;
    [SerializeField]
    protected Item.Rarity m_RangerRarity;
    [SerializeField]
    protected Item.Rarity m_SorcererWizardRarity;
    [SerializeField]
    protected Item.Rarity m_SummonerRarity;
    [SerializeField]
    protected Item.Rarity m_WitchRarity;

    public Item.Rarity this [Spell.Creator creator]
    {
        get
        {
            switch (creator)
            {
                case Spell.Creator.Alc:
                    return m_AlchemistRarity;
                case Spell.Creator.Brd:
                    return m_BardRarity;
                case Spell.Creator.ClrOcl:
                    return m_ClericOracleRarity;
                case Spell.Creator.Drd:
                    return m_DruidRarity;
                case Spell.Creator.Inq:
                    return m_InquisitorRarity;
                case Spell.Creator.Mag:
                    return m_MagusRarity;
                case Spell.Creator.Pal:
                    return m_PaladinRarity;
                case Spell.Creator.Rgr:
                    return m_RangerRarity;
                case Spell.Creator.SorWiz:
                    return m_SorcererWizardRarity;
                case Spell.Creator.Sum:
                    return m_SummonerRarity;
                case Spell.Creator.Wit:
                    return m_WitchRarity;
                default:
                    throw new ArgumentOutOfRangeException (nameof(creator), creator, null);
            }
        }
    }
    
    // TODO NEXT: complete this.  Compare to PerSizeAvailability and make another setting for PerContainerPerCreatorRarity compared to PerStockTypePerSizeAvailability
    protected override string ConvertToJsonString (string[] jsonSplitter)
    {
        throw new System.NotImplementedException ();
    }

    protected override void SetupFromSplitJsonString (string[] splitJsonString)
    {
        throw new System.NotImplementedException ();
    }
}
