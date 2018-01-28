using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WeaponQuality))]
public class WeaponQualityDrawer : JsonableDrawer
{
    SerializedProperty m_CostProp;
    SerializedProperty m_RarityProp;
    SerializedProperty m_BookProp;
    SerializedProperty m_PageProp;
    SerializedProperty m_QualityTypeProp;
    SerializedProperty m_BonusEquivProp;

    protected override float GetJsonablePropertyHeight (SerializedProperty property, GUIContent label)
    {
        int count = 6;
        //if ((Quality.QualityType)m_QualityTypeProp.intValue != Quality.QualityType.SpecialMaterial)
            count++;

        //if ((Quality.BonusEquivalent)m_BonusEquivProp.intValue == Quality.BonusEquivalent.NA)
            count++;

        return count;
    }

    protected override void GetProperties (SerializedProperty property)
    {
        m_CostProp = m_SerializedObject.FindProperty("cost");
        m_RarityProp = m_SerializedObject.FindProperty("rarity");
        m_BookProp = m_SerializedObject.FindProperty("book");
        m_PageProp = m_SerializedObject.FindProperty("page");
        m_QualityTypeProp = m_SerializedObject.FindProperty("qualityType");
        m_BonusEquivProp = m_SerializedObject.FindProperty("bonusEquivalent");
    }

    protected override void OnJsonableGUI (Rect totalPropertyRect, SerializedProperty property, GUIContent label)
    {
        totalPropertyRect.y += totalPropertyRect.height;
        EditorGUI.PropertyField(totalPropertyRect, m_NameProp);

        //if ((Quality.BonusEquivalent)m_BonusEquivProp.intValue == Quality.BonusEquivalent.NA)
        {
            totalPropertyRect.y += totalPropertyRect.height;
            EditorGUI.PropertyField(totalPropertyRect, m_CostProp);
        }

        totalPropertyRect.y += totalPropertyRect.height;
        EditorGUI.PropertyField(totalPropertyRect, m_RarityProp);

        totalPropertyRect.y += totalPropertyRect.height;
        EnumSettingEditorHelpers.DrawEnumSettingIndexPopup(totalPropertyRect, m_BookProp);

        totalPropertyRect.y += totalPropertyRect.height;
        EditorGUI.PropertyField(totalPropertyRect, m_PageProp);

        totalPropertyRect.y += totalPropertyRect.height;
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(totalPropertyRect, m_QualityTypeProp);
        if (EditorGUI.EndChangeCheck())
        {
            /*switch ((Quality.QualityType)m_QualityTypeProp.intValue)
            {
                // TODO FROM HOME: set page number to appropriate for each type
                case Quality.QualityType.SpecialMaterial:
                    break;
                case Quality.QualityType.EnhancementBonus:
                    break;
                case Quality.QualityType.SpecialAbility:
                    break;
            }*/
        }

        //if ((Quality.QualityType)m_QualityTypeProp.intValue != Quality.QualityType.SpecialMaterial)
        //{
            totalPropertyRect.y += totalPropertyRect.height;
            EditorGUI.PropertyField(totalPropertyRect, m_BonusEquivProp);
        //}
        //else
           // m_BonusEquivProp.enumValueIndex = (int)Quality.BonusEquivalent.NA;
    }
}
