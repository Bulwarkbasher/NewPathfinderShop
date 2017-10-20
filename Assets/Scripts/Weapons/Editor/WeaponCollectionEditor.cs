using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponCollection))]
public class WeaponCollectionEditor : Editor
{
    WeaponCollection m_WeaponCollection;
    SerializedProperty m_ItemsProp;
    SerializedProperty m_WeaponQualityCollectionProp;

    private void OnEnable ()
    {
        m_ItemsProp = serializedObject.FindProperty("items");
        m_WeaponQualityCollectionProp = serializedObject.FindProperty ("potentialWeaponQualities");

        m_WeaponCollection = (WeaponCollection)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField (m_WeaponQualityCollectionProp);

        for (int i = 0; i < m_ItemsProp.arraySize; i++)
        {
            SerializedProperty elementProp = m_ItemsProp.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(elementProp);
        }

        if (GUILayout.Button("Add"))
        {
            m_ItemsProp.AddObjectAsSubAsset<WeaponQuality>(m_WeaponCollection, true);
        }
    }
}
