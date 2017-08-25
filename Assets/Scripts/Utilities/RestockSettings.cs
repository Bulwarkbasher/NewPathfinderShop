using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RestockSettings
{
    public Range days;
    public Range percent;


    static readonly string[] k_JsonSplitter =
    {
        "###RestoSettSplitter###",
    };


    public static string GetJsonString(RestockSettings restockSettings)
    {
        string jsonString = "";

        jsonString += Range.GetJsonString (restockSettings.days) + k_JsonSplitter[0];
        jsonString += Range.GetJsonString(restockSettings.percent) + k_JsonSplitter[0];

        return jsonString;
    }


    public static RestockSettings CreateFromJsonString(string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);
        
        RestockSettings restockSettings = new RestockSettings ();

        restockSettings.days = Range.CreateFromJsonString (splitJsonString[0]);
        restockSettings.percent = Range.CreateFromJsonString (splitJsonString[1]);

        return restockSettings;
    }
}