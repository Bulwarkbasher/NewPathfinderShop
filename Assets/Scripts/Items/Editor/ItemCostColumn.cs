using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemCostColumn : DoublePropertyColumn
{
    public ItemCostColumn() : base ("Cost", 60f, "description", "cost")
    { }


    protected override void DrawCell(int rowIndex, Rect position, SerializedProperty firstProp, SerializedProperty secondProp)
    {
        if (firstProp.isExpanded)
        {
            secondProp.intValue = EditorGUI.IntField(position, secondProp.intValue);
        }
        else
        {
            EditorGUI.LabelField(position, secondProp.intValue.ToString());
        }
    }
}
