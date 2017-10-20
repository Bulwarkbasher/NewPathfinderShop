using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Weapon))]
public class WeaponDrawer : PropertyDrawer
{
    SerializedObject m_SerializedObject;
    SerializedProperty m_NameProp;
    SerializedProperty m_CostProp;
    SerializedProperty m_RarityProp;
    SerializedProperty m_PageProp;
    SerializedProperty m_WeaponTypeProp;
    SerializedProperty m_HandednessProp;
    SerializedProperty m_DamageTypeProp;
    SerializedProperty m_SpecialProp;
    SerializedProperty m_ConstraintsProp;
    bool m_HasSetupRun;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!m_HasSetupRun)
            Setup(property);

        if (m_NameProp.isExpanded)
            return EditorGUIUtility.singleLineHeight;

        float height = 9 * EditorGUIUtility.singleLineHeight;
        if(m_ConstraintsProp.isExpanded)
            height += m_ConstraintsProp.arraySize * EditorGUIUtility.singleLineHeight;
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!m_HasSetupRun)
            Setup(property);

        Rect singleLineRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        m_NameProp.isExpanded = EditorGUI.Foldout(singleLineRect, m_NameProp.isExpanded, m_NameProp.stringValue);

        if (!m_NameProp.isExpanded)
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
        EditorGUI.PropertyField(singleLineRect, m_WeaponTypeProp);

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_HandednessProp);

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_DamageTypeProp);

        singleLineRect.y += singleLineRect.height;
        EditorGUI.PropertyField(singleLineRect, m_SpecialProp);

        singleLineRect.y += singleLineRect.height;
        m_ConstraintsProp.isExpanded = EditorGUI.Foldout(singleLineRect, m_ConstraintsProp.isExpanded, "Allowed Qualities");

        EditorGUI.indentLevel++;

        for (int i = 0; i < m_ConstraintsProp.arraySize; i++)
        {
            SerializedProperty constraintElementProp = m_ConstraintsProp.GetArrayElementAtIndex (i);
            singleLineRect.y += singleLineRect.height;
            EditorGUI.PropertyField (singleLineRect, constraintElementProp);
        }

        EditorGUI.indentLevel--;
        EditorGUI.indentLevel--;
    }

    void Setup(SerializedProperty property)
    {
        Debug.Log (property.propertyType);      // Is object reference but is null...
        if (property.objectReferenceValue == null)
            return;

        Debug.Log (property.objectReferenceValue.name);

        m_SerializedObject = new SerializedObject(property.objectReferenceValue);

        m_NameProp = m_SerializedObject.FindProperty("m_Name"); // TODO NEXT: null
        m_CostProp = m_SerializedObject.FindProperty("cost");
        m_RarityProp = m_SerializedObject.FindProperty("rarity");
        m_PageProp = m_SerializedObject.FindProperty("ulimateEquipmentPage");
        m_WeaponTypeProp = m_SerializedObject.FindProperty("weaponType");
        m_HandednessProp = m_SerializedObject.FindProperty("handedness");
        m_DamageTypeProp = m_SerializedObject.FindProperty("damageType");
        m_SpecialProp = m_SerializedObject.FindProperty("special");
        m_ConstraintsProp = m_SerializedObject.FindProperty("constraints");

        m_HasSetupRun = true;
    }
}