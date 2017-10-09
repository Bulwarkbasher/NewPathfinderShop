using UnityEngine;
using UnityEditor;
using System;
using System.Text;

public class FlagsPropertyColumn<TFlags> : DoublePropertyColumn
    where TFlags : struct, IConvertible
{
    private StringBuilder stringBuilder = new StringBuilder();


    public FlagsPropertyColumn(string name, float width, string flagsPropName)
        : base(name, width, "m_Name", flagsPropName)
    {
        Type genericType = typeof(TFlags);

        if (!genericType.IsEnum)
            throw new UnityException("FlagsPropertyColumn must be created with an enum type.");

        Attribute flagsAttribute = Attribute.GetCustomAttribute(genericType, typeof(FlagsAttribute));

        if (flagsAttribute == null)
            throw new UnityException("Enum does not have the Flags attribute.");
    }


    protected override void DrawCell(int rowIndex, Rect position, SerializedProperty descripProp, SerializedProperty flagsProp)
    {
        if(descripProp.isExpanded)
        {
            DrawEditableFlags(position, flagsProp);
        }
        else
        {
            DrawUneditableFlags(position, flagsProp);
        }
    }


    protected void DrawEditableFlags (Rect position, SerializedProperty flagsProp)
    {
        Enum enumValue = FlagUtilities.ConvertIntToEnum<TFlags>(flagsProp.intValue);
        enumValue = EditorGUI.EnumMaskField(position, enumValue);
        flagsProp.intValue = Convert.ToInt32(enumValue);
    }


    protected void DrawUneditableFlags (Rect position, SerializedProperty flagsProp)
    {
        EditorGUI.LabelField(position, FlagUtilities.FlagToString<TFlags>(stringBuilder, flagsProp.intValue));
    }
}
