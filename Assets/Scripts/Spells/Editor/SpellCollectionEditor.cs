using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpellCollection))]
public class SpellCollectionEditor : AssetWithSubAssetElementsEditor<SpellCollection, Spell>
{
    SerializedProperty m_SpellsProp;

    protected override void GetProperties ()
    {
        m_SpellsProp = serializedObject.FindProperty ("spells");
    }

    protected override void AssetGUI ()
    {
        CollectionGUI (m_SpellsProp);
        AddButtonGUI (m_SpellsProp, Spell.CreateBlank);
    }
}
