using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Availability
{
    public StratRanges stock;
    public StratRanges budget;
    public float budgetVariation = 0.15f;


    private static readonly string[] k_JsonSplitter =
    {
        "###AvailabilitySplitter###",
    };


    public int RandomMinorTotalBudget()
    {
        return budget.minor.Random() * stock.minor.Random();
    }


    public int RandomMediumTotalBudget()
    {
        return budget.medium.Random() * stock.medium.Random();
    }


    public int RandomMajorTotalBudget()
    {
        return budget.major.Random() * stock.major.Random();
    }


    public int RandomTotalBudget()
    {
        return RandomMinorTotalBudget() + RandomMediumTotalBudget() + RandomMajorTotalBudget();
    }


    public static string GetJsonString (Availability availability)
    {
        string stockString = StratRanges.GetJsonString(availability.stock);
        string budgetString = StratRanges.GetJsonString(availability.budget);
        string budgetVarString = Wrapper<float>.GetJsonString(availability.budgetVariation);

        return stockString + k_JsonSplitter[0] + budgetString + k_JsonSplitter[0] + budgetVarString;
    }


    public static Availability CreateFromJsonString (string jsonString)
    {
        string[] splitJsonString = jsonString.Split(k_JsonSplitter, StringSplitOptions.RemoveEmptyEntries);

        string stockString = splitJsonString[0];
        string budgetString = splitJsonString[1];
        string budgetVarString = splitJsonString[2];

        Availability availability = new Availability();

        availability.stock = StratRanges.CreateFromJsonString(stockString);
        availability.budget = StratRanges.CreateFromJsonString(budgetString);
        availability.budgetVariation = Wrapper<float>.CreateFromJsonString(budgetVarString);

        return availability;
    }
}