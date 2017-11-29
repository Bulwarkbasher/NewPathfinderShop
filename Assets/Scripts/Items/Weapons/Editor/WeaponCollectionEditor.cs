using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponCollection))]
public class WeaponCollectionEditor : ItemCollectionEditor<WeaponCollection, Weapon>
{
    protected override void SaveableGUI ()
    {
        DrawDefaultAssetGUI (Weapon.CreateBlank, m_BooksProp.objectReferenceValue as EnumSetting);
    }
}
