using UnityEngine;
using UnityEditor;
using System.Collections;

public class Row<TElement>
    where TElement : ScriptableObject
{
    private bool deleteThisElement;
    private int index;
    private float height;
    private TElement element;
    private SerializedObject serializedObject;


    public bool DeleteThisElement
    {
        get { return deleteThisElement; }
    }


    public Row (int index, float height, TElement element)
    {
        this.index = index;
        this.height = height;
        this.element = element;
        serializedObject = new SerializedObject(element);
    }


    public void OnRowGUI (float leftX, float currentY, Column[] columns)
    {
        if (index % 2 == 0)
        {
            Rect rowRect = new Rect(leftX, currentY, Column.GetTotalWidth(columns), height);
            EditorGUI.DrawRect(rowRect, TableEditorSettings.oddRowColor);
        }

        float currentX = leftX;

        for (int i = 0; i < columns.Length; i++)
        {
            Rect divisionRect = new Rect(currentX + columns[i].width, currentY, 1f, height);
            EditorGUI.DrawRect(divisionRect, TableEditorSettings.columnDivisionColor);

            Rect cellRect = new Rect(currentX, currentY, columns[i].width, height);
            columns[i].OnCellGUI(index, cellRect, serializedObject);
            currentX += columns[i].width + TableEditorSettings.columnSpacing;
        }
    }


    public static SerializedObject[] GetSerializedObjects (Row<TElement>[] rows)
    {
        SerializedObject[] serializedObjects = new SerializedObject[rows.Length];

        for(int i = 0; i < rows.Length; i++)
        {
            serializedObjects[i] = rows[i].serializedObject;
        }

        return serializedObjects;
    }


    public void MarkForRemoval ()
    {
        deleteThisElement = true;
    }


    public void DestroyElement ()
    {
        Object.DestroyImmediate(element, true);
    }
}
