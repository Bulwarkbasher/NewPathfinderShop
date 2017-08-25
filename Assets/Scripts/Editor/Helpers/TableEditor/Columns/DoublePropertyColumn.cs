using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DoublePropertyColumn : Column
{
    protected SerializedProperty[] firstProps;
    protected SerializedProperty[] secondProps;


    protected string firstPropName;
    protected string secondPropName;


    public DoublePropertyColumn(string name, float width, string firstPropName, string secondPropName) : base (name, width)
    {
        this.firstPropName = firstPropName;
        this.secondPropName = secondPropName;
    }


    public sealed override void SetSerializedProperties(SerializedObject[] serializedObjects)
    {
        firstProps = GetProperties(serializedObjects, firstPropName);
        secondProps = GetProperties(serializedObjects, secondPropName);
        SetAdditionalSerializedProps(serializedObjects);
    }


    protected virtual void SetAdditionalSerializedProps(SerializedObject[] serializedObjects)
    { }


    protected sealed override void DrawCell(int rowIndex, Rect position)
    {
        SerializedProperty firstProp = firstProps[rowIndex];
        SerializedProperty secondProp = secondProps[rowIndex];
        DrawCell(rowIndex, position, firstProp, secondProp);
    }


    protected virtual void DrawCell(int rowIndex, Rect position, SerializedProperty firstProp, SerializedProperty secondProp)
    {
        EditorGUI.PropertyField(position, firstProp, GUIContent.none);
    }
}
