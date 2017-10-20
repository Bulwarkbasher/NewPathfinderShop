using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(WeaponQualityCollection))]
public class WeaponQualitiesCollectionEditor : Editor
{
    private WeaponQualityCollection weaponQualityCollection;
    private SerializedProperty qualitiesProp;
    private TableEditor<WeaponQuality> weaponQualitiesTable;

    
    private void OnEnable ()
    {
        qualitiesProp = serializedObject.FindProperty("qualities");

        weaponQualityCollection = (WeaponQualityCollection)target;

        Column[] columns =
        {
            new ItemEditButtonColumn (),
            new ItemDescriptionColumn (),
            new WeaponQualityCostColumn (),
            new QualityTypeColumn (),
            new QualityBonusEquivalentColumn (),
            new ItemRarityColumn (),
            new ItemPageColumn (),
            new FlagsPropertyColumn<Weapon.WeaponType>("Apply to Any Type", 90f, "anyOfWeaponType"),
            new FlagsPropertyColumn<Weapon.Handedness>("Apply to Any Handedness", 100f, "anyOfHandedness"),
            new FlagsPropertyColumn<Weapon.DamageType>("Apply to Any Damage Type", 100f, "anyOfDamageType"),
            new FlagsPropertyColumn<Weapon.Special>("Apply to Any Special", 200f, "anyOfSpecial"),
            //new FlagsPropertyColumn<Weapon.AttackConstraints>("Apply to Any Attack Constraints", 200f, "anyOfAttackConstraints"),
            //new FlagsPropertyColumn<Weapon.MaterialConstraints>("Apply to Any Material Constraints", 200f, "anyOfMaterialConstraints"),
            //new FlagsPropertyColumn<Weapon.SpecialConstraints>("Apply to Any Special Constraints", 200f, "anyOfSpecialConstraints"),
            new FlagsPropertyColumn<Weapon.WeaponType>("Apply to Only Type", 90f, "allOfWeaponType"),
            new FlagsPropertyColumn<Weapon.Handedness>("Apply to Only Handedness", 100f, "allOfHandedness"),
            new FlagsPropertyColumn<Weapon.DamageType>("Apply to Only Damage Type", 100f, "allOfDamageType"),
            new FlagsPropertyColumn<Weapon.Special>("Apply to Only Special", 200f, "allOfSpecial"),
            //new FlagsPropertyColumn<Weapon.AttackConstraints>("Apply to Only Attack Constraints", 200f, "allOfAttackConstraints"),
            //new FlagsPropertyColumn<Weapon.MaterialConstraints>("Apply to Only Material Constraints", 200f, "allOfMaterialConstraints"),
            //new FlagsPropertyColumn<Weapon.SpecialConstraints>("Apply to Only Special Constraints", 200f, "allOfSpecialConstraints"),
            new RemoveRowColumn<WeaponQuality> (),
        };

        weaponQualitiesTable = new TableEditor<WeaponQuality>(columns);
    }


    public override void OnInspectorGUI()
    {
        weaponQualitiesTable.OnGUI(qualitiesProp);

        weaponQualitiesTable.AddElementButtonGUI(qualitiesProp, weaponQualityCollection);
    }
}
