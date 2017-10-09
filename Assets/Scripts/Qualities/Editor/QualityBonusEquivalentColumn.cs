using UnityEditor;
using UnityEngine;

public class QualityBonusEquivalentColumn : DoublePropertyColumn
{
    public QualityBonusEquivalentColumn () : base ("Equivalent Bonus", 110f, "m_Name", "bonusEquivalent")
    { }


    protected override void DrawCell(int rowIndex, Rect position, SerializedProperty firstProp, SerializedProperty secondProp)
    {
        if (firstProp.isExpanded)
        {
            EditorGUI.PropertyField(position, secondProp, GUIContent.none);
        }
        else
        {
            EditorGUI.LabelField(position, Quality.BonusEquivalentNames[secondProp.enumValueIndex]);
        }
    }
}
