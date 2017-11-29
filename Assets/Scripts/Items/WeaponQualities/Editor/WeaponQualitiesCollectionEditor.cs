using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(WeaponQualityCollection))]
public class WeaponQualitiesCollectionEditor : ItemCollectionEditor<WeaponQualityCollection, WeaponQuality>
{
    protected override void SaveableGUI()
    {
        DrawDefaultAssetGUI(WeaponQuality.CreateBlank, m_BooksProp.objectReferenceValue as EnumSetting);
    }
}
