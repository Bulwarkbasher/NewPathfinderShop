using System;
using UnityEditor;
using UnityEngine;

// TODO: after making all serializeable classes jsonables, rework editors and drawers to use them properly
// NOTE: sideways text using GUI.Matrix
public abstract class SaveableEditor : Editor
{
    protected void CreateJsonableInField<TField> (SerializedProperty jsonableField, Func<TField> createBlankFunction)
        where TField : ScriptableObject
    {
        if (jsonableField.objectReferenceValue != null)
            DestroyImmediate(jsonableField.objectReferenceValue, true);

        ScriptableObject newTField = createBlankFunction();
        newTField.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newTField, target);
        jsonableField.objectReferenceValue = newTField;
    }

    protected void CreateJsonableInField<TArg0, TField>(SerializedProperty jsonableField, Func<TArg0, TField> createBlankFunction, TArg0 arg0)
        where TField : ScriptableObject
    {
        if (jsonableField.objectReferenceValue != null)
            DestroyImmediate(jsonableField.objectReferenceValue, true);

        ScriptableObject newTField = createBlankFunction(arg0);
        newTField.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newTField, target);
        jsonableField.objectReferenceValue = newTField;
    }

    protected void CreateJsonableInField<TArg0, TArg1, TField>(SerializedProperty jsonableField, Func<TArg0, TArg1, TField> createBlankFunction, TArg0 arg0, TArg1 arg1)
        where TField : ScriptableObject
    {
        if (jsonableField.objectReferenceValue != null)
            DestroyImmediate(jsonableField.objectReferenceValue, true);

        ScriptableObject newTField = createBlankFunction(arg0, arg1);
        newTField.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newTField, target);
        jsonableField.objectReferenceValue = newTField;
    }

    protected void CreateJsonableInField<TArg0, TArg1, TArg2, TField>(SerializedProperty jsonableField, Func<TArg0, TArg1, TArg2, TField> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        where TField : ScriptableObject
    {
        if (jsonableField.objectReferenceValue != null)
            DestroyImmediate(jsonableField.objectReferenceValue, true);

        ScriptableObject newTField = createBlankFunction(arg0, arg1, arg2);
        newTField.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newTField, target);
        jsonableField.objectReferenceValue = newTField;
    }

    protected void CreateJsonableInField<TArg0, TArg1, TArg2, TArg3, TField>(SerializedProperty jsonableField, Func<TArg0, TArg1, TArg2, TArg3, TField> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        where TField : ScriptableObject
    {
        if (jsonableField.objectReferenceValue != null)
            DestroyImmediate(jsonableField.objectReferenceValue, true);

        ScriptableObject newTField = createBlankFunction(arg0, arg1, arg2, arg3);
        newTField.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newTField, target);
        jsonableField.objectReferenceValue = newTField;
    }

    protected void CreateJsonableInField<TArg0, TArg1, TArg2, TArg3, TArg4, TField>(SerializedProperty jsonableField, Func<TArg0, TArg1, TArg2, TArg3, TArg4, TField> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        where TField : ScriptableObject
    {
        if (jsonableField.objectReferenceValue != null)
            DestroyImmediate(jsonableField.objectReferenceValue, true);

        ScriptableObject newTField = createBlankFunction(arg0, arg1, arg2, arg3, arg4);
        newTField.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newTField, target);
        jsonableField.objectReferenceValue = newTField;
    }

    protected void CreateJsonableInField<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TField>(SerializedProperty jsonableField, Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TField> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        where TField : ScriptableObject
    {
        if (jsonableField.objectReferenceValue != null)
            DestroyImmediate(jsonableField.objectReferenceValue, true);

        ScriptableObject newTField = createBlankFunction(arg0, arg1, arg2, arg3, arg4, arg5);
        newTField.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newTField, target);
        jsonableField.objectReferenceValue = newTField;
    }

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
                    arrayProp.RemoveArrayElementObjectAsSubAsset(i);
                }
            }
        }
    }

    protected void ClearJsonableArray (SerializedProperty arrayProp)
    {
        for(int i = 0; i < arrayProp.arraySize; i++)
        {
            SerializedProperty element = arrayProp.GetArrayElementAtIndex(i);
            DestroyImmediate(element.objectReferenceValue, true);
        }
        arrayProp.arraySize = 0;
    }

    protected void CreateJsonableInArray <TField> (SerializedProperty arrayProp, Func<TField> createBlankFunction)
        where TField : ScriptableObject
    {
        TField newElement = createBlankFunction();
        AssetDatabase.AddObjectToAsset(newElement, target);
        arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).objectReferenceValue = newElement;        
    }

    protected void CreateJsonableInArray<TArg0, TField> (SerializedProperty arrayProp, Func<TArg0, TField> createBlankFunction, TArg0 arg0)
        where TField : ScriptableObject
    {
        TField newElement = createBlankFunction(arg0);
        AssetDatabase.AddObjectToAsset(newElement, target);
        arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
    }

    protected void CreateJsonableInArray<TArg0, TArg1, TField>(SerializedProperty arrayProp, Func<TArg0, TArg1, TField> createBlankFunction, TArg0 arg0, TArg1 arg1)
        where TField : ScriptableObject
    {
        TField newElement = createBlankFunction(arg0, arg1);
        AssetDatabase.AddObjectToAsset(newElement, target);
        arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
    }

    protected void CreateJsonableInArray<TArg0, TArg1, TArg2, TField>(SerializedProperty arrayProp, Func<TArg0, TArg1, TArg2, TField> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        where TField : ScriptableObject
    {
        TField newElement = createBlankFunction(arg0, arg1, arg2);
        AssetDatabase.AddObjectToAsset(newElement, target);
        arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
    }

    protected void CreateJsonableInArray<TArg0, TArg1, TArg2, TArg3, TField>(SerializedProperty arrayProp, Func<TArg0, TArg1, TArg2, TArg3, TField> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        where TField : ScriptableObject
    {
        TField newElement = createBlankFunction(arg0, arg1, arg2, arg3);
        AssetDatabase.AddObjectToAsset(newElement, target);
        arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
    }

    protected void CreateJsonableInArray<TArg0, TArg1, TArg2, TArg3, TArg4, TField>(SerializedProperty arrayProp, Func<TArg0, TArg1, TArg2, TArg3, TArg4, TField> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        where TField : ScriptableObject
    {
        TField newElement = createBlankFunction(arg0, arg1, arg2, arg3, arg4);
        AssetDatabase.AddObjectToAsset(newElement, target);
        arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
    }

    protected void CreateJsonableInArray<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TField>(SerializedProperty arrayProp, Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TField> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        where TField : ScriptableObject
    {
        TField newElement = createBlankFunction(arg0, arg1, arg2, arg3, arg4, arg5);
        AssetDatabase.AddObjectToAsset(newElement, target);
        arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1).isExpanded = true;
    }
}
