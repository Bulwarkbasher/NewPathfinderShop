using UnityEngine;
using UnityEditor;
using System;

// TODO AFTER: use ItemCollectionEditor
[CustomEditor(typeof(WeaponQualityCollection))]
public class WeaponQualitiesCollectionEditor : AssetWithSubAssetElementsEditor<WeaponQualityCollection, WeaponQuality>
{
    SerializedProperty m_QualitiesProp;

    protected override void GetProperties ()
    {
        m_QualitiesProp = serializedObject.FindProperty("qualities");
    }

    protected override void AssetGUI ()
    {
        CollectionGUI (m_QualitiesProp);
        AddButtonGUI (m_QualitiesProp, WeaponQuality.CreateBlank);
    }
}
