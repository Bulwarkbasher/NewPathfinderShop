using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QualityCollection<TQualityCollectionFilter, TQualityCollection, TQuality> : ItemCollection<TQualityCollectionFilter, TQualityCollection, TQuality>
    where TQualityCollectionFilter : QualityCollectionFilter<TQualityCollectionFilter, TQualityCollection, TQuality>
    where TQualityCollection : QualityCollection<TQualityCollectionFilter, TQualityCollection, TQuality>
    where TQuality : Quality<TQuality>
{
}
