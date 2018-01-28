using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SinglePropertyColumn : Column
{
    protected SerializedProperty[] props;


    protected string propName;


    public SinglePropertyColumn(string name, float width, string propName) : base (name, width)
    {
        this.propName = propName;
    }


    public sealed override void SetSerializedProperties(SerializedObject[] serializedObjects)
    {
        props = GetProperties(serializedObjects, propName);
        SetAdditionalSerializedProps(serializedObjects);
    }


    protected virtual void SetAdditionalSerializedProps(SerializedObject[] serializedObjects)
    { }


    protected sealed override void DrawCell(int rowIndex, Rect position)
    {
        SerializedProperty prop = props[rowIndex];
        DrawCell(rowIndex, position, prop);
    }


    protected virtual void DrawCell(int rowIndex, Rect position, SerializedProperty prop)
    {
        EditorGUI.PropertyField(position, prop, GUIContent.none);
    }
}
