using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestSO))]
public class TestSOEditor : Editor
{
    SerializedProperty m_NameProp;

    void OnEnable ()
    {
        m_NameProp = serializedObject.FindProperty ("m_Name");
    }

    public override void OnInspectorGUI ()
    {
        TestSO testSO = (TestSO)target;

        testSO.myFloat = EditorGUILayout.FloatField ("my float", testSO.myFloat);
        //testSO.name = EditorGUILayout.TextField ("Name", testSO.name);

        serializedObject.Update ();
        EditorGUILayout.PropertyField (m_NameProp);
        serializedObject.ApplyModifiedProperties ();

        Debug.Log (JsonUtility.ToJson (testSO));
    }
}
