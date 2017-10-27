using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WeaponQuality))]
public class WeaponQualityDrawer : SubAssetElementDrawer
{
    SerializedProperty m_CostProp;
    SerializedProperty m_RarityProp;
    SerializedProperty m_PageProp;
    SerializedProperty m_QualityTypeProp;
    SerializedProperty m_BonusEquivProp;

    protected override int GetPropertyLineCount (SerializedProperty property, GUIContent label)
    {
        int count = 5;
        if ((Quality.QualityType)m_QualityTypeProp.intValue != Quality.QualityType.SpecialMaterial)
            count++;

        if ((Quality.BonusEquivalent)m_BonusEquivProp.intValue == Quality.BonusEquivalent.NA)
            count++;

        return count;
    }

    protected override void GetProperties (SerializedProperty property)
    {
        m_CostProp = m_SerializedObject.FindProperty("cost");
        m_RarityProp = m_SerializedObject.FindProperty("rarity");
        m_PageProp = m_SerializedObject.FindProperty("ulimateEquipmentPage");
        m_QualityTypeProp = m_SerializedObject.FindProperty("qualityType");
        m_BonusEquivProp = m_SerializedObject.FindProperty("bonusEquivalent");
    }

    protected override void OnElementGUI (Rect totalPropertyRect, SerializedProperty property, GUIContent label, Rect nameFoldoutLineRect)
    {
        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_NameProp);

        if ((Quality.BonusEquivalent)m_BonusEquivProp.intValue == Quality.BonusEquivalent.NA)
        {
            nameFoldoutLineRect.y += nameFoldoutLineRect.height;
            EditorGUI.PropertyField(nameFoldoutLineRect, m_CostProp);
        }

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_RarityProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_PageProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(nameFoldoutLineRect, m_QualityTypeProp);
        if (EditorGUI.EndChangeCheck())
        {
            switch ((Quality.QualityType)m_QualityTypeProp.intValue)
            {
                // TODO: set page number to appropriate for each type
                case Quality.QualityType.SpecialMaterial:
                    break;
                case Quality.QualityType.EnhancementBonus:
                    break;
                case Quality.QualityType.SpecialAbility:
                    break;
            }
        }

        if ((Quality.QualityType)m_QualityTypeProp.intValue != Quality.QualityType.SpecialMaterial)
        {
            nameFoldoutLineRect.y += nameFoldoutLineRect.height;
            EditorGUI.PropertyField(nameFoldoutLineRect, m_BonusEquivProp);
        }
        else
            m_BonusEquivProp.enumValueIndex = (int)Quality.BonusEquivalent.NA;
    }
}
