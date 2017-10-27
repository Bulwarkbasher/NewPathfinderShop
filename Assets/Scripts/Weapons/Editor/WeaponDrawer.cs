using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Weapon))]
public class WeaponDrawer : SubAssetElementDrawer
{
    SerializedProperty m_CostProp;
    SerializedProperty m_RarityProp;
    SerializedProperty m_PageProp;
    SerializedProperty m_WeaponTypeProp;
    SerializedProperty m_HandednessProp;
    SerializedProperty m_DamageTypeProp;
    SerializedProperty m_WeaponGroupProp;
    SerializedProperty m_SpecialProp;
    SerializedProperty m_ConstraintsProp;
    
    protected override int GetPropertyLineCount (SerializedProperty property, GUIContent label)
    {
        int count = 11;
        if (m_ConstraintsProp.isExpanded)
            count += m_ConstraintsProp.arraySize;
        return count;
    }

    protected override void GetProperties (SerializedProperty property)
    {
        m_CostProp = m_SerializedObject.FindProperty("cost");
        m_RarityProp = m_SerializedObject.FindProperty("rarity");
        m_PageProp = m_SerializedObject.FindProperty("ulimateEquipmentPage");
        m_WeaponTypeProp = m_SerializedObject.FindProperty("weaponType");
        m_HandednessProp = m_SerializedObject.FindProperty("handedness");
        m_DamageTypeProp = m_SerializedObject.FindProperty("damageType");
        m_WeaponGroupProp = m_SerializedObject.FindProperty("weaponGroup");
        m_SpecialProp = m_SerializedObject.FindProperty("special");
        m_ConstraintsProp = m_SerializedObject.FindProperty("constraints");
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
        EditorGUI.PropertyField(nameFoldoutLineRect, m_PageProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_WeaponTypeProp.intValue = (int)((Weapon.WeaponType)EditorGUI.EnumMaskField(nameFoldoutLineRect, "Weapon Type", (Weapon.WeaponType)m_WeaponTypeProp.intValue));

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_HandednessProp.intValue = (int)((Weapon.Handedness)EditorGUI.EnumMaskField(nameFoldoutLineRect, "Handedness", (Weapon.Handedness)m_HandednessProp.intValue));

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_DamageTypeProp.intValue = (int)((Weapon.DamageType)EditorGUI.EnumMaskField(nameFoldoutLineRect, "Damage Type", (Weapon.DamageType)m_DamageTypeProp.intValue));

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_WeaponGroupProp.intValue = (int)((Weapon.WeaponGroup)EditorGUI.EnumMaskField(nameFoldoutLineRect, "Weapon Group", (Weapon.WeaponGroup)m_WeaponGroupProp.intValue));

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_SpecialProp.intValue = (int)((Weapon.Special)EditorGUI.EnumMaskField(nameFoldoutLineRect, "Special", (Weapon.Special)m_SpecialProp.intValue));

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_ConstraintsProp.isExpanded = EditorGUI.Foldout(nameFoldoutLineRect, m_ConstraintsProp.isExpanded, "Allowed Qualities");

        if (m_ConstraintsProp.isExpanded)
        {
            EditorGUI.indentLevel++;

            for (int i = 0; i < m_ConstraintsProp.arraySize; i++)
            {
                SerializedProperty constraintElementProp = m_ConstraintsProp.GetArrayElementAtIndex(i);
                nameFoldoutLineRect.y += nameFoldoutLineRect.height;
                EditorGUI.PropertyField(nameFoldoutLineRect, constraintElementProp);
            }

            EditorGUI.indentLevel--;
        }
    }
}