using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Availability : Jsonable<Availability>
{
    public StratRanges stock;
    public StratRanges budget;
    public float budgetVariation = 0.15f;

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
    
    public int MaxTotalBudget ()
    {
        return budget.minor.max + budget.medium.max + budget.major.max;
    }

    protected override string ConvertToJsonString(string[] jsonSplitter)
    {
        string jsonString = "";

        jsonString += StratRanges.GetJsonString (stock) + jsonSplitter[0];
        jsonString += StratRanges.GetJsonString(budget) + jsonSplitter[0];
        jsonString += Wrapper<float>.GetJsonString(budgetVariation) + jsonSplitter[0];

        return jsonString;
    }

    protected override void SetupFromSplitJsonString(string[] splitJsonString)
    {
        stock = StratRanges.CreateFromJsonString(splitJsonString[0]);
        budget = StratRanges.CreateFromJsonString(splitJsonString[1]);
        budgetVariation = Wrapper<float>.CreateFromJsonString(splitJsonString[2]);
    }
}