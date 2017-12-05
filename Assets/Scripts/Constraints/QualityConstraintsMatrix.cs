using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityConstraintsMatrix<TConstraintsMatrix, TItemCollectionFilter, TItemCollection, TItem, TQualityCollectionFilter, TQualityCollection, TQuality> : Saveable<TConstraintsMatrix>
    where TConstraintsMatrix : QualityConstraintsMatrix<TConstraintsMatrix, TItemCollectionFilter, TItemCollection, TItem, TQualityCollectionFilter, TQualityCollection, TQuality>
    where TItemCollectionFilter : ItemCollectionFilter<TItemCollectionFilter, TItemCollection, TItem>
    where TItemCollection : ItemCollection<TItemCollectionFilter, TItemCollection, TItem>
    where TItem : Item<TItem>
    where TQualityCollectionFilter : QualityCollectionFilter<TQualityCollectionFilter, TQualityCollection, TQuality>
    where TQualityCollection : QualityCollection<TQualityCollectionFilter, TQualityCollection, TQuality>
    where TQuality : Quality<TQuality>
{
    public TItemCollection itemCollection;
    public TQualityCollection qualityCollection;
    public bool[,] matrix = new bool[0, 0]; // [Rows,Columns]  -  [items, qualities]
    public bool[,] doesMatrixEntryPassFilter = new bool[0, 0];

    public static TConstraintsMatrix Create(string name, TItemCollection itemCollection, TQualityCollection qualityCollection)
    {
        TConstraintsMatrix newMatrix = CreateInstance<TConstraintsMatrix>();

        if (CheckName(name) == NameCheckResult.Bad)
            throw new UnityException("Matrix name invalid, contains invalid characters.");
        if (CheckName(name) == NameCheckResult.IsDefault)
            throw new UnityException("Matrix name invalid, name cannot start with Default");

        newMatrix.name = name;
        newMatrix.itemCollection = itemCollection;
        newMatrix.qualityCollection = qualityCollection;
        newMatrix.matrix = new bool[itemCollection.Length, qualityCollection.Length];

        SaveableHolder.AddSaveable(newMatrix);

        return newMatrix;
    }

    public TConstraintsMatrix CreateCopyFromFilter ()
    {
        ApplyFilter();
        TConstraintsMatrix newConstraintsMatrix = Create("CopyOf" + name, itemCollection.CreateCopyFromFilter(), qualityCollection.CreateCopyFromFilter());
        for(int i = 0; i < newConstraintsMatrix.itemCollection.Length; i++)
        {
            for(int j = 0; j < newConstraintsMatrix.qualityCollection.Length; j++)
            {
                newConstraintsMatrix.matrix[i, j] = CanItemUseQuality(newConstraintsMatrix.itemCollection[i], newConstraintsMatrix.qualityCollection[j]);
            }
        }

        SaveableHolder.AddSaveable(newConstraintsMatrix);

        return newConstraintsMatrix;
    }

    public void ApplyFilter ()
    {
        itemCollection.ApplyFilter();
        qualityCollection.ApplyFilter();
        doesMatrixEntryPassFilter = new bool[itemCollection.Length, qualityCollection.Length];
        for(int i = 0; i < itemCollection.Length; i++)
        {
            if (!itemCollection.doesItemPassFilter[i])
                continue;

            for (int j = 0; j < qualityCollection.Length; j++)
            {
                doesMatrixEntryPassFilter[i, j] = qualityCollection.doesItemPassFilter[j];
            }
        }
    }

    public bool CanItemUseQuality(TItem item, TQuality quality)
    {
        int itemIndex = -1;
        for (int i = 0; i < itemCollection.Length; i++)
        {
            if (itemCollection[i] == item)
                itemIndex = i;
        }

        if (itemIndex == -1)
            return false;

        int qualityIndex = -1;
        for (int i = 0; i < qualityCollection.Length; i++)
        {
            if (qualityCollection[i] == quality)
                qualityIndex = i;
        }

        if (qualityIndex == -1)
            return false;

        return matrix[itemIndex, qualityIndex];
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += name + jsonSplitter[0];
        jsonString += itemCollection.name + jsonSplitter[0];
        jsonString += qualityCollection.name + jsonSplitter[0];

        for (int i = 0; i < itemCollection.Length; i++)
        {
            for (int j = 0; j < qualityCollection.Length; j++)
            {
                jsonString += Wrapper<bool>.GetJsonString(matrix[i, j]) + jsonSplitter[0];
            }
        }

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        name = splitJsonString[0];
        itemCollection = ItemCollection<TItemCollectionFilter, TItemCollection, TItem>.Load(splitJsonString[1]);
        qualityCollection = QualityCollection<TQualityCollectionFilter, TQualityCollection, TQuality>.Load(splitJsonString[2]);

        int jsonIndex = 3;

        for (int i = 0; i < itemCollection.Length; i++)
        {
            for (int j = 0; j < qualityCollection.Length; j++)
            {
                matrix[i, j] = Wrapper<bool>.CreateFromJsonString(splitJsonString[jsonIndex]);
                jsonIndex++;
            }
        }

        ApplyFilter();
    }
}