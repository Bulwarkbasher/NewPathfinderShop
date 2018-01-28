using UnityEditor;
using UnityEngine;

// TODO NEXT: Make a PropertyDrawer for every Jsonable that is not a Saveable
public abstract class JsonableDrawer : PropertyDrawer
{
    protected SerializedObject m_SerializedObject;
    protected SerializedProperty m_NameProp;

    bool m_HasSetupRun;

    public sealed override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        if(!m_HasSetupRun)
            Setup (property);

        return GetJsonablePropertyHeight (property, label);
    }

    /// <summary>
    /// Get and sum the heights of all the serialized properties to be drawn.  Don't forget m_Name if used.
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns>The sum height of all properties.</returns>
    protected abstract float GetJsonablePropertyHeight (SerializedProperty property, GUIContent label);

    protected void Setup (SerializedProperty property)
    {
        if (property.objectReferenceValue == null)
            return;

        m_SerializedObject = new SerializedObject(property.objectReferenceValue);

        m_NameProp = m_SerializedObject.FindProperty("m_Name");

        GetProperties (property);

        m_HasSetupRun = true;
    }

    /// <summary>
    /// Used to find all the serialized properties by name on the m_SerializedObject.  Also use for any other initial setup.
    /// </summary>
    /// <param name="property"></param>
    protected abstract void GetProperties (SerializedProperty property);

    public sealed override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        if (!m_HasSetupRun)
            Setup(property);

        m_SerializedObject.Update();

        OnJsonableGUI (position, property, label);

        m_SerializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Draw the entire Jsonable.  No drawing is done outside of this method.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    protected abstract void OnJsonableGUI (Rect position, SerializedProperty property, GUIContent label);
}
