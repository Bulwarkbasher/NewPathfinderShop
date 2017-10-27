using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(QualityConstraint))]
public class QualityConstraintDrawer : PropertyDrawer
{
    SerializedObject m_SerializedObject;
    SerializedProperty m_NameProp;
    SerializedProperty m_AllowedProp;
    bool m_HasSetupRun;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!m_HasSetupRun)
            Setup(property);

        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!m_HasSetupRun)
            Setup(property);

        m_SerializedObject.Update ();

        EditorGUI.indentLevel++;
        
        Rect singleLineRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        m_AllowedProp.boolValue = EditorGUI.ToggleLeft(singleLineRect, m_NameProp.stringValue, m_AllowedProp.boolValue);

        EditorGUI.indentLevel--;

        m_SerializedObject.ApplyModifiedProperties ();
    }

    void Setup(SerializedProperty property)
    {
        if (property.objectReferenceValue == null)
            return;

        m_SerializedObject = new SerializedObject(property.objectReferenceValue);

        m_NameProp = m_SerializedObject.FindProperty("m_Name");
        m_AllowedProp = m_SerializedObject.FindProperty("allowed");

        m_HasSetupRun = true;
    }
}
