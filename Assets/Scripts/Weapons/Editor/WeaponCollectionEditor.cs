using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponCollection))]
public class WeaponCollectionEditor : ItemCollectionEditor<WeaponCollection, Weapon>
{
    SerializedProperty m_WeaponQualityCollectionProp;

    protected override void GetAdditionalProperties ()
    {
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

        DrawDefaultAssetGUI (Weapon.CreateBlank, m_WeaponQualityCollectionProp.objectReferenceValue as WeaponQualityCollection);
    }
}
