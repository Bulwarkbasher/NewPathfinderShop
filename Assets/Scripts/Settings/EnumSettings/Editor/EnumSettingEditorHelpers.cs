using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EnumSettingEditorHelpers
{
    public static int DrawEnumSettingPopup (int index, SerializedProperty enumSettingProp)
    {
        EnumSetting enumSetting = enumSettingProp.objectReferenceValue as EnumSetting;

        if(enumSetting == null)
        {
            throw new UnityException("Supplied SerializedProperty is not an EnumSetting.");
        }

        return EditorGUILayout.Popup(index, enumSetting.settings);
    }

    public static int DrawEnumSettingPopup (Rect position, int index, SerializedProperty enumSettingProp)
    {
        EnumSetting enumSetting = enumSettingProp.objectReferenceValue as EnumSetting;

        if (enumSetting == null)
        {
            throw new UnityException("Supplied SerializedProperty is not an EnumSetting.");
        }

        return EditorGUI.Popup(position, index, enumSetting.settings);
    }

    public static void DrawEnumSettingIndexPopup(SerializedProperty enumSettingIndexProp)
    {
        SerializedProperty enumSettingProp = enumSettingIndexProp.FindPropertyRelative("enumSetting");
        SerializedProperty indexProp = enumSettingIndexProp.FindPropertyRelative("index");

        indexProp.intValue = DrawEnumSettingPopup(indexProp.intValue, enumSettingProp);
    }

    public static void DrawEnumSettingIndexPopup(Rect position, SerializedProperty enumSettingIndexProp)
    {
        SerializedProperty enumSettingProp = enumSettingIndexProp.FindPropertyRelative("enumSetting");
        SerializedProperty indexProp = enumSettingIndexProp.FindPropertyRelative("index");

        indexProp.intValue = DrawEnumSettingPopup(position, indexProp.intValue, enumSettingProp);
    }

    public static int GetEnumSettingIntPairingLineCount (SerializedProperty enumSettingIntPairingProp)
    {
        SerializedProperty enumSettingProp = enumSettingIntPairingProp.FindPropertyRelative("enumSetting");
        SerializedProperty pairingsProp = enumSettingIntPairingProp.FindPropertyRelative("pairings");

        if (enumSettingProp.objectReferenceValue == null)
            return 1;

        return pairingsProp.arraySize + 1;
    }

    public static void DrawEnumSettingIntPairing (SerializedProperty enumSettingIntPairingProp, string label)
    {
        Rect position = EditorGUILayout.GetControlRect(false, GetEnumSettingIntPairingLineCount(enumSettingIntPairingProp));

        SerializedProperty enumSettingProp = enumSettingIntPairingProp.FindPropertyRelative("enumSetting");
        SerializedProperty pairingsProp = enumSettingIntPairingProp.FindPropertyRelative("pairings");

        EditorGUILayout.ObjectField(enumSettingProp, new GUIContent(label));
        EnumSetting enumSetting = enumSettingProp.objectReferenceValue as EnumSetting;
        if (enumSetting == null)
            pairingsProp.arraySize = 0;
        else
            pairingsProp.arraySize = enumSetting.settings.Length;

        for(int i = 0; i < pairingsProp.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(enumSetting.settings[i], GUILayout.Width(position.width * 0.8f));
            SerializedProperty elementProp = pairingsProp.GetArrayElementAtIndex(i);
            elementProp.intValue = EditorGUILayout.IntField(GUIContent.none, elementProp.intValue, GUILayout.Width(position.width * 0.2f));
            EditorGUILayout.EndHorizontal();
        }
    }
}
