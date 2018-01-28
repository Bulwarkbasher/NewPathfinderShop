using UnityEngine;
using UnityEditor;
// TODO: how to initialise null objects?  they need references to the asset in order to be added
[CustomPropertyDrawer(typeof(Spell))]
public class SpellDrawer : JsonableDrawer
{
    SerializedProperty m_ContainerAllowancesProp;
    SerializedProperty m_ContainerRaritiesProp;
    SerializedProperty m_CreatorLevelsProp;
    SerializedProperty m_BookProp;
    SerializedProperty m_PageProp;
    SerializedProperty m_MaterialCostProp;

    protected override float GetJsonablePropertyHeight (SerializedProperty property, GUIContent label)
    {
        if (!property.isExpanded)
            return EditorGUIUtility.singleLineHeight;

        float height = EditorGUIUtility.singleLineHeight;
        height += EditorGUI.GetPropertyHeight(m_NameProp);
        height += EditorGUI.GetPropertyHeight(m_ContainerAllowancesProp);
        height += EditorGUI.GetPropertyHeight(m_ContainerRaritiesProp);
        height += EditorGUI.GetPropertyHeight(m_CreatorLevelsProp);
        height += EditorGUI.GetPropertyHeight(m_BookProp);
        height += EditorGUI.GetPropertyHeight(m_PageProp);
        height += EditorGUI.GetPropertyHeight(m_MaterialCostProp);

        return height;
    }

    protected override void GetProperties (SerializedProperty property)
    {
        m_ContainerAllowancesProp = m_SerializedObject.FindProperty ("containerAllowances");
        m_ContainerRaritiesProp = m_SerializedObject.FindProperty("containerRarities");
        m_CreatorLevelsProp = m_SerializedObject.FindProperty("creatorLevels");
        m_BookProp = m_SerializedObject.FindProperty("book");
        m_PageProp = m_SerializedObject.FindProperty("page");
        m_MaterialCostProp = m_SerializedObject.FindProperty("materialCost");
    }

    protected override void OnJsonableGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = EditorGUIUtility.singleLineHeight;
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, new GUIContent(m_NameProp.stringValue));

        if (!property.isExpanded)
            return;

        EditorGUI.indentLevel++;

        position.y += position.height;
        position.height = EditorGUI.GetPropertyHeight(m_NameProp);
        EditorGUI.PropertyField(position, m_NameProp);

        position.y += position.height;
        position.height = EditorGUI.GetPropertyHeight(m_ContainerAllowancesProp);
        EditorGUI.PropertyField(position, m_ContainerAllowancesProp);

        position.y += position.height;
        position.height = EditorGUI.GetPropertyHeight(m_ContainerRaritiesProp);
        EditorGUI.PropertyField(position, m_ContainerRaritiesProp);

        position.y += position.height;
        position.height = EditorGUI.GetPropertyHeight(m_CreatorLevelsProp);
        EditorGUI.PropertyField(position, m_CreatorLevelsProp);

        position.y += position.height;
        position.height = EditorGUI.GetPropertyHeight(m_BookProp);
        EditorGUI.PropertyField(position, m_BookProp);

        position.y += position.height;
        position.height = EditorGUI.GetPropertyHeight(m_PageProp);
        EditorGUI.PropertyField(position, m_PageProp);

        position.y += position.height;
        position.height = EditorGUI.GetPropertyHeight(m_MaterialCostProp);
        EditorGUI.PropertyField(position, m_MaterialCostProp);

        EditorGUI.indentLevel--;
    }
}
