using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class EnumColumn<TEnum> : DoublePropertyColumn
{
    public EnumColumn (string name, float width, string enumPropName) : base(name, width, "description", enumPropName)
    {
        if (!typeof(TEnum).IsEnum)
            throw new UnityException("The type used to create this EnumColumn is not an enum.");
    }


    protected override void DrawCell(int rowIndex, Rect position, SerializedProperty descripProp, SerializedProperty enumProp)
    {
        if (descripProp.isExpanded)
        {
            Enum enumValue = (Enum)Enum.GetValues(typeof(TEnum)).GetValue(enumProp.enumValueIndex);
            enumValue = EditorGUI.EnumPopup(position, enumValue);
            enumProp.enumValueIndex = Convert.ToInt32(enumValue);
        }
        else
        {
            EditorGUI.LabelField(position, enumProp.enumDisplayNames[enumProp.enumValueIndex]);
        }
    }
}
