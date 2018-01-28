using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// TODO NEXT: complete this class and others in the same folder.
[CustomEditor(typeof(RarityPerCharacterClass))]
public class RarityPerCharacterClassEditor : SaveableEditor
{
    SerializedProperty m_EnumSettingProp;
    SerializedProperty m_EnumedJsonablesProp;
    EnumSetting m_CharacterClassesEnumSetting;
    EnumSetting m_RaritiesEnumSetting;

    GUIContent m_EnumSettingContent = new GUIContent("Character Classes");
    GUIContent m_RaritiesEnumSettingContent = new GUIContent("Rarities");

    private void OnEnable()
    {
        m_EnumSettingProp = serializedObject.FindProperty("m_EnumSetting");
        m_EnumedJsonablesProp = serializedObject.FindProperty("m_EnumedJsonables");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(m_EnumSettingProp, m_EnumSettingContent);
        m_RaritiesEnumSetting = EditorGUILayout.ObjectField(m_RaritiesEnumSettingContent, m_RaritiesEnumSetting, typeof(EnumSetting), false) as EnumSetting;
        
        if (EditorGUI.EndChangeCheck() && m_EnumSettingProp.objectReferenceValue != null && m_RaritiesEnumSetting != null)
        {
            ClearJsonableArray(m_EnumedJsonablesProp);

            m_CharacterClassesEnumSetting = m_EnumSettingProp.objectReferenceValue as EnumSetting;
            int count = m_CharacterClassesEnumSetting.Length;
            for(int i = 0; i < count; i++)
            {
                CreateJsonableInArray(m_EnumedJsonablesProp, EnumValue.CreateBlank, m_RaritiesEnumSetting);
            }
        }

        if(m_EnumSettingProp.objectReferenceValue != null && m_RaritiesEnumSetting != null)
        {
            for(int i = 0; i < m_CharacterClassesEnumSetting.Length; i++)
            {
                SerializedProperty element = m_EnumedJsonablesProp.GetArrayElementAtIndex(i);
                GUIContent label = new GUIContent(m_CharacterClassesEnumSetting[i]);
                EditorGUILayout.PropertyField(element, label);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
