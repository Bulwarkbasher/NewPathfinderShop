using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Range))]
public class RangeDrawer : PropertyDrawer
{
    GUIContent m_MinContent = new GUIContent("Min");
    GUIContent m_MaxContent = new GUIContent("Max");

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty minProp = property.FindPropertyRelative ("min");
        SerializedProperty maxProp = property.FindPropertyRelative ("max");

        float propLabelWidth = position.width / 3f;
        float subPropLabelWidth = position.width / 9f;
        float propWidth = position.width / 4.5f;

        position.width = propLabelWidth;
        EditorGUI.LabelField (position, label);

        position.x += position.width;
        position.width = subPropLabelWidth;
        EditorGUI.LabelField(position, m_MinContent);

        position.x += position.width;
        position.width = propWidth;
        EditorGUI.PropertyField (position, minProp, GUIContent.none);

        position.x += position.width;
        position.width = subPropLabelWidth;
        EditorGUI.LabelField(position, m_MaxContent);

        position.x += position.width;
        position.width = propWidth;
        EditorGUI.PropertyField(position, maxProp, GUIContent.none);
    }
}
