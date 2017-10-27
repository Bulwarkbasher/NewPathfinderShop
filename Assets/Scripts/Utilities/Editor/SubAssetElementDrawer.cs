using UnityEditor;
using UnityEngine;

public abstract class SubAssetElementDrawer : PropertyDrawer
{
    protected SerializedObject m_SerializedObject;
    protected SerializedProperty m_NameProp;

    bool m_HasSetupRun;

    public sealed override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        if(!m_HasSetupRun)
            Setup (property);

        if (!property.isExpanded)
            return EditorGUIUtility.singleLineHeight;

        return GetPropertyLineCount (property, label) * EditorGUIUtility.singleLineHeight;
    }

    protected abstract int GetPropertyLineCount (SerializedProperty property, GUIContent label);

    protected void Setup (SerializedProperty property)
    {
        if (property.objectReferenceValue == null)
            return;

        m_SerializedObject = new SerializedObject(property.objectReferenceValue);

        m_NameProp = m_SerializedObject.FindProperty("m_Name");

        GetProperties (property);

        m_HasSetupRun = true;
    }

    protected abstract void GetProperties (SerializedProperty property);

    public sealed override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        if (!m_HasSetupRun)
            Setup(property);

        m_SerializedObject.Update();

        Rect singleLineRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(singleLineRect, property.isExpanded, m_NameProp.stringValue);

        if (!property.isExpanded)
            return;

        EditorGUI.indentLevel++;

        OnElementGUI (position, property, label, singleLineRect);

        EditorGUI.indentLevel--;

        m_SerializedObject.ApplyModifiedProperties();
    }

    protected abstract void OnElementGUI (Rect totalPropertyRect, SerializedProperty property, GUIContent label, Rect nameFoldoutLineRect);
}
