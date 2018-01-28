using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Weapon))]
public class WeaponDrawer : JsonableDrawer
{
    SerializedProperty m_CostProp;
    SerializedProperty m_RarityProp;
    SerializedProperty m_BookProp;
    SerializedProperty m_PageProp;
    
    protected override float GetJsonablePropertyHeight (SerializedProperty property, GUIContent label)
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

    protected override void OnJsonableGUI (Rect totalPropertyRect, SerializedProperty property, GUIContent label)
    {
        totalPropertyRect.y += totalPropertyRect.height;
        EditorGUI.PropertyField(totalPropertyRect, m_NameProp);

        totalPropertyRect.y += totalPropertyRect.height;
        EditorGUI.PropertyField(totalPropertyRect, m_CostProp);

        totalPropertyRect.y += totalPropertyRect.height;
        EditorGUI.PropertyField(totalPropertyRect, m_RarityProp);

        totalPropertyRect.y += totalPropertyRect.height;
        EnumSettingEditorHelpers.DrawEnumSettingIndexPopup(totalPropertyRect, m_BookProp);

        totalPropertyRect.y += totalPropertyRect.height;
        EditorGUI.PropertyField(totalPropertyRect, m_PageProp);
    }
}