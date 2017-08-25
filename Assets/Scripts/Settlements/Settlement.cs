using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Settlement : ScriptableObject
{
    public enum Size
    {
        Thorpe,
        Hamlet,
        Village,
        SmallTown,
        LargeTown,
        SmallCity,
        LargeCity,
        Metropolis,
    }


    public readonly static Dictionary<Size, RestockSettings> defaultRestockSettings = new Dictionary<Size, RestockSettings>()
    {
        {Size.Thorpe, new RestockSettings
            {
                days = new Range { min = 29, max = 31},
                percent = new Range { min = 10 , max = 12}
            }
        },
        {Size.Hamlet, new RestockSettings
            {
                days = new Range { min = 25, max = 27},
                percent = new Range { min = 12 , max = 14}
            }
        },
        {Size.Village, new RestockSettings
            {
                days = new Range { min = 19, max = 21},
                percent = new Range { min = 14 , max = 16}
            }
        },
        {Size.SmallTown, new RestockSettings
            {
                days = new Range { min = 15, max = 17},
                percent = new Range { min = 16 , max = 18}
            }
        },
        {Size.LargeTown, new RestockSettings
            {
                days = new Range { min = 11, max = 13},
                percent = new Range { min = 18 , max = 20}
            }
        },
        {Size.SmallCity, new RestockSettings
            {
                days = new Range { min = 9, max = 11},
                percent = new Range { min = 20 , max = 22}
            }
        },
        {Size.LargeCity, new RestockSettings
            {
                days = new Range { min = 7, max = 9},
                percent = new Range { min = 22 , max = 24}
            }
        },
        {Size.Metropolis, new RestockSettings
            {
                days = new Range { min = 5, max = 7},
                percent = new Range { min = 24 , max = 26}
            }
        },
    };

    private static readonly string[] k_JsonSplitter =
    {
        "###SettlementSplitter###",
    };

    public string notes;
    public Size size;
    public RestockSettings restockSettings;
    public Shop[] shops = new Shop[0];
    
    public static Settlement Create (string name, string notes, Size size, Dictionary<Size, RestockSettings> restockSettings)
    {
        Settlement newSettlement = CreateInstance<Settlement>();
        newSettlement.name = name;
        newSettlement.notes = notes;
        newSettlement.size = size;
        newSettlement.restockSettings = restockSettings[size];
        return newSettlement;
    }
    
    public static Settlement Create (string name, string notes, Size size)
    {
        return Create(name, notes, size, defaultRestockSettings);
    }

    public static string GetJsonString (Settlement settlement)
    {
        string jsonString = "";

        jsonString += settlement.name + k_JsonSplitter[0];
        jsonString += settlement.notes + k_JsonSplitter[0];
        jsonString += Wrapper<int>.GetJsonString((int)settlement.size) + k_JsonSplitter[0];
        jsonString += RestockSettings.GetJsonString (settlement.restockSettings) + k_JsonSplitter[0];

        for (int i = 0; i < settlement.shops.Length; i++)
        {
            jsonString += Shop.GetJsonString (settlement.shops[i]) + k_JsonSplitter[0];
        }

        return jsonString;
    }

    public static Settlement CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split (k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);

        Settlement settlement = CreateInstance<Settlement> ();

        settlement.name = splitJsonString[0];
        settlement.notes = splitJsonString[1];
        settlement.size = (Size)Wrapper<int>.CreateFromJsonString (splitJsonString[2]);
        settlement.restockSettings = RestockSettings.CreateFromJsonString (splitJsonString[3]);

        settlement.shops = new Shop[splitJsonString.Length - 4];
        for (int i = 0; i < settlement.shops.Length; i++)
        {
            settlement.shops[i] = Shop.CreateFromJsonString (splitJsonString[i + 4], settlement);
        }

        return settlement;
    }
}

