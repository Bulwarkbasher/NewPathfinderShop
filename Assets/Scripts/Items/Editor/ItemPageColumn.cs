using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ItemPageColumn : DoublePropertyColumn
{
    public ItemPageColumn () : base ("UE Page", 60f, "m_Name", "ulimateEquipmentPage")
    { }


    protected override void DrawCell(int rowIndex, Rect position, SerializedProperty firstProp, SerializedProperty secondProp)
    {
        if(firstProp.isExpanded)
        {
            secondProp.intValue = EditorGUI.IntField(position, secondProp.intValue);
        }
        else
        {
            EditorGUI.LabelField(position, secondProp.intValue.ToString());
        }
    }
}
