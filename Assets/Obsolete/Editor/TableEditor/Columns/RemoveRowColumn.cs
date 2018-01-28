using UnityEditor;
using UnityEngine;

public class RemoveRowColumn<TElement> : Column, IRowEditor<TElement>
    where TElement : ScriptableObject
{
    private Row<TElement>[] rows;


    public RemoveRowColumn () : base ("Remove", 70f)
    { }


    public override void SetSerializedProperties(SerializedObject[] serializedObjects)
    { }


    protected override void DrawCell(int rowIndex, Rect position)
    {
        if(GUI.Button(position, "Remove"))
        {
            rows[rowIndex].MarkForRemoval();
        }
    }


    public void GetRows (Row<TElement>[] rows)
    {
        this.rows = rows;
    }
}


public interface IRowEditor <TElement>
    where TElement : ScriptableObject
{
    void GetRows(Row<TElement>[] rows);
}
