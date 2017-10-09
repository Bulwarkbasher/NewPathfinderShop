using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ItemDescriptionColumn : SinglePropertyColumn
{
    public ItemDescriptionColumn()
        : base("Name", 120f, "m_Name")
    { }


    protected override void DrawCell(int rowIndex, Rect position, SerializedProperty prop)
    {
        if(prop.isExpanded)
        {
            prop.stringValue = EditorGUI.TextField(position, prop.stringValue);
        }
        else
        {
            EditorGUI.LabelField(position, prop.stringValue);
        }
    }
}
