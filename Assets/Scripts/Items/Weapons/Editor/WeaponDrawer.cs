using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Weapon))]
public class WeaponDrawer : SubAssetElementDrawer
{
    SerializedProperty m_CostProp;
    SerializedProperty m_RarityProp;
    SerializedProperty m_BookProp;
    SerializedProperty m_PageProp;
    
    protected override int GetPropertyLineCount (SerializedProperty property, GUIContent label)
    {
        int count = 5;
        return count;
    }

    protected override void GetProperties (SerializedProperty property)
    {
        m_CostProp = m_SerializedObject.FindProperty("cost");
        m_RarityProp = m_SerializedObject.FindProperty("rarity");
        m_BookProp = m_SerializedObject.FindProperty("book");
        m_PageProp = m_SerializedObject.FindProperty("page");
    }

    protected override void OnElementGUI (Rect totalPropertyRect, SerializedProperty property, GUIContent label, Rect nameFoldoutLineRect)
    {
        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_NameProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_CostProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_RarityProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EnumSettingEditorHelpers.DrawEnumSettingIndexPopup(nameFoldoutLineRect, m_BookProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_PageProp);
    }
}