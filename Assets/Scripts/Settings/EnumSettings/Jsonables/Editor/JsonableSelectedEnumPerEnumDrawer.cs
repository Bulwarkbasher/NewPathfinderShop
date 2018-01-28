using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(JsonableSelectedEnumPerEnum))]
public class JsonableSelectedEnumPerEnumDrawer : JsonableDrawer
{
    SerializedProperty m_EnumSettingProp;
    SerializedProperty m_EnumedJsonablesProp;
    EnumSetting m_EnumSetting;

    protected override float GetJsonablePropertyHeight(SerializedProperty property, GUIContent label)
    {
        if(property.isExpanded)
            return EditorGUIUtility.singleLineHeight;

        return (m_EnumSetting.Length) * EditorGUIUtility.singleLineHeight;
    }

    protected override void GetProperties(SerializedProperty property)
    {
        m_EnumSettingProp = m_SerializedObject.FindProperty("enumSetting");
        m_EnumedJsonablesProp = m_SerializedObject.FindProperty("enumedJsonables");
        m_EnumSetting = m_EnumSettingProp.objectReferenceValue as EnumSetting;
    }

    protected override void OnJsonableGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (m_EnumSettingProp.objectReferenceValue == null)
        {
            property.isExpanded = false;
            EditorGUI.LabelField(position, "ENUMSETTING NULL");
            return;
        }

        position.height = EditorGUIUtility.singleLineHeight;
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

        if (!property.isExpanded)
            return;

        EditorGUI.indentLevel++;

        for (int i = 0; i < m_EnumSetting.Length; i++)
        {
            position.y += position.height;
            SerializedProperty jsonableElementProp = m_EnumedJsonablesProp.GetArrayElementAtIndex(i);
            GUIContent elementLabel = new GUIContent(m_EnumSetting[i]);
            EditorGUI.PropertyField(position, jsonableElementProp, elementLabel);
        }

        EditorGUI.indentLevel--;
    }
}
