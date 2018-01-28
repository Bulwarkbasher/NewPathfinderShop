using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumValue))]
public class EnumValueDrawer : JsonableDrawer
{
    SerializedProperty m_EnumSettingProp;
    SerializedProperty m_EnumedValuesProp;
    GUIContent[] settings;

    protected override float GetJsonablePropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    protected override void GetProperties(SerializedProperty property)
    {
        m_EnumSettingProp = m_SerializedObject.FindProperty("enumSetting");
        m_EnumedValuesProp = m_SerializedObject.FindProperty("enumedValues");
        settings = (m_EnumSettingProp.objectReferenceValue as EnumSetting).SettingsAsGUIContentArray();
    }

    protected override void OnJsonableGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(m_EnumSettingProp.objectReferenceValue == null)
        {
            EditorGUI.LabelField(position, "ENUMSETTING NULL");
            return;
        }

        int index = 0;
        int arraySize = m_EnumedValuesProp.arraySize;
        for (int i = 0; i < arraySize; i++)
        {
            SerializedProperty element = m_EnumedValuesProp.GetArrayElementAtIndex(i);
            if (element.boolValue)
                index = i;
        }

        EditorGUI.BeginChangeCheck();
        index = EditorGUI.Popup(position, index, settings);
        if (EditorGUI.EndChangeCheck())
        {
            for(int i = 0; i < arraySize; i++)
            {
                SerializedProperty element = m_EnumedValuesProp.GetArrayElementAtIndex(i);
                element.boolValue = i == index;
            }
        }
    }
}
