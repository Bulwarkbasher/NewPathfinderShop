using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestMB : MonoBehaviour
{
    public float someFloat = 101f;
    public int weaponCount = 10;
    public int budget = 30000;
    public int budgetRange = 20000;
    public WeaponCollection allWeapons;
    public WeaponQualityCollection allWeaponQualities;


    private void Start ()
    {
        /*string shopContents = "";
        SpecificWeapon[] shop = new SpecificWeapon[weaponCount];
        for(int i = 0; i < weaponCount; i++)
        {
            int actualBudget = Random.Range(-budgetRange / 2, budgetRange / 2) + budget;
            SpecificWeapon specificWeapon = SpecificWeapon.CreateRandom(SpecificItem.PowerLevel.Minor, actualBudget, allWeapons, allWeaponQualities);
            shopContents += specificWeapon.ToString() + "\n";
            shop[i] = specificWeapon;
        }
        Debug.Log(shopContents);

        string[] shopWeaponJsons = new string[weaponCount];
        string shopJson = "";
        for(int i = 0; i < weaponCount; i++)
        {
            shopWeaponJsons[i] = SpecificWeapon.GetJsonString(shop[i]);
            shopJson += shopWeaponJsons[i] + "\n\n";
        }
        //Debug.Log(shopJson);

        string remadeShop = "";
        SpecificWeapon[] remadeWeapons = new SpecificWeapon[weaponCount];
        for(int i = 0; i < remadeWeapons.Length; i++)
        {
            remadeWeapons[i] = SpecificWeapon.CreateFromJsonString(shopWeaponJsons[i]);
            remadeShop += remadeWeapons[i].ToString() + "\n";
        }
        Debug.Log(remadeShop);*/

        
        string jsonString = Wrapper<float>.GetJsonString(someFloat);

        float differentFloat = Wrapper<float>.CreateFromJsonString(jsonString);
        Debug.Log(differentFloat);
    }
}