using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponCollection))]
public class WeaponCollectionEditor : Editor
{
    private WeaponCollection weaponCollection;
    private SerializedProperty weaponsProp;
    private TableEditor<Weapon> weaponsTable;


    private void OnEnable ()
    {
        weaponsProp = serializedObject.FindProperty("weapons");

        weaponCollection = (WeaponCollection)target;

        Column[] columns =
        {
            new ItemEditButtonColumn(),
            new ItemDescriptionColumn(),
            new ItemCostColumn (),
            new FlagsPropertyColumn<Weapon.WeaponType>("Type", 90f, "weaponType"),
            new FlagsPropertyColumn<Weapon.Handedness>("Handedness", 100f, "handedness"),
            new FlagsPropertyColumn<Weapon.DamageType>("Damage Type", 100f, "damageType"),
            new FlagsPropertyColumn<Weapon.Special>("Special", 200f, "special"),
            //new FlagsPropertyColumn<Weapon.AttackConstraints>("Attack Constraints", 200f, "attackConstraints"),
            //new FlagsPropertyColumn<Weapon.MaterialConstraints>("Material Constraints", 200f, "materialConstraints"),
            //new FlagsPropertyColumn<Weapon.SpecialConstraints>("Special Constraints", 200f, "specialConstraints"),
            new ItemRarityColumn(),
            new ItemPageColumn(),
            new RemoveRowColumn<Weapon>(),
        };

        weaponsTable = new TableEditor<Weapon>(columns);
    }


    public override void OnInspectorGUI()
    {
        weaponsTable.OnGUI(weaponsProp);

        weaponsTable.AddElementButtonGUI(weaponsProp, weaponCollection);
    }
}
