using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(WeaponQualityCollection))]
public class WeaponQualitiesCollectionEditor : ItemCollectionEditor<WeaponQualityCollection, WeaponQuality>
{
    protected override void AssetGUI ()
    {
        DrawDefaultAssetGUI (WeaponQuality.CreateBlank);
    }
}
