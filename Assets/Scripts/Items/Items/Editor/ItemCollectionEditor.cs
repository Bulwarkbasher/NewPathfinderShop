using System;
using UnityEditor;
using UnityEngine;

public abstract class ItemCollectionEditor<TCollection, TItem> : AssetWithSubAssetElementsEditor<TCollection, TItem>
    where TCollection : ItemCollection<TCollection, TItem>
    where TItem : Item<TItem>
{
    protected SerializedProperty m_BooksProp;
    protected SerializedProperty m_ItemsProp;

    protected sealed override void GetProperties ()
    {
        m_BooksProp = serializedObject.FindProperty ("books");
        m_ItemsProp = serializedObject.FindProperty ("items");

        GetAdditionalProperties ();
    }

    protected virtual void GetAdditionalProperties ()
    {}

    protected void CollectionGUI ()
    {
        for (int i = 0; i < m_ItemsProp.arraySize; i++)
        {
            SerializedProperty elementProp = m_ItemsProp.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(elementProp);

            if (elementProp.isExpanded)
            {
                if (GUILayout.Button("Remove"))
                {
                    m_ItemsProp.RemoveObjectAsSubAsset(i);
                }
            }
        }
    }

    protected void BooksGUI ()
    {
        if (m_ItemsProp.arraySize == 0)
        {
            EditorGUILayout.PropertyField(m_BooksProp);
        }
        else
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(new GUIContent("Books"), new GUIContent(m_BooksProp.objectReferenceValue.name));
            EditorGUILayout.EndVertical();
        }
    }

    protected void DrawDefaultAssetGUI (Func<TItem> createBlankFunction)
    {
        BooksGUI();
        CollectionGUI();
        AddButtonGUI (createBlankFunction);
    }

    protected void DrawDefaultAssetGUI<TArg0>(Func<TArg0, TItem> createBlankFunction, TArg0 arg0)
    {
        BooksGUI();
        CollectionGUI();
        AddButtonGUI(createBlankFunction, arg0);
    }

    protected void DrawDefaultAssetGUI<TArg0, TArg1>(Func<TArg0, TArg1, TItem> createBlankFunction, TArg0 arg0, TArg1 arg1)
    {
        BooksGUI();
        CollectionGUI();
        AddButtonGUI(createBlankFunction, arg0, arg1);
    }

    protected void DrawDefaultAssetGUI<TArg0, TArg1, TArg2>(Func<TArg0, TArg1, TArg2, TItem> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        BooksGUI();
        CollectionGUI();
        AddButtonGUI(createBlankFunction, arg0, arg1, arg2);
    }

    protected void AddButtonGUI(Func<TItem> createBlankFunction)
    {
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < m_ItemsProp.arraySize; i++)
            {
                SerializedProperty elementProp = m_ItemsProp.GetArrayElementAtIndex(i);
                elementProp.isExpanded = false;
            }

            TItem newElement = createBlankFunction();
            m_ItemsProp.AddObjectAsSubAsset(m_Asset, true, newElement);
            m_ItemsProp.GetArrayElementAtIndex(m_ItemsProp.arraySize - 1).isExpanded = true;
        }
    }

    protected void AddButtonGUI<TArg0>(Func<TArg0, TItem> createBlankFunction, TArg0 arg0)
    {
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < m_ItemsProp.arraySize; i++)
            {
                SerializedProperty elementProp = m_ItemsProp.GetArrayElementAtIndex(i);
                elementProp.isExpanded = false;
            }

            TItem newElement = createBlankFunction(arg0);
            m_ItemsProp.AddObjectAsSubAsset(m_Asset, true, newElement);
            m_ItemsProp.GetArrayElementAtIndex(m_ItemsProp.arraySize - 1).isExpanded = true;
        }
    }

    protected void AddButtonGUI<TArg0, TArg1>(Func<TArg0, TArg1, TItem> createBlankFunction, TArg0 arg0, TArg1 arg1)
    {
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < m_ItemsProp.arraySize; i++)
            {
                SerializedProperty elementProp = m_ItemsProp.GetArrayElementAtIndex(i);
                elementProp.isExpanded = false;
            }

            TItem newElement = createBlankFunction(arg0, arg1);
            m_ItemsProp.AddObjectAsSubAsset(m_Asset, true, newElement);
            m_ItemsProp.GetArrayElementAtIndex(m_ItemsProp.arraySize - 1).isExpanded = true;
        }
    }

    protected void AddButtonGUI<TArg0, TArg1, TArg2>(Func<TArg0, TArg1, TArg2, TItem> createBlankFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        if (GUILayout.Button("Add"))
        {
            for (int i = 0; i < m_ItemsProp.arraySize; i++)
            {
                SerializedProperty elementProp = m_ItemsProp.GetArrayElementAtIndex(i);
                elementProp.isExpanded = false;
            }

            TItem newElement = createBlankFunction(arg0, arg1, arg2);
            m_ItemsProp.AddObjectAsSubAsset(m_Asset, true, newElement);
            m_ItemsProp.GetArrayElementAtIndex(m_ItemsProp.arraySize - 1).isExpanded = true;
        }
    }
}
