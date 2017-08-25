using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Text;
using System.Text.RegularExpressions;

public static class FlagUtilities
{
    public static int[] GetEnumValuesFromFlag (int flag)
    {
        if (flag < 0)
            throw new UnityException("Flags must have a value of at least 0.");

        if (flag == 0)
            return new int[0];

        List<int> enumValues = new List<int>();

        int closestPowerOfTwo = Mathf.ClosestPowerOfTwo(flag);

        if(closestPowerOfTwo > flag)
        {
            closestPowerOfTwo /= 2;
        }

        enumValues.Add(closestPowerOfTwo);

        flag -= closestPowerOfTwo;

        if (flag > 0)
        {
            int[] subEnumValues = GetEnumValuesFromFlag(flag);
            enumValues.AddRange(subEnumValues);
        }

        return enumValues.ToArray();
    }


    public static string FlagToString <TFlags> (StringBuilder sb, int flag)
    {
        Type genericType = typeof(TFlags);

        if (!genericType.IsEnum)
            throw new UnityException("Function must be called with an Enum type.");

        Attribute flagsAttribute = Attribute.GetCustomAttribute(genericType, typeof(FlagsAttribute));

        if (flagsAttribute == null)
            throw new UnityException("Enum does not have the Flags attribute.");

        int[] flagValues = GetEnumValuesFromFlag(flag);

        sb.Length = 0;

        for (int i = 0; i < flagValues.Length; i++)
        {
            sb.Append(Regex.Replace(Enum.GetName(typeof(TFlags), flagValues[i]), "([A-Z])", " $1").Trim());

            if (i != flagValues.Length - 1)
                sb.Append(", ");
        }

        return sb.ToString();
    }


    public static Enum ConvertIntToEnum<TFlags> (int intValue)
    {
        Type genericType = typeof(TFlags);

        if (!genericType.IsEnum)
            throw new UnityException("Function must be called with an Enum type.");

        Attribute flagsAttribute = Attribute.GetCustomAttribute(genericType, typeof(FlagsAttribute));

        if (flagsAttribute == null)
            throw new UnityException("Enum does not have the Flags attribute.");

        Enum value = (Enum)Enum.ToObject(genericType, intValue);

        return value;
    }


    public static bool ValueHasAllFlags (int value, int flags)
    {
        return (value | flags) == value;
    }


    public static bool ValueHasAnyFlag (int value, int flags)
    {
        return (value & flags) == 0;
    }
}
