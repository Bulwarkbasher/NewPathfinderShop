using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(WeaponQualityCollection))]
public class WeaponQualitiesCollectionEditor : Editor
{
    WeaponQualityCollection m_WeaponQualityCollection;
    SerializedProperty m_QualitiesProp;
    
    private void OnEnable ()
    {
        m_QualitiesProp = serializedObject.FindProperty("qualities");

        m_WeaponQualityCollection = (WeaponQualityCollection)target;
    }


    public override void OnInspectorGUI()
    {
        for (int i = 0; i < m_QualitiesProp.arraySize; i++)
        {
            SerializedProperty elementProp = m_QualitiesProp.GetArrayElementAtIndex (i);
            EditorGUILayout.PropertyField (elementProp);
        }

        if (GUILayout.Button("Add"))
        {
            m_QualitiesProp.AddObjectAsSubAsset<WeaponQuality>(m_WeaponQualityCollection, true);
        }
    }
}
