using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ItemEditButtonColumn : SinglePropertyColumn
{
    public ItemEditButtonColumn () : base ("Edit", 45f, "m_Name")
    { }


    protected override void DrawCell(int rowIndex, Rect position, SerializedProperty prop)
    {
        if(prop.isExpanded)
        {
            if (GUI.Button(position, "Save"))
            {
                prop.isExpanded = false;
            }
        }
        else
        {
            if(GUI.Button(position, "Edit"))
            {
                prop.isExpanded = true;
            }
        }
    }
}
