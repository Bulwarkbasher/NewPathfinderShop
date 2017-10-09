using UnityEngine;
using UnityEditor;

public class TableEditor<TElement>
    where TElement : ScriptableObject
{
    private Row<TElement>[] rows;
    private Column[] columns;


    public TableEditor (Column[] columns)
    {
        this.columns = columns;
    }


    public void OnGUI (SerializedProperty arrayProp)
    {
        CheckArrayProp(arrayProp);

        arrayProp.serializedObject.Update();

        CheckAndCreateRows(arrayProp);

        Rect totalRect = GetTotalRect(arrayProp);
                
        float leftX = totalRect.x;
        float currentY = totalRect.y;

        OnHeaderGUI(leftX, currentY);

        RowGUI(leftX, currentY);

        CheckRowsForRemoval(arrayProp);

        arrayProp.serializedObject.ApplyModifiedProperties();
    }


    private void CheckArrayProp (SerializedProperty arrayProp)
    {
        if (arrayProp == null)
            throw new UnityException("OnGUI must be called with a non-null property.");

        if (!arrayProp.isArray)
            throw new UnityException("OnGUI must be called with an array property.");

        for (int i = 0; i < arrayProp.arraySize; i++)
        {
            if (arrayProp.GetArrayElementAtIndex(i) == null)
                throw new UnityException("OnGUI cannot be called if any of the elements of the array are null.");
        }
    }


    private void CheckAndCreateRows (SerializedProperty arrayProp)
    {
        if (rows == null || rows.Length != arrayProp.arraySize)
        {
            rows = new Row<TElement>[arrayProp.arraySize];

            for (int i = 0; i < rows.Length; i++)
            {
                TElement element = arrayProp.GetArrayElementAtIndex(i).objectReferenceValue as TElement;
                rows[i] = new Row<TElement>(i, TableEditorSettings.rowHeight + TableEditorSettings.rowSpacing, element);
            }

            SerializedObject[] serializedObjects = Row<TElement>.GetSerializedObjects(rows);

            for(int i = 0; i < columns.Length; i++)
            {
                columns[i].SetSerializedProperties(serializedObjects);

                IRowEditor<TElement> editorColumn = columns[i] as IRowEditor<TElement>;

                if(editorColumn != null)
                {
                    editorColumn.GetRows(rows);
                }
            }
        }
    }


    private Rect GetTotalRect (SerializedProperty arrayProp)
    {
        float totalWidth = Column.GetTotalWidth(columns);
        float totalHeight = (arrayProp.arraySize + 1) * (TableEditorSettings.rowHeight + TableEditorSettings.rowSpacing);

        return EditorGUILayout.GetControlRect(GUILayout.Width(totalWidth), GUILayout.Height(totalHeight));
    }


    private void OnHeaderGUI (float leftX, float y)
    {
        float currentX = leftX;

        for (int i = 0; i < columns.Length; i++)
        {
            Rect divisionRect = new Rect(currentX + columns[i].width, y, 1f, TableEditorSettings.rowHeight + TableEditorSettings.rowSpacing);
            EditorGUI.DrawRect(divisionRect, TableEditorSettings.columnDivisionColor);

            Rect headerRect = new Rect(currentX, y, columns[i].width, TableEditorSettings.rowHeight);
            columns[i].DrawHeader(headerRect);
            currentX += columns[i].width + TableEditorSettings.columnSpacing;
        }
    }


    private void RowGUI (float leftX, float currentY)
    {
        currentY += TableEditorSettings.rowHeight + TableEditorSettings.rowSpacing;

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].OnRowGUI(leftX, currentY, columns);
            currentY += TableEditorSettings.rowHeight + TableEditorSettings.rowSpacing;
        }
    }


    private void CheckRowsForRemoval (SerializedProperty arrayProp)
    {
        bool elementRemoved = false;
        for(int i = 0; i < rows.Length; i++)
        {
            if(rows[i].DeleteThisElement && Event.current.type == EventType.layout)
            {
                rows[i].DestroyElement();
                arrayProp.DeleteArrayElementAtIndex(i);
                arrayProp.DeleteArrayElementAtIndex(i);
                elementRemoved = true;
            }
        }

        if (elementRemoved)
            CheckAndCreateRows(arrayProp);
    }


    public void AddElementButtonGUI (SerializedProperty arrayProp, Object collectionAsset)
    {
        if (GUILayout.Button("Add"))
        {
            arrayProp.AddObjectAsSubAsset<TElement> (collectionAsset, true);
        }
    }
}
