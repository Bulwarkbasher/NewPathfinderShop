using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WeaponQuality))]
public class WeaponQualityDrawer : PropertyDrawer
{
    SerializedObject m_SerializedObject;
    SerializedProperty m_NameProp;
    SerializedProperty m_CostProp;
    SerializedProperty m_RarityProp;
    SerializedProperty m_PageProp;
    SerializedProperty m_QualityTypeProp;
    SerializedProperty m_BonusEquivProp;
    bool m_HasSetupRun;

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        if(!m_HasSetupRun)
            Setup (property);

        int count = m_NameProp.isExpanded ? 7 : 1;
        return count * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        if (!m_HasSetupRun)
            Setup(property);

        Rect singleLineRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        m_NameProp.isExpanded = EditorGUI.Foldout (singleLineRect, m_NameProp.isExpanded, m_NameProp.stringValue);

        if(!m_NameProp.isExpanded)
            return;

        EditorGUI.indentLevel++;

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_NameProp);

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_CostProp);

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_RarityProp);

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_PageProp);

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_QualityTypeProp);

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_BonusEquivProp);

        EditorGUI.indentLevel--;
    }

    void Setup (SerializedProperty property)
    {
        if (property.objectReferenceValue == null)
            return;

        m_SerializedObject = new SerializedObject(property.objectReferenceValue);

        m_NameProp = m_SerializedObject.FindProperty("m_Name");
        m_CostProp = m_SerializedObject.FindProperty("cost");
        m_RarityProp = m_SerializedObject.FindProperty("rarity");
        m_PageProp = m_SerializedObject.FindProperty("ulimateEquipmentPage");
        m_QualityTypeProp = m_SerializedObject.FindProperty("qualityType");
        m_BonusEquivProp = m_SerializedObject.FindProperty("bonusEquivalent");

        m_HasSetupRun = true;
    }
}
