using UnityEngine;
using System;
using UnityEditor;
using System.Collections;

public abstract class Column
{
    public string name;
    public float width;


    private float defaultWidth;


    public Column (string name, float startingWidth)
    {
        this.name = name;
        defaultWidth = startingWidth;
        width = defaultWidth;
    }


    public abstract void SetSerializedProperties(SerializedObject[] serializedObjects);


    public void DrawHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, name);
    }


    public void OnCellGUI(int rowIndex, Rect position, SerializedObject rowSO)
    {
        rowSO.Update();

        position.x += TableEditorSettings.columnSpacing * 0.5f;
        position.y += TableEditorSettings.rowSpacing * 0.5f;
        position.width -= TableEditorSettings.columnSpacing;
        position.height -= TableEditorSettings.rowSpacing;

        DrawCell(rowIndex, position);

        rowSO.ApplyModifiedProperties();
    }


    protected abstract void DrawCell(int rowIndex, Rect position);


    public static float GetTotalWidth (Column[] columns)
    {
        float totalWidth = 0f;

        for(int i = 0; i < columns.Length; i++)
        {
            totalWidth += columns[i].width + TableEditorSettings.columnSpacing;
        }

        return totalWidth;
    }


    public static SerializedProperty[] GetProperties (SerializedObject[] serializedObjects, string propertyName)
    {
        SerializedProperty[] serializedProperties = new SerializedProperty[serializedObjects.Length];

        for(int i = 0; i < serializedObjects.Length; i++)
        {
            serializedProperties[i] = serializedObjects[i].FindProperty(propertyName);

            if (serializedProperties[i] == null)
            {
                throw new UnityException("A field with the name '" + propertyName + "' was not found.");
            }
        }

        return serializedProperties;
    }
}