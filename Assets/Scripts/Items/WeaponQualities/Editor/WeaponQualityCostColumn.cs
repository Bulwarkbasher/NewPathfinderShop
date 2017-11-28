using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponQualityCostColumn : DoublePropertyColumn
{
    protected SerializedProperty[] bonusEquivProps;


    public WeaponQualityCostColumn () : base ("Cost", 60f, "m_Name", "cost")
    { }


    protected override void SetAdditionalSerializedProps(SerializedObject[] serializedObjects)
    {
        bonusEquivProps = GetProperties(serializedObjects, "bonusEquivalent");
    }


    protected override void DrawCell(int rowIndex, Rect position, SerializedProperty descripProp, SerializedProperty costProp)
    {
        SerializedProperty bonusEquivProp = bonusEquivProps[rowIndex];

        if (bonusEquivProp.enumValueIndex == 0)
        {
            if(descripProp.isExpanded)
            {
                costProp.intValue = EditorGUI.IntField(position, costProp.intValue);
            }
            else
            {
                EditorGUI.LabelField(position, costProp.intValue.ToString());
            }
        }
        else
        {
            EditorGUI.LabelField(position, Quality.GetBonusEquivalentName((Quality.BonusEquivalent)bonusEquivProp.enumValueIndex));
            costProp.intValue = -1;
        }

    }
}
