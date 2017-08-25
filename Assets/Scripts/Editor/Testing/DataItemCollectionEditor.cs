using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataItemCollection))]
public class DataItemCollectionEditor : Editor
{
    private class CustomColumn1 : Column
    {
        private SerializedProperty[] switchableBoolProps;


        public CustomColumn1 (string name, float width) : base (name, width)
        { }


        public override void SetSerializedProperties(SerializedObject[] serializedObjects)
        {
            switchableBoolProps = GetProperties(serializedObjects, "switchableBool");
        }


        protected override void DrawCell(int rowIndex, Rect position)
        {
            EditorGUI.Toggle(position, switchableBoolProps[rowIndex].boolValue);
        }
    }


    private class CustomColumn2 : Column
    {
        private SerializedProperty[] someNumberProps;


        public CustomColumn2(string name, float width) : base(name, width)
        { }


        public override void SetSerializedProperties(SerializedObject[] serializedObjects)
        {
            someNumberProps = GetProperties(serializedObjects, "someNumber");
        }


        protected override void DrawCell(int rowIndex, Rect position)
        {
            someNumberProps[rowIndex].floatValue = EditorGUI.FloatField(position, someNumberProps[rowIndex].floatValue);
        }
    }


    private class CustomColumn3 : Column
    {
        private SerializedProperty[] switchableBoolProps;


        public CustomColumn3(string name, float width) : base(name, width)
        { }


        public override void SetSerializedProperties(SerializedObject[] serializedObjects)
        {
            switchableBoolProps = GetProperties(serializedObjects, "switchableBool");
        }


        protected override void DrawCell(int rowIndex, Rect position)
        {
            if (GUI.Button(position, "switch"))
            {
                switchableBoolProps[rowIndex].boolValue = !switchableBoolProps[rowIndex].boolValue;
            }
        }
    }


    private TableEditor<DataItem> dataItemTable;
    private SerializedProperty dataItemsProp;


    private void OnEnable ()
    {
        dataItemsProp = serializedObject.FindProperty("dataItems");

        Column[] columns =
        {
            new CustomColumn1("Readonly bool", 200f),
            new CustomColumn2("number", 150f),
            new CustomColumn3("switch button", 150f),
        };

        dataItemTable = new TableEditor<DataItem>(columns);
    }


    public override void OnInspectorGUI()
    {
        dataItemTable.OnGUI(dataItemsProp);

        dataItemTable.AddElementButtonGUI(dataItemsProp, target);
    }
}
