using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntValuedEnum))]
public class IntValuedEnumDrawer : JsonableDrawer
{
    SerializedProperty m_EnumSettingProp;
    SerializedProperty m_EnumedValues;
    EnumSetting m_EnumSetting;

    protected override float GetJsonablePropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
            return EditorGUIUtility.singleLineHeight;

        return (m_EnumSetting.Length) * EditorGUIUtility.singleLineHeight;
    }

    protected override void GetProperties(SerializedProperty property)
    {
        m_EnumSettingProp = m_SerializedObject.FindProperty("enumSetting");
        m_EnumedValues = m_SerializedObject.FindProperty("enumedValues");
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
            SerializedProperty enumValueProp = m_EnumedValues.GetArrayElementAtIndex(i);
            GUIContent elementLabel = new GUIContent(m_EnumSetting[i]);
            EditorGUI.PropertyField(position, enumValueProp, elementLabel);
        }

        EditorGUI.indentLevel--;
    }
}
