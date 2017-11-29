using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpellCollection))]
public class SpellCollectionEditor : SaveableEditor<SpellCollection, Spell>
{
    SerializedProperty m_SpellsProp;
    SerializedProperty m_CharacterCasterTypesProp;
    SerializedProperty m_BooksProp;

    protected override void GetProperties ()
    {
        m_CharacterCasterTypesProp = serializedObject.FindProperty("characterCasterTypes");
        m_BooksProp = serializedObject.FindProperty("books");
        m_SpellsProp = serializedObject.FindProperty ("spells");
    }

    protected override void SaveableGUI ()
    {
        CollectionGUI (m_SpellsProp);
        AddButtonGUI(m_SpellsProp, Spell.CreateBlank, m_CharacterCasterTypesProp.objectReferenceValue as CasterTypesPerCharacterClass, m_BooksProp.objectReferenceValue as EnumSetting);
    }
}
