using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Spell))]
public class SpellDrawer : SubAssetElementDrawer
{
    SerializedProperty m_AllowancesProp;
    SerializedProperty m_PotionAllowanceProp;
    SerializedProperty m_ScrollAllowanceProp;
    SerializedProperty m_WandAllowanceProp;
    SerializedProperty m_RaritiesProp;
    SerializedProperty m_PotionRarityProp;
    SerializedProperty m_ScrollRarityProp;
    SerializedProperty m_WandRarityProp;
    SerializedProperty m_CreatorLevelsProp;
    SerializedProperty m_BookProp;
    SerializedProperty m_PageProp;
    SerializedProperty m_MaterialCostProp;

    protected override int GetPropertyLineCount (SerializedProperty property, GUIContent label)
    {
        int count = 7;
        if (m_AllowancesProp.isExpanded)
            count += 3;
        if (m_RaritiesProp.isExpanded)
            count += 3;
        if (m_CreatorLevelsProp.isExpanded)
            count += EnumSettingEditorHelpers.GetEnumSettingIntPairingLineCount(m_CreatorLevelsProp);
        return count;
    }

    protected override void GetProperties (SerializedProperty property)
    {
        m_AllowancesProp = m_SerializedObject.FindProperty ("allowances");
        m_PotionAllowanceProp = m_AllowancesProp.FindPropertyRelative ("potionAllowance");
        m_ScrollAllowanceProp = m_AllowancesProp.FindPropertyRelative("scrollAllowance");
        m_WandAllowanceProp = m_AllowancesProp.FindPropertyRelative("wandAllowance");
        m_RaritiesProp = m_SerializedObject.FindProperty("rarities");
        m_PotionRarityProp = m_RaritiesProp.FindPropertyRelative("potionRarity");
        m_ScrollRarityProp = m_RaritiesProp.FindPropertyRelative("scrollRarity");
        m_WandRarityProp = m_RaritiesProp.FindPropertyRelative("wandRarity");
        m_CreatorLevelsProp = m_SerializedObject.FindProperty("creatorLevels");
        m_BookProp = m_SerializedObject.FindProperty("book");
        m_PageProp = m_SerializedObject.FindProperty("page");
        m_MaterialCostProp = m_SerializedObject.FindProperty("materialCost");
    }

    protected override void OnElementGUI (Rect totalPropertyRect, SerializedProperty property, GUIContent label, Rect nameFoldoutLineRect)
    {
        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_NameProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_AllowancesProp.isExpanded = EditorGUI.Foldout (nameFoldoutLineRect, m_AllowancesProp.isExpanded, "Allowances");

        if (m_AllowancesProp.isExpanded)
        {
            nameFoldoutLineRect.y += nameFoldoutLineRect.height;
            EditorGUI.PropertyField (nameFoldoutLineRect, m_PotionAllowanceProp);

            nameFoldoutLineRect.y += nameFoldoutLineRect.height;
            EditorGUI.PropertyField(nameFoldoutLineRect, m_ScrollAllowanceProp);

            nameFoldoutLineRect.y += nameFoldoutLineRect.height;
            EditorGUI.PropertyField(nameFoldoutLineRect, m_WandAllowanceProp);
        }

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_RaritiesProp.isExpanded = EditorGUI.Foldout (nameFoldoutLineRect, m_RaritiesProp.isExpanded, "Rarities");

        if (m_RaritiesProp.isExpanded)
        {
            nameFoldoutLineRect.y += nameFoldoutLineRect.height;
            EditorGUI.PropertyField(nameFoldoutLineRect, m_PotionRarityProp);

            nameFoldoutLineRect.y += nameFoldoutLineRect.height;
            EditorGUI.PropertyField(nameFoldoutLineRect, m_ScrollRarityProp);

            nameFoldoutLineRect.y += nameFoldoutLineRect.height;
            EditorGUI.PropertyField(nameFoldoutLineRect, m_WandRarityProp);
        }

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        m_CreatorLevelsProp.isExpanded = EditorGUI.Foldout(nameFoldoutLineRect, m_CreatorLevelsProp.isExpanded, "Creator Levels");

        if (m_CreatorLevelsProp.isExpanded)
        {
            EnumSettingEditorHelpers.DrawEnumSettingIntPairing(m_CreatorLevelsProp, "Character Classes");
        }

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EnumSettingEditorHelpers.DrawEnumSettingIndexPopup(nameFoldoutLineRect, m_BookProp);

        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_PageProp);


        nameFoldoutLineRect.y += nameFoldoutLineRect.height;
        EditorGUI.PropertyField(nameFoldoutLineRect, m_MaterialCostProp);
    }
}
