using System;
using UnityEditor;
using UnityEngine;

// TODO: after making all serializeable classes jsonables, rework editors and drawers to use them properly
// NOTE: sideways text using GUI.Matrix
public abstract class SaveableEditor<TTarget, TElement> : Editor
    where TTarget : ScriptableObject
    where TElement : ScriptableObject
{
    protected TTarget m_Asset;

    protected void OnEnable ()
    {
        m_Asset = (TTarget)target;

        GetProperties ();
    }

    protected abstract void GetProperties ();

    public sealed override void OnInspectorGUI ()
    {
        serializedObject.Update ();

        SaveableGUI ();

        serializedObject.ApplyModifiedProperties ();
    }

    protected abstract void SaveableGUI ();

    protected void CollectionGUI (SerializedProperty arrayProp)
    {
        for (int i = 0; i < arrayProp.arraySize; i++)
        {
            SerializedProperty elementProp = arrayProp.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(elementProp);

            if (elementProp.isExpanded)
            {
                if (GUILayout.Button("Remove"))
                {
                    arrayProp.RemoveObjectAsSubAsset(i);
                }
            }
        }
    }

    protected void AddButtonGUI (SerializedProperty arrayProp, Func<TElement> createBlankFunction)
    {
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < arrayProp.arraySize; i++)
            {
                SerializedProperty elementProp = arrayProp.GetArrayElementAtIndex(i);
                elementProp.isExpanded = false;
            }

            TElement newElement = createBlankFunction ();
            arrayProp.AddObjectAsSubAsset(m_Asset, true, newElement);
            arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
        }
    }

    protected void AddButtonGUI<TArg0> (SerializedProperty arrayProp, Func<TArg0, TElement> createBlankFunction, TArg0 arg0)
    {
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < arrayProp.arraySize; i++)
            {
                SerializedProperty elementProp = arrayProp.GetArrayElementAtIndex(i);
                elementProp.isExpanded = false;
            }

            TElement newElement = createBlankFunction(arg0);
            arrayProp.AddObjectAsSubAsset(m_Asset, true, newElement);
            arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
        }
    }

    protected void AddButtonGUI<TArg0, TArg1>(SerializedProperty arrayProp, Func<TArg0, TArg1, TElement> createBlankFunction, TArg0 arg0, TArg1 arg1)
    {
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < arrayProp.arraySize; i++)
            {
                SerializedProperty elementProp = arrayProp.GetArrayElementAtIndex(i);
                elementProp.isExpanded = false;
            }

            TElement newElement = createBlankFunction(arg0, arg1);
            arrayProp.AddObjectAsSubAsset(m_Asset, true, newElement);
            arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
        }
    }

    protected void AddButtonGUI<TArg0, TArg1, TArg2>(SerializedProperty arrayProp, Func<TArg0, TArg1, TArg2, TElement> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < arrayProp.arraySize; i++)
            {
                SerializedProperty elementProp = arrayProp.GetArrayElementAtIndex(i);
                elementProp.isExpanded = false;
            }

            TElement newElement = createBlankFunction(arg0, arg1, arg2);
            arrayProp.AddObjectAsSubAsset(m_Asset, true, newElement);
            arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
        }
    }
}
