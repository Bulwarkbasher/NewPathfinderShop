using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ItemDescriptionColumn : SinglePropertyColumn
{
    public ItemDescriptionColumn () : base ("Description", 120f, "description")
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
