using System;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;


public static class SerializedPropertyExtensions
{
    public static void AddToObjectArray<T> (this SerializedProperty arrayProperty, T elementToAdd)
        where T : Object
    {
        if (!arrayProperty.isArray)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");

        arrayProperty.serializedObject.Update();
        arrayProperty.InsertArrayElementAtIndex(arrayProperty.arraySize);
        arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1).objectReferenceValue = elementToAdd;
        arrayProperty.serializedObject.ApplyModifiedProperties();
    }


    public static void RemoveFromObjectArrayAt (this SerializedProperty arrayProperty, int index)
    {
        if(index < 0)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " cannot have negative elements removed.");

        if (!arrayProperty.isArray)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");

        if(index > arrayProperty.arraySize - 1)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " has only " + arrayProperty.arraySize + " elements so element " + index + " cannot be removed.");

        arrayProperty.serializedObject.Update();
        if (arrayProperty.GetArrayElementAtIndex(index).objectReferenceValue)
            arrayProperty.DeleteArrayElementAtIndex(index);
        arrayProperty.DeleteArrayElementAtIndex(index);
        arrayProperty.serializedObject.ApplyModifiedProperties();
    }


    public static void RemoveFromObjectArray<T> (this SerializedProperty arrayProperty, T elementToRemove)
        where T : Object
    {
        if (!arrayProperty.isArray)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");

        if(!elementToRemove)
            throw new UnityException("Removing a null element is not supported using this method.");

        arrayProperty.serializedObject.Update();

        for (int i = 0; i < arrayProperty.arraySize; i++)
        {
            SerializedProperty elementProperty = arrayProperty.GetArrayElementAtIndex(i);

            if (elementProperty.objectReferenceValue == elementToRemove)
            {
                arrayProperty.RemoveFromObjectArrayAt (i);
                return;
            }
        }

        throw new UnityException("Element " + elementToRemove.name + "was not found in property " + arrayProperty.name);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset)
        where TElement : ScriptableObject
    {
        TElement newElement = ScriptableObject.CreateInstance<TElement>();
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = "New Element";
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement> (this SerializedProperty arrayProp, Object collectionAsset, bool hideSubAsset)
        where TElement : ScriptableObject
    {
        TElement newElement = ScriptableObject.CreateInstance<TElement>();
        if(hideSubAsset)
            newElement.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = "New Element";
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset, bool hideSubAsset, string name)
        where TElement : ScriptableObject
    {
        TElement newElement = ScriptableObject.CreateInstance<TElement>();
        if (hideSubAsset)
            newElement.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = name;
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset, bool hideSubAsset, string name, Action<TElement> initMethod)
        where TElement : ScriptableObject
    {
        TElement newElement = ScriptableObject.CreateInstance<TElement>();
        if (hideSubAsset)
            newElement.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = name;
        initMethod (newElement);
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset, bool hideSubAsset, Action<TElement> initMethod)
        where TElement : ScriptableObject
    {
        TElement newElement = ScriptableObject.CreateInstance<TElement>();
        if (hideSubAsset)
            newElement.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = "New Element";
        initMethod(newElement);
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset, string name, Action<TElement> initMethod)
        where TElement : ScriptableObject
    {
        TElement newElement = ScriptableObject.CreateInstance<TElement>();
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = name;
        initMethod(newElement);
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset, string name)
        where TElement : ScriptableObject
    {
        TElement newElement = ScriptableObject.CreateInstance<TElement>();
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = name;
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset, Action<TElement> initMethod)
        where TElement : ScriptableObject
    {
        TElement newElement = ScriptableObject.CreateInstance<TElement>();
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = "New Element";
        initMethod(newElement);
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement> (this SerializedProperty arrayProp, Object collectionAsset, bool hideSubAsset, string name, TElement newElement)
        where TElement : ScriptableObject
    {
        if (hideSubAsset)
            newElement.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = name;
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset, bool hideSubAsset, TElement newElement)
        where TElement : ScriptableObject
    {
        if (hideSubAsset)
            newElement.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        arrayProp.AddToObjectArray(newElement);
    }

    public static void AddObjectAsSubAsset<TElement>(this SerializedProperty arrayProp, Object collectionAsset, string name, TElement newElement)
        where TElement : ScriptableObject
    {
        AssetDatabase.AddObjectToAsset(newElement, collectionAsset);
        newElement.name = name;
        arrayProp.AddToObjectArray(newElement);
    }

    public static void RemoveObjectAsSubAsset (this SerializedProperty arrayProp, int removeAtIndex)
    {
        Object objectToDestroy = arrayProp.GetArrayElementAtIndex (removeAtIndex).objectReferenceValue;
        arrayProp.RemoveFromObjectArrayAt (removeAtIndex);
        Object.DestroyImmediate (objectToDestroy, true);
    }
}
