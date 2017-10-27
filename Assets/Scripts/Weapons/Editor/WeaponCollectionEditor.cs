using UnityEngine;
using UnityEditor;

// TODO AFTER: use ItemCollectionEditor
[CustomEditor(typeof(WeaponCollection))]
public class WeaponCollectionEditor : AssetWithSubAssetElementsEditor<WeaponCollection, Weapon>
{
    SerializedProperty m_ItemsProp;
    SerializedProperty m_WeaponQualityCollectionProp;

    protected override void GetProperties ()
    {
        m_ItemsProp = serializedObject.FindProperty("items");
        m_WeaponQualityCollectionProp = serializedObject.FindProperty("potentialWeaponQualities");
    }

    protected override void AssetGUI ()
    {
        if (m_ItemsProp.arraySize == 0)
        {
            EditorGUILayout.PropertyField(m_WeaponQualityCollectionProp);
        }
        else
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(new GUIContent("Weapon Quality Collection"), new GUIContent(m_WeaponQualityCollectionProp.objectReferenceValue.name));
            EditorGUILayout.EndVertical();
        }

        CollectionGUI (m_ItemsProp);

        if (m_WeaponQualityCollectionProp.objectReferenceValue != null)
        {
            AddButtonGUI (m_ItemsProp, Weapon.CreateBlank, m_WeaponQualityCollectionProp.objectReferenceValue as WeaponQualityCollection);
        }
    }
}
